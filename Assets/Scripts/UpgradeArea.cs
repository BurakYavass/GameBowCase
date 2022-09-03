using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : ObjectID
{
    public event Action Activated;
    private GameManager gameManager;
    private ObjectID _otherId;
    [SerializeField] private GameObject activatedGameObject;
    [SerializeField] private GameObject deactivatedObject;
    
    [SerializeField] private GrapeSmashArea grapeSmashArea;

    [SerializeField] private Image fillImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;

    [SerializeField] private float requireMoney = 10;
    private int TotalMoney = 10;
    private float previousMoney;
    
    [Header("Object Animation")]
    [SerializeField] private Animation objectAnimation;

    [Header("Selected Object")]
    public UpgradeObject upgradeObject;

    private Tween requireMoneyTween;

    private bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        if (upgradeObject == UpgradeObject.UpgradeDesk)
        {
            Upgrade(UpgradeObject.UpgradeDesk);
        }
        else if (upgradeObject == UpgradeObject.UpgradeTree)
        {
            Upgrade(UpgradeObject.UpgradeTree);
        }
        else if (upgradeObject == UpgradeObject.UpgradeSmash)
        {
            Upgrade(UpgradeObject.UpgradeSmash);
        }

        DOTween.Init();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Upgrade(UpgradeObject state)
    {
        upgradeObject = state;
        
        switch (state)
        {
            case UpgradeObject.UpgradeDesk:
                upgradeRequire.text = (requireMoney = requireMoney * 1).ToString("0");
                TotalMoney = (int)requireMoney;
                break;
            case UpgradeObject.UpgradeTree:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                TotalMoney = (int)requireMoney;
                break;
            case UpgradeObject.UpgradeSmash:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                TotalMoney = (int)requireMoney;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeObject), upgradeObject, null);
        }
    }

    private void Update()
    {
        if (gameManager.playerGold < (int)requireMoney)
        {
            upgradeRequire.color = Color.red;
        }
        else
        {
            upgradeRequire.color = Color.white;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        
        if(_otherId.Type == ObjectType.Player && gameManager.playerGold >= (int)requireMoney)
        {
            MoneyDecrease_ObjectControl();
        }
    }

    private void MoneyDecrease_ObjectControl()
    {
        fillImage.DOPlay();
        requireMoneyTween.Play();
        
        if (!once)
        {
            once = true;
            previousMoney = requireMoney;
            requireMoneyTween = DOTween.To((() => requireMoney), x => requireMoney = x, 0, GameManager.UpgradeDuration)
                .OnUpdate((() =>
                {
                    var money = (int) previousMoney - (int) requireMoney;
                    if (money > 0)
                    {
                        previousMoney = requireMoney;
                        GameEventHandler.current.UpgradeTriggerEnter(money);
                    }
                }))
                .OnComplete(() =>
                {
                    deactivatedObject.transform.DOScaleY(0.1f,0.5f)
                        //deactivatedObject.transform.DOShakeScale(.5f,0.5f)
                    .OnComplete(() => deactivatedObject.transform.DOScale(Vector3.zero, 0.5f))
                    .OnComplete(() => { 
                        deactivatedObject.SetActive(false);
                        activatedGameObject.SetActive(true);
                    if (objectAnimation != null)
                        objectAnimation.Play();
                    else
                    {
                        GameEventHandler.current.ObjectActivator();
                        activatedGameObject.transform.DOShakeScale(.5f).SetEase(Ease.OutBounce)
                            .OnComplete(() => {
                                if (upgradeObject == UpgradeObject.UpgradeSmash)
                                {
                                    grapeSmashArea.GrapeSmashPoint.Add(activatedGameObject.GetComponent<SmashBowlController>());
                                }
                            });
                    } 
                    });
                });
                        
            fillImage.DOFillAmount(1, GameManager.UpgradeDuration);
        }
        upgradeRequire.text = Mathf.FloorToInt(requireMoney).ToString("0") ;
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        
        if (_otherId.Type == ObjectType.Player)
        {
            fillImage.DOPause();
            requireMoneyTween.Pause();
            GameEventHandler.current.UpgradeTriggerExit();
        }
    }

    public enum UpgradeObject
    {
        UpgradeTree,
        UpgradeDesk,
        UpgradeSmash,
    }
}
