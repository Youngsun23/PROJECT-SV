using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance { get; private set; }

    private Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField] private float moveSpeed;

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

    private void Update()
    {
        Vector2 input = InputManager.Instance.MovementInput;
        Move(input);

        if (input.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
    }

    private void Move(Vector2 input)
    {
        rigidBody.velocity = input * moveSpeed;
    }
}
