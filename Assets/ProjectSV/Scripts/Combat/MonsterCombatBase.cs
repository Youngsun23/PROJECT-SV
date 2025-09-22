using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
public enum MonsterAIState
{
    PeaceState,
    CombatState,
}

public class MonsterCombatBase : MonoBehaviour, IActor, IDamage
{
    [field: SerializeField] public MonsterAIState AIState { get; protected set; }
    [field: SerializeField] public float MaxHP { get; protected set; }
    [field: SerializeField] public float MaxStamina { get; protected set; }
    [field: SerializeField] public float MoveSpeed { get; protected set; }
    [field: SerializeField] public float PatrolRange { get; protected set; }
    [field: SerializeField] public float DetectRadius { get; protected set; }
    [field: SerializeField] public float AttackPossibleRadius { get; protected set; }
    [field: SerializeField] public float AttackRadius { get; protected set; }
    [field: SerializeField] public float AttackDamage { get; protected set; }
    [field: SerializeField] public float AttackCoolTime { get; protected set; }

    [SerializeField] private float currentHP;
    [SerializeField] private float currentStamina;

    [SerializeField] private MonsterGameData monsterStatData;
    [SerializeField] private MonsterOwnedSensor sensor;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    private PlayerCharacter targetPlayer;
    private bool hasPatrolTargetPos = false;
    private Vector3 targetPatrolPos;
    private bool attackAvailable = true;
    private bool justAttacked = false;
    private float attackCoolTimer;
    private bool isDead = false;
    private float stunTimer = 0f;
    private Coroutine hitCoroutine;

    private void Awake()
    {
        MaxHP = monsterStatData.MaxHP;
        MaxStamina = monsterStatData.MaxStamina;
        MoveSpeed = monsterStatData.MoveSpeed;
        PatrolRange = monsterStatData.PatrolRange;
        DetectRadius = monsterStatData.DetectRadius;
        AttackPossibleRadius = monsterStatData.AttackPossibleRadius;
        AttackRadius = monsterStatData.AttackRadius;
        AttackDamage = monsterStatData.AttackDamage;
        AttackCoolTime = monsterStatData.AttackCoolTime;
        sensor.SetSensorDetectRadius(monsterStatData.DetectRadius);
    }

    private void Start()
    {
        sensor.OnDetectedTarget += OnDetectedPlayer;
        sensor.OnLostTarget += OnLostPlayer;
        currentHP = MaxHP;
    }

    private void FixedUpdate()
    {
        if (isDead) 
            return;

        switch (AIState)
        {
            case MonsterAIState.CombatState:
                {
                    if(justAttacked)
                    {
                        attackCoolTimer += Time.deltaTime;
                        if (attackCoolTimer >= AttackCoolTime)
                        {
                            justAttacked = false;
                            attackAvailable = true;
                        }
                    }
                    else if(Vector2.Distance(transform.position, targetPlayer.transform.position) <= AttackPossibleRadius)
                    {
                        Attack();
                    }
                    else
                    {
                        Chase();
                    }
                }
                break;
            case MonsterAIState.PeaceState:
                {
                    Patrol();
                }
                break;
        }
    }

    public void Patrol()
    {
        // Debug.Log($"{this.gameObject.name} - {nameof(Patrol)}");
        if(!hasPatrolTargetPos)
        {
            targetPatrolPos = GetRandomPosition();
            hasPatrolTargetPos = true;
        }
        else if (Vector2.Distance(transform.position, targetPatrolPos) <= 0.1f)
            hasPatrolTargetPos = false;

        Vector3 targetPos = Vector3.MoveTowards(transform.position, targetPatrolPos, MoveSpeed * Time.deltaTime);
        rb.MovePosition(targetPos);
    }

    public void Chase()
    {
        // Debug.Log($"{this.gameObject.name} - {nameof(Chase)}");

        Vector3 targetPos = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, MoveSpeed * 2f * Time.deltaTime);
        rb.MovePosition(targetPos);
    }

    public void Attack()
    {
        Debug.Log($"{this.gameObject.name} - {nameof(Attack)}");

        // 어택 애니메이션, 이펙트
        if(attackAvailable)
            animator.SetTrigger("Attack");

        attackAvailable = false;
        Vector3 targetPos = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, AttackRadius * 2f * Time.deltaTime);
        rb.MovePosition(targetPos);
    }

    public void ExecuteAttack()
    {
        // Debug.Log($"{nameof(ExecuteAttack)}");

        //Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRadius);
        //for (int i = 0; i < targets.Length; i++)
        //{
            //if (targets[i].TryGetComponent<PlayerCharacter>(out PlayerCharacter character))
            //{
            //    character.TakeDamage(this, AttackDamage);
                justAttacked = true;
                attackCoolTimer = 0f;
        //    }
        //}
    }

    private Vector3 GetRandomPosition()
    {
        // Debug.Log($"{this.gameObject.name} - {nameof(GetRandomPosition)}");

        Vector3 randomOffset = Random.insideUnitCircle * PatrolRange;
        return transform.position + randomOffset;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ColliderWall"))
        {
            hasPatrolTargetPos = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
            return;

        if(other.TryGetComponent<PlayerCharacter>(out PlayerCharacter character))
        {
            character.TakeDamage(this, AttackDamage);
        }
    }

    public void OnDetectedPlayer(PlayerCharacter player)
    {
        Debug.Log($"{nameof(OnDetectedPlayer)}");
        AIState = MonsterAIState.CombatState;
        targetPlayer = player;
    }

    public void OnLostPlayer(PlayerCharacter player)
    {
        AIState = MonsterAIState.PeaceState;
        targetPlayer = null;
    }

    public GameObject GetActor()
    {
        return this.gameObject;
    }

    public void TakeDamage(IActor actor, float damage)
    {
        if (isDead)
            return;

        // 애니메이터
        // 이펙트
        // 카메라 쉐이크
        // 대미지 인디케이터
        OnScreenMessageManager.Singleton.ShowMessageOnScreen(transform.position, damage.ToString());

        currentHP -= damage;

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");

        Destroy(this.gameObject, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    //public override void TakeDamage(IActor actor, float damage)
    //{
    //    if (hitCoroutine != null)
    //    {
    //        StopCoroutine(hitCoroutine);
    //    }
    //    hitCoroutine = StartCoroutine(HitCoroutine(actor.GetActor().transform.forward));

    //    characterAnimator.SetTrigger("HitTrigger");

    //    var effect = EffectPoolManager.Singleton.GetEffect("MonsterTakeDamage");
    //    effect.gameObject.transform.position = transform.position + Vector3.up;

    //    currentHP -= damage;

    //    if (currentHP <= 0)
    //    {
    //        Die();
    //    }
    //}

    //private IEnumerator HitCoroutine(Vector3 direction)
    //{
    //    stunTimer = 2f;
    //    float knockbackDuration = stunTimer / 2;
    //    float knockbackSpeed = MoveSpeed * 1.2f;
    //    float elapsedTime = 0f;

    //    navMeshAgent.isStopped = true;

    //    while (elapsedTime < knockbackDuration)
    //    {
    //        float normalizedTime = elapsedTime / knockbackDuration;
    //        float easeOutFactor = 1f - Mathf.Pow(normalizedTime, 2);
    //        transform.position += direction.normalized * knockbackSpeed * easeOutFactor * Time.deltaTime;
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }


    //    navMeshAgent.isStopped = false;

    //    hitCoroutine = null;
    //}
}