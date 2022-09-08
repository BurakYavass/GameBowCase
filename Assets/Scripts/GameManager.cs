using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    private UiManager _uiManager;
    [SerializeField] private GameObject waiter;
    [SerializeField] private GameObject barmen;

    public float playerGold = 100;

    public static float playerSpeed = 12.0f;

    public static int playerMaxStack = 5;

    public static readonly float UpgradeDuration = 2.0f;
    
    private Tween moneyTween;
    public bool speedMax;
    public bool stackMax;
    public bool waiterActive = false;
    public bool barmenActive = false;
    public bool waiterUnlock = false;

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
        if (_uiManager==null)
        {
            _uiManager= UiManager.current;
        }
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
        playerGold = Mathf.Clamp(playerGold + money, 0, 5000);
        _uiManager.goldText.transform.DOShakeScale(0.3f, Vector3.up,1);
        _uiManager.EarningMoney(customerPos);
    }

    public void PlayerSpeedIncrease()
    {
        if (speedCounter <6 && playerGold >=100)
        {
            _uiManager.SpendMoney();
            speedCounter += 1;
            playerGold = Mathf.Clamp(playerGold - 100.0f, 0, 5000);
            PlayerAnimationHandler.current.ParticlePlay();
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
            playerGold = Mathf.Clamp(playerGold - 100.0f, 0, 5000);
            PlayerAnimationHandler.current.ParticlePlay();
            playerMaxStack += 1;
        }
        else if(stackCounter==10)
        {
            stackMax = true;
        }
    }

    public void BarmenHire()
    {
        if (playerGold>=100)
        {
            _uiManager.SpendMoney();
            playerGold = Mathf.Clamp(playerGold - 100.0f, 0, 5000);
            barmenActive = true;
            barmen.SetActive(true);
            waiterUnlock = true;
        }
    }

    public void WaiterHire()
    {
        if (playerGold>=100 && barmenActive)
        {
            _uiManager.SpendMoney();
            playerGold = Mathf.Clamp(playerGold - 100.0f, 0, 5000);
            waiterActive = true;
            waiter.SetActive(true);
        }
    }
    
}
