using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : ObjectID
{
    private PlayerController _otherId;
    private Tween _requireMoneyTween;

    public event Action Activator;
    [SerializeField] private GameObject activatedGameObject;
    [SerializeField] private GameObject deactivatedObject;

    [SerializeField] private Image fillImage;
    [SerializeField] private Image notEnoughImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;

    [SerializeField] private float requireMoney = 10;

    [Header("Object Animation")]
    [SerializeField] private Animation objectAnimation;
    
    private float _previousMoney;
    
    private bool _warningAnim = false;
    private bool _once = false;
    

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
            _otherId = other.gameObject.GetComponent<PlayerController>();
        //var gold = _gameManager.playerGold - requireMoney;
        if(_otherId != null && _otherId.Type == ObjectType.Player && GameManager.current.playerGold > 0 )
        {
            MoneyDecrease_ObjectControl();
        }
        else if (_otherId != null && _otherId.Type == ObjectType.Player &&GameManager.current.playerGold == 0)
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
                // ekranın sağ üstünde para akıcak
                var money =  _previousMoney - requireMoney;
                if (money > 0)
                {
                    _previousMoney = requireMoney;
                    GameEventHandler.current.UpgradeTriggerEnter(money);
                }
            }))
            .OnComplete(() =>
            {
                if (requireMoney == 0)
                {
                    deactivatedObject.transform.DOPunchScale(new Vector3(0.5f,2,0.5f),0.5f).SetEase(Ease.InElastic)
                        .OnComplete(() =>
                        {
                            deactivatedObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f)
                                .OnComplete((() =>deactivatedObject.SetActive(false) ));
                            activatedGameObject.SetActive(true);
                            //GameEventHandler.current.ObjectActivator();
                            Activator?.Invoke();
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
                                            GrapeSmashArea.current.grapeSmashPoint.Add(activatedGameObject.GetComponent<SmashBowlController>());
                                        }
                                        else if(Type == ObjectType.Desk)
                                        {
                                            DeskArea.current.Desks.AddRange(activatedGameObject.GetComponentsInChildren<ChairCheck>());
                                        }
                                    });
                            }
                        });
                }
            });
                    
        fillImage.DOFillAmount(1, GameManager.UpgradeDuration); 
        }
        //upgradeRequire.text = Mathf.FloorToInt(requireMoney).ToString("0") ;
        upgradeRequire.text = requireMoney.ToString("0") ;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<PlayerController>();
        
        if (_otherId != null && _otherId.Type == ObjectType.Player)
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
