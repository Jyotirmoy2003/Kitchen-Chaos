using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Event
    [SerializeField] GameEvent OnSelectedCounterChanged;
    #endregion
    [Header("Settings")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float playerRadius = 0.7f;
    [SerializeField] float playerHeight = 2f;
    [Header("Interact settings")]
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float interactionDistance = 2f;

    private Vector3 lastInteractDir;
    private Transform myTranform;
    private bool isWalking = false;
    private ClearCounter selectedCounter;




    void Start()
    {
        myTranform = transform;
        GameInput.Instance.OnInteractAction += OnInreactButtonPressed;
    }

    private void OnInreactButtonPressed(object sender, EventArgs e)
    {
       if(selectedCounter != null)
       {
            selectedCounter.Interact();
       }
    }

    private void Update()
    {
        HandelMovement();
        HandelInteraction();
    } 
    public bool IsWalking()
    {
        return isWalking;
    }

    
    private void HandelInteraction()
    {
        
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,0f,inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        
        if(Physics.Raycast(myTranform.position,lastInteractDir,out RaycastHit hitInfo,interactionDistance,interactableLayer))
        {
            if(hitInfo.collider.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                   SetSelectedCounter(clearCounter);
                    
                }
            }else{
                SetSelectedCounter(null);
            }
        }else{
            SetSelectedCounter(null);
        }
        
    }

    private void HandelMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,0f,inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(myTranform.position,myTranform.position + Vector3.up * playerHeight ,playerRadius,moveDir,moveDistance);

        if(!canMove)
        {
            // cannot move towards movedir

            //Attempt to move in x
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(myTranform.position,myTranform.position + Vector3.up * playerHeight ,playerRadius,moveDirX,moveDistance);

            if(canMove)
            {
                //can move only on X
                moveDir = moveDirX;
            }else{
                //Arrempt to move in Y
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(myTranform.position,myTranform.position + Vector3.up * playerHeight ,playerRadius,moveDirZ,moveDistance);
                if(canMove)
                {
                    //can move only on X
                    moveDir = moveDirZ;
                }
            }
        }

        if(canMove)
        {
            myTranform.position += moveDir * moveDistance; 
        }

        isWalking = moveDir != Vector3.zero;
        myTranform.forward = Vector3.Slerp(myTranform.forward,moveDir,Time.deltaTime*rotationSpeed);
    }


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged.Raise(this,selectedCounter); //fire event when coutner changes
        
    }










}
