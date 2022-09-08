using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    [SerializeField] private Save_Load _saveLoad;
    private UiManager _uiManager;
    [SerializeField] private GameObject waiter;
    [SerializeField] private GameObject barmen;
    public List<AgentAI> customerPoint = new List<AgentAI>();

    public float playerGold = 500;
    
    public static float playerSpeed = 14.0f;

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

        _saveLoad.PlayerGold = PlayerPrefs.GetFloat("playerGold", 500);
        
        GameEventHandler.current.OnUpgradeTriggerEnter += PlayerMoneyDecrease;
        GameEventHandler.current.CustomerServeWaiting += OnCustomerServeWaiting;
        Application.targetFrameRate = 60;
        
        DOTween.Init();
    }
    private void OnDestroy()
    {
        GameEventHandler.current.OnUpgradeTriggerEnter -= PlayerMoneyDecrease;
        GameEventHandler.current.CustomerServeWaiting -= OnCustomerServeWaiting;
    }

    private void OnCustomerServeWaiting(AgentAI customerObje)
    {
        customerPoint.Add(customerObje);
    }
    
    private void PlayerMoneyDecrease(float value)
    {
        _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold-value, 0, 5000);
    }

    public void PlayerMoneyIncrease(float money,Vector3 customerPos , AgentAI customer)
    {
        _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold + money, 0, 5000);
        _uiManager.goldText.transform.DOShakeScale(0.3f, Vector3.up,1);
        _uiManager.EarningMoney(customerPos);
        customerPoint.Remove(customer);
    }

    public void PlayerSpeedIncrease()
    {
        if (speedCounter <6 && _saveLoad.PlayerGold >=100)
        {
            _uiManager.SpendMoney();
            speedCounter += 1;
            _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold - 100.0f, 0, 5000);
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
        if (stackCounter < 10 && _saveLoad.PlayerGold >=100)
        {
            _uiManager.SpendMoney();
            stackCounter += 1;
            _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold - 100.0f, 0, 5000);
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
        if (_saveLoad.PlayerGold>=100)
        {
            _uiManager.SpendMoney();
            _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold - 100.0f, 0, 5000);
            barmenActive = true;
            barmen.SetActive(true);
            waiterUnlock = true;
        }
    }

    public void WaiterHire()
    {
        if (_saveLoad.PlayerGold>=100 && barmenActive)
        {
            _uiManager.SpendMoney();
            _saveLoad.PlayerGold = Mathf.Clamp(_saveLoad.PlayerGold - 100.0f, 0, 5000);
            waiterActive = true;
            waiter.SetActive(true);
        }
    }
    
}
