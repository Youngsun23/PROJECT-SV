using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInput playerInput;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void OnMove(InputValue input)
    {
        MovementInput = input.Get<Vector2>();
    }
}
