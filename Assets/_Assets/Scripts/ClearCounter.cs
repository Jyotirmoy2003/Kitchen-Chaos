using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    
    [SerializeField] GameObject selectedVisual;
    [SerializeField] Transform coutnerTopPoint;
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public void Interact()
    {
        Instantiate(kitchenObjectSO.prefab,coutnerTopPoint);
    }













    public void ListentoOnSelectedCounterChanged(Component sender,object data)
    {
        if(data is ClearCounter)
        {
            selectedVisual.SetActive(this == (ClearCounter)data);
        }else{
            selectedVisual.SetActive(false);
        }
    }
}
