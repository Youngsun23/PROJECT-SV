using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerCharacter : SingletonBase<PlayerCharacter>, IActor, IDamage
{
    public CharacterAttributeComponent CharacterAttributeComponent => characterAttributeComponent;
    private CharacterAttributeComponent characterAttributeComponent;
    [SerializeField] private CharacterGameData characterData;
    public CharacterResourceComponent CharacterResourceComponent => characterResourceComponent; 
    private CharacterResourceComponent characterResourceComponent;
    public DisableCharacterControl DisableCharacterControl => disableCharacterControl;
    private DisableCharacterControl disableCharacterControl;
    private Animator animator;
    private HUDBarPanel HUDBarUI;
    private HUDResourcePanel HUDResourceUI;
    private bool isInvincible = false;
    private float invincibleTimer;
    private bool hasPassedOut = false;

    protected override void Awake()
    {
        characterAttributeComponent = GetComponent<CharacterAttributeComponent>();
        characterResourceComponent = GetComponent<CharacterResourceComponent>();
        animator = GetComponent<Animator>();
        disableCharacterControl = GetComponent<DisableCharacterControl>();
    }

    private void Start()
    {
        HUDBarUI = UIManager.Singleton.GetUI<HUDUI>(UIType.HUD).gameObject.GetComponentInChildren<HUDBarPanel>();
        HUDResourceUI = UIManager.Singleton.GetUI<HUDUI>(UIType.HUD).gameObject.GetComponentInChildren<HUDResourcePanel>();
        characterAttributeComponent.RegisterEvent(AttributeTypes.HP, (float max, float cur) => HUDBarUI?.UpdateHUDUIHP(max, cur), (float max, float cur) => HUDBarUI?.UpdateHUDUIHP(max, cur));
        characterAttributeComponent.RegisterEvent(AttributeTypes.Stamina, (float max, float cur) => HUDBarUI?.UpdateHUDUIStamina(max, cur), (float max, float cur) => HUDBarUI?.UpdateHUDUIHP(max, cur));
        characterResourceComponent.RegisterEvent(ResourceTypes.Coin, (int val) => HUDResourceUI?.UpdateHUDUIMoney(val)); 
    }

    private void OnDestroy()
    {
        characterAttributeComponent.EraseEvent(AttributeTypes.HP, (float max, float cur) => HUDBarUI.UpdateHUDUIHP(max, cur), (float max, float cur) => HUDBarUI.UpdateHUDUIHP(max, cur));
        characterAttributeComponent.EraseEvent(AttributeTypes.Stamina, (float max, float cur) => HUDBarUI.UpdateHUDUIStamina(max, cur), (float max, float cur) => HUDBarUI.UpdateHUDUIStamina(max, cur));
        characterResourceComponent.EraseEvent(ResourceTypes.Coin, (int val) => HUDResourceUI?.UpdateHUDUIMoney(val));
    }

    private void Update()
    {
        if (!isInvincible) return;

        invincibleTimer += Time.deltaTime;
        if(invincibleTimer >= characterAttributeComponent.GetAttributeCurrentValue(AttributeTypes.InvincibleTime))
        {
            isInvincible = false;
        }
    }

    public void InitializeCharacterAttribute()
    {
        Dictionary<AttributeTypes, float> totalEquipmentModifier = new();

        // ���� ��� üũ
        // ���� -> EquipItem �����ϴ� ��� ID(int) List�� �����ؼ� GameDataManager�� ����� ������ List�� ����, ���� ������?
        foreach (EquipItem item in UserDataManager.Singleton.GetUserDataEquippedItems())
        {
            if (item == null || item.Effects == null) continue;

            foreach (var effect in item.Effects)
            {
                if (totalEquipmentModifier.ContainsKey(effect.Key))
                    totalEquipmentModifier[effect.Key] += effect.Value;
                else
                    totalEquipmentModifier[effect.Key] = effect.Value;
            }
        }

        characterAttributeComponent.SetAttribute(AttributeTypes.HP, characterData.HP, totalEquipmentModifier.TryGetValue(AttributeTypes.HP, out float mHP) ? mHP : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.Stamina, characterData.Stamina, totalEquipmentModifier.TryGetValue(AttributeTypes.Stamina, out float mStamina) ? mStamina : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.MoveSpeed, characterData.MoveSpeed, totalEquipmentModifier.TryGetValue(AttributeTypes.MoveSpeed, out float mMoveSpeed) ? mMoveSpeed : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.PickupRadius, characterData.PickupRadius, totalEquipmentModifier.TryGetValue(AttributeTypes.PickupRadius, out float mPickupRadius) ? mPickupRadius : 0f);
        characterAttributeComponent.SetAttribute(AttributeTypes.InvincibleTime, characterData.InvincibleTime, totalEquipmentModifier.TryGetValue(AttributeTypes.InvincibleTime, out float mInvincibleTime) ? mInvincibleTime : 0f);

        // HUDUI.UpdateHUDUIHP(characterAttributeComponent.GetAttribute(AttributeTypes.HP).MaxValue, characterAttributeComponent.GetAttribute(AttributeTypes.HP).CurrentValue);
        // HUDUI.UpdateHUDUIStamina(characterAttributeComponent.GetAttribute(AttributeTypes.Stamina).MaxValue, characterAttributeComponent.GetAttribute(AttributeTypes.Stamina).CurrentValue);

        characterResourceComponent.Initialize(UserDataManager.Singleton.GetUserDataResource());
    }

    public void Attack(Item usedWeapon)
    {
        // usedWeapon�� ���� �����ͼ� �׸�ŭ�� ��Ÿ�, �ӵ�, �����
        WeaponItem weapon = usedWeapon as WeaponItem;
        Collider2D[] overlapObjects = Physics2D.OverlapCircleAll(transform.position, weapon.Radius);

        if (overlapObjects?.Length == 0) return;

        for (int i = 0; i < overlapObjects.Length; i++)
        {
            // ����1) ĳ���Ͱ� �ٶ󺸰� �ִ� ���� ���� ���� 0.5f �̳� ��ä�� ����
            //Vector2 position = overlapObjects[i].transform.position;
            //Vector2 direction = (position - (Vector2)transform.position).normalized;

            //float dotAngle = Vector2.Dot(PlayerCharacterController.Singleton.lastMoveVector, direction);
            //DrawFOVLines(weapon.Radius, 60f);
            //if (dotAngle > 0.5f)
            //{
            //    if (overlapObjects[i].CompareTag("Monster"))
            //    {
            //        var damageInterface = overlapObjects[i].GetComponent<IDamage>();
            //        damageInterface.TakeDamage(this, weapon.Damage);
            //        Debug.Log($"{overlapObjects[i].name}���� {weapon.Damage}��ŭ�� ����");
            //    }
            //}

            // ����2) �ִϸ��̼��� ���� 90��, ���� 30��, ���� 120 ������ ġ���� �־ �װŶ� ���߱� ���� �������� �߰��� ����
            // ���� �´µ�, ���� ��ġ�ϰ� �Ϸ��� ��ġ ���� ������ �ʿ��ҵ�...��ü �� �ִϸ��̼��� �¿��Ī�� �ƴѰɱ�
            Vector2 forward = PlayerCharacterController.Singleton.lastMoveVector;
            Vector2 targetPos = overlapObjects[i].transform.position;
            Vector2 toTarget = (targetPos - (Vector2)transform.position).normalized;

            float dot = Vector2.Dot(forward, toTarget);
            float cross = forward.x * toTarget.y - forward.y * toTarget.x;
            bool hit = false;
            // ������ 80��
            if (cross < 0 && dot > Mathf.Cos(80f * Mathf.Deg2Rad))
                hit = true;
            // ���� 40��
            if (cross > 0 && dot > Mathf.Cos(40f * Mathf.Deg2Rad))
                hit = true;

            DrawAsymmetricFOVLines(weapon.Radius, 90f, 30f);

            if (hit)
            {
                if (overlapObjects[i].CompareTag("Monster") || overlapObjects[i].CompareTag("Breakable"))
                {
                    var damageInterface = overlapObjects[i].GetComponent<IDamage>();
                    damageInterface.TakeDamage(this, weapon.Damage);
                    Debug.Log($"{overlapObjects[i].name}���� {weapon.Damage}��ŭ�� ����");
                }
            }
        }
    }

    private void DrawFOVLines(float radius, float fovAngle)
    {
        Vector2 forward = PlayerCharacterController.Singleton.lastMoveVector;

        float halfFOV = fovAngle / 2f;

        // ���� �� ����
        Vector2 leftDir = Quaternion.Euler(0, 0, -halfFOV) * forward;
        Debug.DrawLine(transform.position, (Vector2)transform.position + leftDir * radius, Color.red);

        // ������ �� ����
        Vector2 rightDir = Quaternion.Euler(0, 0, halfFOV) * forward;
        Debug.DrawLine(transform.position, (Vector2)transform.position + rightDir * radius, Color.red);
    }

    private void DrawAsymmetricFOVLines(float radius, float rightAngle, float leftAngle)
    {
        Vector2 forward = PlayerCharacterController.Singleton.lastMoveVector;

        // ���� ��
        Vector2 leftDir = Quaternion.Euler(0, 0, leftAngle) * forward;
        Debug.DrawLine(transform.position, (Vector2)transform.position + leftDir * radius, Color.yellow);

        // ������ ��
        Vector2 rightDir = Quaternion.Euler(0, 0, -rightAngle) * forward;
        Debug.DrawLine(transform.position, (Vector2)transform.position + rightDir * radius, Color.yellow);
    }

    public GameObject GetActor()
    {
        return this.gameObject;
    }

    public void TakeDamage(IActor actor, float damage)
    {
        if (hasPassedOut)
            return;

        Debug.Log($"{this.gameObject.name} - {nameof(TakeDamage)} - isInvincible: {isInvincible}");
        if (isInvincible)
            return;

        // �ִϸ�����
        // ����Ʈ
        // ī�޶� ����ũ
        // �����
        OnScreenMessageManager.Singleton.ShowMessageOnScreen(transform.position, damage.ToString());

        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.HP, -damage);
        float currentHP = characterAttributeComponent.GetAttribute(AttributeTypes.HP).CurrentValue;

        // ����
        isInvincible = true;
        invincibleTimer = 0f;
        
        // HUD UI ����

        if(currentHP <= 0f)
        {
            StartCoroutine(PassOutCoroutine(1f));
        }
    }

    public void Heal(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.HP, val);
        CharacterAttribute hpAttribute = characterAttributeComponent.GetAttribute(AttributeTypes.HP);

        if(hpAttribute.CurrentValue > hpAttribute.MaxValue)
        {
            characterAttributeComponent.SetBuffedAttribute(AttributeTypes.HP, 0);
        }
    }

    public void FullHeal()
    {
        characterAttributeComponent.SetBuffedAttribute(AttributeTypes.HP, 0);
    }

    public void PassOut()
    {
        hasPassedOut = true;
        animator.SetTrigger("PassOut");

        disableCharacterControl.DisableControl();

        SceneTransitionManager.Singleton.LoadLevel("Farm", new Vector2(3f, 0f));

        StartCoroutine(WakeupCoroutine(2f));
    }

    public void WakeUp()
    {
        hasPassedOut = false;
        // TimeManager.Singleton.SkipTick(6);
        FullRecoverStamina();
        FullHeal();
        // disableCharacterControl.EnableControl();
    }

    private IEnumerator PassOutCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PassOut();
    }

    private IEnumerator WakeupCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        WakeUp();
    }

    public void UseStamina(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.Stamina, -val);
        float currentStamina = characterAttributeComponent.GetAttribute(AttributeTypes.Stamina).CurrentValue;

        // HUD UI ����

        if (currentStamina <= 0f)
        {
            GetExhausted();
        }
    }

    public void RecoverStamina(int val)
    {
        characterAttributeComponent.ChangeBuffedAttribute(AttributeTypes.Stamina, val);
        CharacterAttribute staminaAttribute = characterAttributeComponent.GetAttribute(AttributeTypes.Stamina);

        if (staminaAttribute.CurrentValue > staminaAttribute.MaxValue)
        {
            characterAttributeComponent.SetBuffedAttribute(AttributeTypes.Stamina, 0);
        }
    }

    public void FullRecoverStamina()
    {
        characterAttributeComponent.SetBuffedAttribute(AttributeTypes.Stamina, 0);
    }

    public void GetExhausted()
    {

    }
}