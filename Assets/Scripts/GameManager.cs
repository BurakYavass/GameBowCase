using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    [SerializeField] private UiManager _uiManager;
    private Tween moneyTween;

    public float playerGold = 100;
    
    public static float playerSpeed = 12.0f;
    
    public static int playerMaxStack = 5;

    public static readonly float UpgradeDuration = 2.0f;
    
    public bool speedMax;
    public bool stackMax;

    private int speedCounter;
    private int stackCounter;

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
        Application.targetFrameRate = 60;
        DOTween.Init();
    }
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
    }

    private void PlayerMoneyDecrease(float value)
    {
        playerGold = Mathf.Clamp(playerGold-value, 0, 5000);
    }

    public void PlayerMoneyIncrease(float money,Vector3 customerPos)
    {
        //playerGold + money;
        money = Mathf.Clamp(money, 0, 10);
        playerGold  += money;
        _uiManager.EarningMoney(customerPos);
    }

    public void PlayerSpeedIncrease()
    {
        if (speedCounter <6 && playerGold >=100)
        {
            _uiManager.SpendMoney();
            speedCounter += 1;
            float mines = 100.0f;
            playerGold = Mathf.Clamp(playerGold - mines, 0, 5000);
            playerSpeed += 1.0f;
        }
        else if(stackCounter==6)
        {
            speedMax = true;
        }
    }

    public void PlayerStackIncrease()
    {
        if (stackCounter < 10 && playerGold >=100)
        {
            _uiManager.SpendMoney();
            stackCounter += 1;
            float mines = 100.0f;
            playerGold = Mathf.Clamp(playerGold - mines, 0, 5000);
            playerMaxStack += 1;
        }
        else if(stackCounter==10)
        {
            stackMax = true;
        }
    }
    
}
