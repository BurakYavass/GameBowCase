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
    
    //[SerializeField] private List<GameObject> PlayerStack


    public float playerGold = 100f;

    private bool once = false;
    
    void Start()
    {
        DOTween.Init();
        GameEventHandler.current.OnUpgradeTriggerEnter += playerGrapeUpgrade;
        //GameEventHandler.current.OnPlayerGathering += OnPlayerGathering;
    }

    // private void OnPlayerGathering()
    // {
    //     throw new System.NotImplementedException();
    // }

    private void playerGrapeUpgrade()
    {
        var gold = Mathf.Clamp(playerGold, 0, 300);
        playerGold = gold;
        playerGold -= 10 * Time.deltaTime;
        once = true;

    }

}
