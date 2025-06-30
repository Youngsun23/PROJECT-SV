using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject Player => player;
    public ItemContainer Inventory => inventory;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemContainer origin_inventory;
    public ItemDragDropController ItemDragDropController { get; private set; }
    [SerializeField] private ItemContainer inventory;

    public DialogueManager DialogueManager => dialogueManager;
    private DialogueManager dialogueManager;

    protected override void Awake()
    {
        base.Awake();
        // ItemDragDropController = GetComponent<ItemDragDropController>();
        inventory = Instantiate(origin_inventory);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // UserDataManager.Singleton.Load();
        PlayerCharacter.Singleton.InitializeCharacterAttribute();

        UIManager.Show<ToolBarUI>(UIType.ToolBar);
        UIManager.Show<DragDropUI>(UIType.DragDrop);
        ItemDragDropController = UIManager.Singleton.GetUI<DragDropUI>(UIType.DragDrop).GetComponent<ItemDragDropController>();
        dialogueManager = UIManager.Singleton.GetUI<DialogueUI>(UIType.Dialogue).GetComponent<DialogueManager>();
    }

    public void TempGameQuit()
    {
        Application.Quit();
    }
}
