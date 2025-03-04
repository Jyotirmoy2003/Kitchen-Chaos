using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoSingleton<GameInput>
{

    private GameInputAction inputActions;

    void Awake()
    {
        base.Awake();
        
        inputActions = new GameInputAction();

        inputActions.Player.Enable();

    }


    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
