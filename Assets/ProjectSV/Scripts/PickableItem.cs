using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pickableRange = 2f;

    [SerializeField] private Item item;
    [SerializeField] private int count = 1;

    private void Awake()
    {
        player = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickableRange)
            return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if(distance < 0.1f)
        {
            if (GameManager.Instance.Inventory != null)
            {
                GameManager.Instance.Inventory.AddItem(item, count);
            }
            else
            {
                Debug.LogWarning("No Inventory Attached To The GameManager");
            }

            Destroy(gameObject);
        }
    }

    public void Set(Item _item, int _count)
    {
        item = _item;   
        count = _count;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.Icon;
    }
}
