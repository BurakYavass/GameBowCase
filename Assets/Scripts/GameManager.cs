using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private Transform customerSpawnPoint;

    public List<AgentAI> customerList;

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
        customerPrefab = Instantiate(customerPrefab,customerSpawnPoint.position,customerPrefab.transform.rotation)as GameObject;
        customerList.Add(customerPrefab.GetComponent<AgentAI>());
    }
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
        GameEventHandler.current.ActiveEmptyDesk -= AgentCreator;
    }

    private void AgentCreator(Vector3 emptyDesk)
    {
        // customerPrefab = Instantiate(customerPrefab,customerSpawnPoint.position,customerPrefab.transform.rotation)as GameObject;
        // customerList.Add(customerPrefab.GetComponent<AgentAI>());
        for (var i = 0; i < customerList.Count; i++)
        {
            if (customerList.Count >0)
            {
                customerList[i].deskPoint = emptyDesk;
                DeskArea.current.Desks[i].deskState = DeskCheck.DeskState.Full;
                return;
            }
        }
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
