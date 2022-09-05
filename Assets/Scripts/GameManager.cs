using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private Transform customerSpawnPoint;

    public List<GameObject> customerList;

    // [SerializeField] private UiManager uiManager;
    // [SerializeField] private PlayerController playerController;
    //
    // [Header("Desk Upgrade Area List")]
    // public List<UpgradeArea> DeskUpgrades = new List<UpgradeArea>(7);

    public float playerGold = 100;

    public static readonly float UpgradeDuration = 2.0f;
    
    private Tween moneyTween;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    void Start()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter += PlayerMoneyDecrease;
        GameEventHandler.current.ActiveEmptyDesk += AgentCreator;
        Application.targetFrameRate = 60;
        DOTween.Init();
    }
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
        GameEventHandler.current.ActiveEmptyDesk -= AgentCreator;
    }
    
    private void AgentCreator(Vector3 emptyDesk,Vector3 bos)
    {
        var clone = Instantiate(customerPrefab,customerSpawnPoint.position,customerPrefab.transform.rotation)as GameObject;
        customerList.Add(customerPrefab);
        //customerPrefab.AddComponent<AgentAI>();
        var agent = clone.GetComponent<AgentAI>();
        agent.destinationPoint = emptyDesk;
        agent.forward = bos;
    }
    
    private void PlayerMoneyDecrease(float value)
    {
        playerGold = Mathf.Clamp(playerGold-value, 0, 5000);
    }
    

}
