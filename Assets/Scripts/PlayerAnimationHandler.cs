using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{ 
    private Animator playerAnimator;
    private PlayerController playerController;
    private PlayerStackList _stackList;

    private void Start()
    {
        _stackList = PlayerStackList.current;
        playerController = PlayerController.current;
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        var blend = GameManager.playerSpeed;
        playerAnimator.SetFloat("Blend", blend/10);
        if (playerController.walking)
        {
            playerAnimator.SetBool("walking", true);
            
            playerAnimator.SetBool("Idle",false);
            if (_stackList.stackList.Count > 1 )
            {
                playerAnimator.SetBool("Idle",false);
                playerAnimator.SetBool("carry", true);
                playerAnimator.SetBool("walking", false);
                playerAnimator.SetBool("carryidle",false);
            }
            else
            {
                playerAnimator.SetBool("carry", false);
            }
        }
        else if(!playerController.walking)
        {
            playerAnimator.SetBool("walking", false);
            playerAnimator.SetBool("Idle",true);
            if (_stackList.stackList.Count > 1 )
            {
                playerAnimator.SetBool("carryidle",true);
                playerAnimator.SetBool("Idle",false);
                playerAnimator.SetBool("carry", false);
                playerAnimator.SetBool("walking", false);
            }
            else
            {
                playerAnimator.SetBool("carryidle",false);
            }
        }
    }
}
