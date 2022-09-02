using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private UiManager uiManager;
    // [SerializeField] private PlayerController playerController;
    //
    // [Header("Desk Upgrade Area List")]
    // public List<UpgradeArea> DeskUpgrades = new List<UpgradeArea>(7);

    public int playerGold = 100;

    public static readonly float UpgradeDuration = 2.0f;
    
    private Tween moneyTween;

    private bool once = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        GameEventHandler.current.OnUpgradeTriggerEnter += PlayerMoneyDecrease;
        //GameEventHandler.current.OnUpgradeTriggerExit += PlayerUpgradeExit;
        //GameEventHandler.current.OnPlayerGathering += OnPlayerGathering;
    }

    // private void PlayerUpgradeExit()
    // {
    //     moneyTween.Kill();
    // }


    private void PlayerMoneyDecrease(int value)
    {
        // moneyTween.Play();
        playerGold = Mathf.Clamp(playerGold-value, 0, 5000);
        
        // if (!once)
        // {
        //     once = true;
        //     moneyTween = DOTween.To(() => playerGold, x => playerGold = x, gold, UpgradeDuration).OnComplete((() => once = false));
        //
        // }
            
    }

}
