using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private UiManager uiManager;
    //[SerializeField] private PlayerController playerController;

    //[Header("Desk Upgrade Area List")]
   // public List<UpgradeArea> DeskUpgrades = new List<UpgradeArea>(7);

    public float playerGold = 100.0f;
    
    void Start()
    {
        DOTween.Init();
        GameEventHandler.current.OngrapeUpgradeTriggerEnter += playerGrapeUpgrade;
    }

    private void Update()
    {
    }

    private void playerGrapeUpgrade()
    {
        var gold = Mathf.Clamp(playerGold, 0, 300);
        //gold = Mathf.FloorToInt(playerGold+1);
        playerGold = gold;
        playerGold -= 10 * Time.deltaTime;
    }

}
