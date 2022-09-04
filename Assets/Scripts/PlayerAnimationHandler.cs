using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private PlayerController playerController;
    private PlayerGrapeStackList _grapeStackList;

    private void Awake()
    {
        _grapeStackList = GetComponentInParent<PlayerGrapeStackList>();
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (_grapeStackList.basketList != null)
            if (_grapeStackList.basketList.Count > 1 && !playerController.walking)
            {
                playerAnimator.SetBool("carry", false);
                playerAnimator.SetBool("carryidle",true);
                playerAnimator.SetBool("walking", false);
                
            }
            else if(_grapeStackList.basketList.Count > 1 && playerController.walking)
            {
                playerAnimator.SetBool("carry", true);
                playerAnimator.SetBool("carryidle",false);
            }
            else
            {
                playerAnimator.SetBool("carry", false);
                playerAnimator.SetBool("carryidle",false);
                if (playerController.walking)
                {
                    playerAnimator.SetBool("walking", true);
                }
                else
                {
                    playerAnimator.SetBool("walking", false);
                }
            }
    }
}
