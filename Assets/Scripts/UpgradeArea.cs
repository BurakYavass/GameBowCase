using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject activatedGameObject;
    [SerializeField] private GameObject deactivatedObject;
    
    [SerializeField] private GrapeSmashArea grapeSmashArea;

    [SerializeField] private Image fillImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;

    [SerializeField] private float requireMoney = 10;
    
    [Header("Object Animation")]
    [SerializeField] private Animation objectAnimation;

    [Header("Selected Object")]
    public UpgradeObject upgradeObject;

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
                break;
            case UpgradeObject.UpgradeTree:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                break;
            case UpgradeObject.UpgradeSmash:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeObject), upgradeObject, null);
        }
    }

    private void Update()
    {
        if (gameManager.playerGold < requireMoney- 0.9f)
        {
            upgradeRequire.color = Color.red;
        }
        else if(gameManager.playerGold >= requireMoney)
        {
            upgradeRequire.color = Color.white;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.playerGold >= requireMoney)
        {
            // Upgrade alani uzerinde yazan sayiyi duzenliyoruz
            var gold = Mathf.Clamp(requireMoney, 0, 300);
            requireMoney = gold;
            requireMoney -= 10.0f * Time.deltaTime;
            upgradeRequire.text = requireMoney.ToString("0");
            
            // Desk State
            if (upgradeObject == UpgradeObject.UpgradeDesk)
            {
                if (fillImage.fillAmount < 1.0f)
                {
                    fillImage.DOPlay();
                    if (!once)
                    {
                        var counter = requireMoney;
                        var duration = 1.0f;
                        if (counter < 10)
                        {
                            duration = counter;
                        }
                        else if (counter > 10)
                        {
                            duration = counter / 10;
                        }
                        fillImage.DOFillAmount(1, duration);
                        once = true;
                    }
                    GameEventHandler.current.UpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount >= .9f)
                {
                    activatedGameObject.SetActive(true);
                    if (once)
                    {
                        activatedGameObject.transform.DOShakeScale(.5f).SetEase(Ease.OutBounce);
                        gameManager.playerGold = Mathf.FloorToInt(gameManager.playerGold += 0.5f);
                        once = false;
                    }
                    deactivatedObject.SetActive(false);
                }
            }
            
            // Tree State
            else if (upgradeObject == UpgradeObject.UpgradeTree)
            {
                if (fillImage.fillAmount < 1)
                {
                    fillImage.DOPlay();
                    if (!once)
                    {
                        var counter = requireMoney;
                        var duration = 1.0f;
                        if (counter < 10)
                        {
                            duration = counter;
                        }
                        else if (counter > 10)
                        {
                            duration = counter / 10;
                        }
                        fillImage.DOFillAmount(1, duration);
                        once = true;
                    }
                    GameEventHandler.current.UpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount >= .9f)
                {
                    activatedGameObject.SetActive(true);
                    if (once && objectAnimation != null)
                    {
                        objectAnimation.Play();
                        gameManager.playerGold = Mathf.FloorToInt(gameManager.playerGold += 0.5f);
                        once = false;
                    }
                    var treeGrape = GetComponent<GrapeSpawner>();
                    if (treeGrape != null)
                        treeGrape.active = true;

                    deactivatedObject.SetActive(false);
                }
            }
            
            // Smash State
            else if (upgradeObject == UpgradeObject.UpgradeSmash)
            {
                if (fillImage.fillAmount < 1)
                {
                    fillImage.DOPlay();
                    if (!once)
                    {
                        var counter = requireMoney;
                        var duration = 1.0f;
                        if (counter < 10)
                        {
                            duration = counter;
                        }
                        else if (counter > 10)
                        {
                            duration = counter / 10;
                        }
                        
                        fillImage.DOFillAmount(1, duration);
                        once = true;
                    }
                    GameEventHandler.current.UpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount >= .9f)
                {
                    gameManager.playerGold = Mathf.FloorToInt(gameManager.playerGold += 0.5f);
                    activatedGameObject.SetActive(true);
                    if (once && objectAnimation != null)
                    {
                        activatedGameObject.transform.DOShakeScale(.5f).SetEase(Ease.OutBounce)
                                                        .OnComplete((() => grapeSmashArea.GrapeSmashPoint.Add(activatedGameObject)));
                        //objectAnimation.Play();
                        once = false;
                    }
                    deactivatedObject.SetActive(false);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fillImage.DOPause();
        }
    }

    public enum UpgradeObject
    {
        UpgradeTree,
        UpgradeDesk,
        UpgradeSmash,
    }
}
