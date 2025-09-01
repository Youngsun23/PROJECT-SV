using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryInteraction : MonoBehaviour, IInteractable
{
    // [SerializeField] private ItemContainer container;
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private bool opened = false;
    public Animator animator;

    [SerializeField] private ItemContainer itemContainer;
    private ChestStorageData data;

    private void Awake()
    {
        if (data == null)
        {
            data = new ChestStorageData();
        }
    }

    private void Start()
    {
        if(itemContainer == null)
        {
            itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
            itemContainer.Init();

            // Debug.Log($"{this.name} - itemContainer({itemContainer.GetInstanceID()}) ����");
        }

        LoadStorageData();
    }

    private void OnDestroy()
    {
        SaveStorageData();
    }

    private void SaveStorageData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        data.SetData(itemContainer.ItemSlots);
        item.SetChestStorageData(data); // ���⼭ ���� ���̶� ����?
    }

    private void LoadStorageData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        if (item?.StorageData != null)
        {
            this.data = item.StorageData;
            itemContainer.SetItemList(data.ItemSlots);
        }
        // Debug.Log($"������ {this.name}���� LoadStorageData ȣ�� - data ����? {(item.StorageData != null)}");
    }

    public void Interact(PlayerCharacterController character)
    {
        if (!opened)
        {
            opened = true;
            animator.SetTrigger("Interact");
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);

            // �ִϸ��̼� ��� �� UI ���� - �ڷ�ƾ/�ִϸ����Ϳ��� UIShow ȣ��/?

            UIManager.Singleton.GetUI<ChestInventoryUI>(UIType.ChestInventory).GetPanel().SetChestInventory(itemContainer); // Show()�� �ҷ����� UI�� �ϳ�, panel�� ����� chestInventory�� �ϳ�, ���� ������ itemContainer ���� ���� �ִ� ���� �ʿ��� �ٽ� �ڱ� �ɷ� ��������� ��

            UIManager.Show<ChestInventoryUI>(UIType.ChestInventory);
        }
        else
        {
            opened = false;
            animator.SetTrigger("Interact");
            chestOpen.SetActive(false);
            chestClosed.SetActive(true);

            UIManager.Hide<ChestInventoryUI>(UIType.ChestInventory);
        }
    }
}