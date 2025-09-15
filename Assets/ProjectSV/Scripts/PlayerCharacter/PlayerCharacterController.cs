using UnityEngine;

public class PlayerCharacterController : SingletonBase<PlayerCharacterController>
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerCharacterToolController toolController;
    [SerializeField] private PlayerCharacterInteraction interaction;
    // public UITabController uiTabController;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    // [SerializeField] private float moveSpeed;
    public Vector2 lastMoveVector { get; private set; }
    public bool isMoving;
    public bool isRunning;

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
        InputManager.Singleton.OnRunTogglePerformed += ExecuteRunToggle;
    }

    private void OnDestroy()
    {
        InputManager.Singleton.OnUseToolPerformed -= ExecuteUseTool;
        InputManager.Singleton.OnInteractPerformed -= ExecuteInteract;
        InputManager.Singleton.OnInventoryTogglePerformed -= ExecuteInventoryToggle;
        //InputManager.Singleton.OnSkillTabTogglePerformed -= ExecuteSkillTabToggle;
        InputManager.Singleton.OnCraftTabTogglePerformed -= ExecuteCraftTabToggle;
        InputManager.Singleton.OnEquipmentTabTogglePerformed -= ExecuteEquipmentTabToggle;
        InputManager.Singleton.OnRunTogglePerformed -= ExecuteRunToggle;
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
        //uiTabController.ToggleInventory();

        UIManager.Singleton.ToggleUI<InventoryUI>(UIType.Inventory);

        //var inventoryUI = UIManager.Singleton.GetUI<InventoryUI>(UIType.Inventory);
        //inventoryUI.Show();
    }

    //private void ExecuteSkillTabToggle()
    //{
    //    uiTabController.ToggleSkillTab();
    //}

    private void ExecuteCraftTabToggle()
    {
        // uiTabController.ToggleCraftTab();

        // 마인크래프트 Crafting
        // UIManager.Singleton.ToggleUI<CraftUI>(UIType.TempCraft);
        // 기획 Crafting
        UIManager.Singleton.ToggleUI<CraftingUI>(UIType.Crafting);
    }

    private void ExecuteEquipmentTabToggle()
    {
        // uiTabController.ToggleEquipmentTab();

        UIManager.Singleton.ToggleUI<EquipmentUI>(UIType.TempEquipment);
    }

    private void ExecuteRunToggle()
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
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

    public void Move(Vector2 input)
    {
        rigidBody.velocity = input * PlayerCharacter.Singleton.CharacterAttributeComponent.GetAttributeCurrentValue(AttributeTypes.MoveSpeed);
        if (isRunning)
            rigidBody.velocity *= 1.5f;
    }
}
