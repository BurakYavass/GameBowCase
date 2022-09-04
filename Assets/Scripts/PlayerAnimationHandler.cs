using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private PlayerController playerController;
    private PlayerStackList _stackList;

    private void Awake()
    {
        _stackList = GetComponentInParent<PlayerStackList>();
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (_stackList.basketList != null)
            if (_stackList.basketList.Count > 1 && !playerController.walking)
            {
                playerAnimator.SetBool("carry", false);
                playerAnimator.SetBool("carryidle",true);
                playerAnimator.SetBool("walking", false);
                
            }
            else if(_stackList.basketList.Count > 1 && playerController.walking)
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
