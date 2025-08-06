using System.Collections;
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

    public PlaceableItemHighlight PlaceableItemHighlight => placeableHighlight;
    [SerializeField] private PlaceableItemHighlight placeableHighlight;

    private bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
        // ItemDragDropController = GetComponent<ItemDragDropController>();
        inventory = Instantiate(origin_inventory);
    }

    private void Start()
    {
        StartCoroutine(DelayedInitialize());
    }

    private IEnumerator DelayedInitialize() // 타이밍 문제
    {
        yield return null;

        if (!initialized)
        {
            UserDataManager.Singleton.Load();
            PlayerCharacter.Singleton.InitializeCharacterAttribute();

            UIManager.Show<ToolBarUI>(UIType.ToolBar);
            UIManager.Show<DragDropUI>(UIType.DragDrop);
            ItemDragDropController = UIManager.Singleton.GetUI<DragDropUI>(UIType.DragDrop).GetComponent<ItemDragDropController>();
            dialogueManager = UIManager.Singleton.GetUI<DialogueUI>(UIType.Dialogue).GetComponent<DialogueManager>();

            initialized = true;
        }
    }

    public void TempGameQuit()
    {
        Application.Quit();
    }
}
