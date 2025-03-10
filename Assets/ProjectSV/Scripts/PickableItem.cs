using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pickableRange = 2f;

    private void Awake()
    {
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickableRange)
            return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if(distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
