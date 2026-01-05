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

            // Debug.Log($"{this.name} - itemContainer({itemContainer.GetInstanceID()}) 생성");
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
        item.SetChestStorageData(data); // 여기서 뭐가 널이란 거임?
    }

    private void LoadStorageData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        if (item?.StorageData != null)
        {
            this.data = item.StorageData;
            itemContainer.SetItemList(data.ItemSlots);
        }
        // Debug.Log($"아이템 {this.name}에서 LoadStorageData 호출 - data 연결? {(item.StorageData != null)}");
    }

    public void Interact(PlayerCharacterController character)
    {
        if (!opened)
        {
            opened = true;
            animator.SetTrigger("Interact");
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);

            // 애니메이션 출력 후 UI 띄우기 - 코루틴/애니메이터에서 UIShow 호출/?

            UIManager.Singleton.GetUI<ChestInventoryUI>(UIType.ChestInventory).GetPanel().SetChestInventory(itemContainer); // Show()로 불러오는 UI는 하나, panel에 연결된 chestInventory도 하나, 따라서 각각의 itemContainer 정보 갖고 있는 상자 쪽에서 다시 자기 걸로 연결해줘야 함

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