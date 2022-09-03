using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : ObjectID
{
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

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        if (Type == ObjectType.Desk)
        {
            upgradeRequire.text = (requireMoney  *= 2).ToString("0");
        }
        if (Type == ObjectType.Grape)
        {
            upgradeRequire.text = (requireMoney  *= 2).ToString("0");
        }
        if (Type == ObjectType.GrapeSmash)
        {
            upgradeRequire.text = (requireMoney  *= 2).ToString("0");
        }
        DOTween.Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        //var gold = _gameManager.playerGold - requireMoney;
        if(_otherId.Type == ObjectType.Player && _gameManager.playerGold > 0 )
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
        fillImage.DOPlay();
        _requireMoneyTween.Play();
        if (!_once) 
        {
            _once = true;
            _previousMoney = requireMoney;
            _requireMoneyTween = DOTween.To((() => requireMoney), x => requireMoney = x, 0, GameManager.UpgradeDuration)
            .OnUpdate((() =>
            {
                //var money = (int) _previousMoney - (int) requireMoney;
                var money =  _previousMoney - requireMoney;
                if (money > 0)
                {
                    _previousMoney = requireMoney;
                    GameEventHandler.current.UpgradeTriggerEnter(money);
                }
            }))
            .OnComplete(() =>
            {
                //deactivatedObject.transform.DOShakeScale(.5f,0.5f)
                deactivatedObject.transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.5f).SetEase(Ease.InBounce)
                    .OnComplete(() =>
                    {
                        deactivatedObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
                        deactivatedObject.SetActive(false);
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
                                    if (Type == ObjectType.GrapeSmash)
                                    {
                                        grapeSmashArea.GrapeSmashPoint.Add(activatedGameObject.GetComponent<SmashBowlController>());
                                    }
                                });
                        } 
                    });
            });
                    
        fillImage.DOFillAmount(1, GameManager.UpgradeDuration); 
        }
        //upgradeRequire.text = Mathf.FloorToInt(requireMoney).ToString("0") ;
        upgradeRequire.text = requireMoney.ToString("0") ;
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
}
