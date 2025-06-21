using UnityEngine;

public class PlayerCharacterController : SingletonBase<PlayerCharacterController>
{
    public Animator animator;
    public PlayerCharacterToolController toolController;
    public PlayerCharacterInteraction interaction;
    public UITabController uiTabController;
    
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    // [SerializeField] private float moveSpeed;
    public Vector2 lastMoveVector { get; private set; }
    public bool isMoving;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InputManager.Singleton.OnUseToolPerformed += ExecuteUseTool;
        InputManager.Singleton.OnInteractPerformed += ExecuteInteract;
        InputManager.Singleton.OnInventoryTogglePerformed += ExecuteInventoryToggle;
        //InputManager.Singleton.OnSkillTabTogglePerformed += ExecuteSkillTabToggle;
        InputManager.Singleton.OnCraftTabTogglePerformed += ExecuteCraftTabToggle;
        InputManager.Singleton.OnEquipmentTabTogglePerformed += ExecuteEquipmentTabToggle;
    }

    private void OnDestroy()
    {
        InputManager.Singleton.OnUseToolPerformed -= ExecuteUseTool;
        InputManager.Singleton.OnInteractPerformed -= ExecuteInteract;
        InputManager.Singleton.OnInventoryTogglePerformed -= ExecuteInventoryToggle;
        //InputManager.Singleton.OnSkillTabTogglePerformed -= ExecuteSkillTabToggle;
        InputManager.Singleton.OnCraftTabTogglePerformed -= ExecuteCraftTabToggle;
        InputManager.Singleton.OnEquipmentTabTogglePerformed -= ExecuteEquipmentTabToggle;
    }

    private void ExecuteUseTool()
    {
        if (toolController.UseToolWorld())
            return;

        toolController.UseToolGrid();
    }

    private void ExecuteInteract()
    {
        interaction.Interact();
    }

    private void ExecuteInventoryToggle()
    {
        uiTabController.ToggleInventory();
    }

    //private void ExecuteSkillTabToggle()
    //{
    //    uiTabController.ToggleSkillTab();
    //}

    private void ExecuteCraftTabToggle()
    {
        uiTabController.ToggleCraftTab();   
    }

    private void ExecuteEquipmentTabToggle()
    {
        uiTabController.ToggleEquipmentTab();
    }

    private void Update()
    {
        Vector2 input = InputManager.Singleton.MovementInput;
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
        rigidBody.velocity = input * PlayerCharacter.Singleton.CharacterAttributeComponent.GetAttributeCurrentValue(AttributeTypes.MoveSpeed);
    }
}
