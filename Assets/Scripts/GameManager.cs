using System;
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

    public float playerGold = 100;

    public static readonly float UpgradeDuration = 2.0f;
    
    private Tween moneyTween;

    void Start()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter += PlayerMoneyDecrease;
        Application.targetFrameRate = 60;
        DOTween.Init();
    }
    
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
    }


    private void PlayerMoneyDecrease(float value)
    {
        // moneyTween.Play();
        playerGold = Mathf.Clamp(playerGold-value, 0, 5000);
        
        // if (!once)
        // {
        //     once = true;
        //     moneyTween = DOTween.To(() => playerGold, x => playerGold = x, value, UpgradeDuration).OnComplete((() => once = false));
        //
        // }
            
    }

}
