using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private PlayerController playerController;

    public int playerGold = 100;
    // Start is called before the first frame update
    void Start()
    {
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
