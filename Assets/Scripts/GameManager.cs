using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private PlayerController playerController;

    public List<UpgradeArea> DeskUpgrades = new List<UpgradeArea>(7);

    public float playerGold = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        GameEventHandler.current.OngrapeUpgradeTriggerEnter += playerGrapeUpgrade;
    }

    private void Update()
    {
        if (playerGold < 50.0f)
        {
            for (int i = 0; i < DeskUpgrades.Capacity; i++)
            {
                DeskUpgrades[i].upgradeRequire.DOColor(Color.red, 1);
            }
        }
    }

    private void playerGrapeUpgrade()
    {
        var gold = Mathf.Clamp(playerGold, 0, 300);
        playerGold = gold;
        playerGold -= 10.0f * Time.deltaTime;
    }

}
