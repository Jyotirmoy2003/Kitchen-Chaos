using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoSingleton<GameInput>
{
    public event EventHandler OnInteractAction;
    private GameInputAction inputActions;

    void Awake()
    {
        base.Awake();
        
        inputActions = new GameInputAction();

        inputActions.Player.Enable();
        inputActions.Player.Interact.performed +=  InteractPerformed;

    }

    void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction.Invoke(this,EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
