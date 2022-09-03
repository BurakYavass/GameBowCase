using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : ObjectID
{
    [Header("Selected Object")]
    public UpgradeObject upgradeObject;
    
    private GameManager _gameManager;
    private ObjectID _otherId;
    private Tween _requireMoneyTween;

    [SerializeField] private GameObject activatedGameObject;
    [SerializeField] private GameObject deactivatedObject;
    
    [SerializeField] private GrapeSmashArea grapeSmashArea;

    [SerializeField] private Image fillImage;
    [SerializeField] private Image notEnoughImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;

    [SerializeField] private float requireMoney = 10;

    [Header("Object Animation")]
    [SerializeField] private Animation objectAnimation;
    
    private float _previousMoney;
    
    private bool _warningAnim = false;
    private bool _once = false;

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

        if (Type == ObjectType.Desk)
        {
            upgradeRequire.text = (requireMoney  *= 1).ToString("0");
        }

        DOTween.Init();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Upgrade(UpgradeObject state)
    {
        upgradeObject = state;
        
        switch (state)
        {
            case UpgradeObject.UpgradeDesk:
                upgradeRequire.text = (requireMoney = requireMoney * 1).ToString("0");
                //totalMoney = (int)requireMoney;
                break;
            case UpgradeObject.UpgradeTree:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                //totalMoney = (int)requireMoney;
                break;
            case UpgradeObject.UpgradeSmash:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                //totalMoney = (int)requireMoney;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeObject), upgradeObject, null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        
        if(_otherId.Type == ObjectType.Player && _gameManager.playerGold > 0.1f)
        {
            MoneyDecrease_ObjectControl();
        }
        else if (_otherId.Type == ObjectType.Player &&_gameManager.playerGold == 0)
        {
            fillImage.DOPause();
            _requireMoneyTween.Pause();
            notEnoughImage.enabled = true;
            if (!_warningAnim)
            {
                _warningAnim = true;
                notEnoughImage.DOColor(Color.white, .2f)
                    .OnComplete((() => notEnoughImage.DOColor(Color.red, 0.2f)));
            }
        }
    }

    private void MoneyDecrease_ObjectControl()
    {
        if (_gameManager.playerGold > 0.1f)
        {
            fillImage.DOPlay();
            _requireMoneyTween.Play();
            if (!_once) 
            {
                _once = true;
                _previousMoney = requireMoney;
                _requireMoneyTween = DOTween.To((() => requireMoney), x => requireMoney = x, 0, GameManager.UpgradeDuration)
                .OnUpdate((() =>
                {
                    var money = (int) _previousMoney - (int) requireMoney;
                    if (money > 0)
                    {
                        _previousMoney = requireMoney;
                        GameEventHandler.current.UpgradeTriggerEnter(money);
                    }
                }))
                .OnComplete(() =>
                {
                    //deactivatedObject.transform.DOScaleY(0.1f,0.5f)
                        deactivatedObject.transform.DOShakeScale(.5f,0.5f)
                            .OnUpdate((() => deactivatedObject.SetActive(false)))
                            .OnComplete(() => 
                            {
                                activatedGameObject.SetActive(true);
                                GameEventHandler.current.ObjectActivator();
                                    if (objectAnimation != null)
                                    {
                                        objectAnimation.Play();
                                    }
                                    else
                                    {
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
            _requireMoneyTween.Pause();
            notEnoughImage.enabled = false;
            _warningAnim = false;
            notEnoughImage.DOKill();
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
