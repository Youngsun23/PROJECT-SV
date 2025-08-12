using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemConvertorInteraction : MonoBehaviour, IInteractable/*, IPersistant*/
{
    [SerializeField] private Item convertableItem;
    [SerializeField] private Item convertedItem;
    [SerializeField] private int convertedItemCount = 1;
    // [SerializeField] private float timeToConvert = 5f;
    private ItemConvertorData data;
    public Animator animator;

    private void Awake()
    {
        if (data == null)
        {
            data = new ItemConvertorData();
        }
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        LoadConvertorData();
        animator.SetBool("Working", data.IsConverting);
    }

    private void OnDestroy()
    {
        SaveConvertorData();
    }

    //private void Update()
    //{
    //    if (data.ItemSlot == null) return;

    //    if(data.Timer > 0f)
    //    {
    //        Debug.Log($"≈∏¿Ã∏” update - {data.Timer}");
    //        data.TickTimer(-Time.deltaTime);
    //        if(data.Timer <= 0f)
    //        {
    //            data.ItemSlot.Set(convertedItem, convertedItemCount);
    //            animator.SetBool("Working", false);
    //        }
    //    }
    //}

    private void Update()
    {
        if (data == null) return;

        if (data.IsConvertingOver)
            animator.SetBool("Working", false);
    }

    public void Interact(PlayerCharacterController character)
    {
        if(data.ItemSlot.Item == null)
        {
            if (GameManager.Singleton.ItemDragDropController.ConvertableCheck(convertableItem))
            {
                StartItemConverting();
            }
        }
        else
        {
            if(data.IsConvertingOver)
            {
                GameManager.Singleton.Inventory.AddItem(convertedItem, convertedItemCount);
                data.ResetData();
            }
        }
    }


    private void StartItemConverting()
    {
        animator.SetBool("Working", true);

        data.ItemSlot.Copy(GameManager.Singleton.ItemDragDropController.DragDropSlot);
        GameManager.Singleton.ItemDragDropController.RemoveItem();
        data.ItemSlot.SetCount(1);
        data.SetIsConverting(true);
        SaveConvertorData();
    }

    public void SaveConvertorData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        item.SetItemConvertorData(data);
    }

    public void LoadConvertorData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        if(item?.ConvertorData != null)
            this.data = item.ConvertorData;
    }

    //public string Read()
    //{
    //    return JsonUtility.ToJson(data);
    //}

    //public void Load(string jsonString)
    //{
    //    data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    //}
}
