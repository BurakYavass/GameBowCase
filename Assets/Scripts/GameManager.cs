using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private PlayerController playerController;

    public float playerGold = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        GameEventHandler.current.OngrapeUpgradeTriggerEnter += playerGrapeUpgrade;
    }

    private void playerGrapeUpgrade()
    {
        var gold = Mathf.Clamp(playerGold, 0, 300);
        playerGold = gold;
        playerGold -= 10.0f * Time.deltaTime;
    }

}
