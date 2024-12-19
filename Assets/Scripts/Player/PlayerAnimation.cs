using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PixelPerfectMovement playerInfo;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerInfo = GetComponent<PixelPerfectMovement>();
    }
    void Update()
    {
        if(playerInfo.inputBuffer.Count == 0)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("X", playerInfo.inputBuffer[0].x);
            animator.SetFloat("Y", playerInfo.inputBuffer[0].y);    
        }
    }
}
