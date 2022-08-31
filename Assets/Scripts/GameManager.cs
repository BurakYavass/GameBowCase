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
        GameEventHandler.current.ongrapeUpgradeTriggerEnter += playerGrapeUpgrade;
    }

    private void playerGrapeUpgrade()
    {
        playerGold -= 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
