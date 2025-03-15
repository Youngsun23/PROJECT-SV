using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInteraction : MonoBehaviour
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

    public void Interact()
    {
        Vector2 position = rigidBody.position + character.lastMoveVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactableRange);
        foreach (Collider2D col in colliders)
        {
            Interactable interaction = col.GetComponent<Interactable>();
            if (interaction != null)
            {
                interaction.Interact(character);
                break;
            }
        }
    }
}
