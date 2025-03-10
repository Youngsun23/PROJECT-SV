using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterToolController : MonoBehaviour
{
    private PlayerCharacterController character;
    private Rigidbody2D rigidBody;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float interactableRange = 1.2f;

    private void Awake()
    {
        character = GetComponent<PlayerCharacterController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void UseTool()
    {
        Vector2 position = rigidBody.position + character.lastMoveVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactableRange);
        foreach(Collider2D col in colliders)
        {
            ToolHit hit = col.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
