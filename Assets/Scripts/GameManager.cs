using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private UiManager _uiManager;

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
        //Application.targetFrameRate = 60;
        DOTween.Init();
    }
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
        GameEventHandler.current.ActiveEmptyDesk -= AgentCreator;
    }
    
    private void AgentCreator(Transform emptyDesk,Vector3 bos)
    {
        var clone = Instantiate(customerPrefab,customerSpawnPoint.position,customerPrefab.transform.rotation)as GameObject;
        //customerList.Add(clone);
        var agent = clone.GetComponent<AgentAI>();
        agent.destinationPoint = emptyDesk.transform;
        agent.forward = bos;
    }

    private void PlayerMoneyDecrease(float value)
    {
        playerGold = Mathf.Clamp(playerGold-value, 0, 5000);
    }

    public void PlayerMoneyIncrease(float money)
    {
        //playerGold + money;
        money = Mathf.Clamp(money, 0, 10);
        playerGold  += money;
        //_uiManager.MoneyInstant();
        
    }
    
}
