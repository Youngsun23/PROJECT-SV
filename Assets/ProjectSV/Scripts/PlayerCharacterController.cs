using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance { get; private set; }

    public Animator animator;
    public PlayerCharacterToolController toolController;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float moveSpeed;
    public Vector2 lastMoveVector { get; private set; }
    public bool isMoving;

    private void Awake()
    {
        Instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        InputManager.Instance.OnUseToolPerformed += ExecuteUseTool;
    }

    private void ExecuteUseTool()
    {
        toolController.UseTool();
    }

    private void Update()
    {
        Vector2 input = InputManager.Instance.MovementInput;
        Move(input);

        float horizontal = input.x;
        float vertical = input.y;

        if (horizontal < 0 || lastMoveVector.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        isMoving = (horizontal != 0 || vertical != 0);
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            lastMoveVector = input.normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    private void Move(Vector2 input)
    {
        rigidBody.velocity = input * moveSpeed;
    }
}
