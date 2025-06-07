using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject Player => player;
    public ItemContainer Inventory => inventory;
    [SerializeField] private GameObject player;
    [SerializeField] private ItemContainer origin_inventory;
    public ItemDragDropController ItemDragDropController { get; private set; }
    [SerializeField] private ItemContainer inventory;

    protected override void Awake()
    {
        base.Awake();
        ItemDragDropController = GetComponent<ItemDragDropController>();
        inventory = Instantiate(origin_inventory);
    }

    private void Start()
    {
        // Initialize();
    }

    private void Initialize()
    {
        UserDataManager.Singleton.Load();
    }

    public void TempGameQuit()
    {
        Application.Quit();
    }
}
