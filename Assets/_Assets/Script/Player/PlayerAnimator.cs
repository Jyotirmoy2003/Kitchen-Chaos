using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   private const string IS_WALKING = "IsWalking";
    private Animator animator;
    [SerializeField] Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        //player = GetComponent<Player>();
        
    }

    void FixedUpdate()
    {
        animator.SetBool(IS_WALKING,player.IsWalking());
    }
}
