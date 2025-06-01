using HAD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBase<InputManager>
{
    private PlayerInput playerInput;

    public System.Action OnUseToolPerformed;
    public System.Action OnInteractPerformed;
    public System.Action OnInventoryTogglePerformed;
    public System.Action OnSkillTabTogglePerformed;
    public System.Action OnCraftTabTogglePerformed;

    public Vector2 MovementInput { get; private set; }

    protected override void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnUseTool()
    {
        OnUseToolPerformed?.Invoke();
    }

    public void OnInteract()
    {
        OnInteractPerformed?.Invoke();
    }

    public void OnInventoryToggle()
    {
        OnInventoryTogglePerformed?.Invoke();
    }

    public void OnSkillTabToggle()
    {
        OnSkillTabTogglePerformed?.Invoke();    
    }

    public void OnCraftTabToggle()
    {
        OnCraftTabTogglePerformed?.Invoke();
    }

    public void OnMove(InputValue input)
    {
        MovementInput = input.Get<Vector2>();
    }
}
