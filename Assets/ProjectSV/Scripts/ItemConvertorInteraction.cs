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
    [SerializeField] private float timeToConvert = 5f;
    private ItemConvertorData data;
    private Animator animator;

    private void Start()
    {
        if(data == null)
        {
            data = new ItemConvertorData();
            LoadConvertorData();
        }
        animator = GetComponent<Animator>();
        animator.SetBool("Working", data.Timer > 0f);
    }

    private void OnDestroy()
    {
        SaveConvertorData();
    }

    private void Update()
    {
        if (data.ItemSlot == null) return;
        
        if(data.Timer > 0f)
        {
            Debug.Log($"≈∏¿Ã∏” update - {data.Timer}");
            data.TickTimer(-Time.deltaTime);
            if(data.Timer <= 0f)
            {
                data.ItemSlot.Set(convertedItem, convertedItemCount);
                animator.SetBool("Working", false);
            }
        }
        
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
            if(data.Timer <= 0f)
            {
                GameManager.Singleton.Inventory.AddItem(data.ItemSlot.Item, data.ItemSlot.Count);
                data.ItemSlot.Clear();
            }
        }
    }


    private void StartItemConverting()
    {
        animator.SetBool("Working", true);
        
        data.ItemSlot.Copy(GameManager.Singleton.ItemDragDropController.DragDropSlot);
        GameManager.Singleton.ItemDragDropController.RemoveItem();

        data.SetTImer(timeToConvert);
        data.ItemSlot.SetCount(1);
    }

    public void SaveConvertorData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        item.SetItemConvertorData(data);
    }

    public void LoadConvertorData()
    {
        PlacedItem item = PlaceableObjectsManager.Singleton.Container.PlacedItems.Find(x => x.Transform == this.gameObject.GetComponent<Transform>());
        if(item != null)
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
