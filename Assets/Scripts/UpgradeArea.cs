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

    [SerializeField] private Image fillImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;

    [SerializeField] private float requireMoney = 50.0f;
    
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
                upgradeRequire.text = (requireMoney = requireMoney * 3).ToString("0");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeObject), upgradeObject, null);
        }
    }

    private void Update()
    {
        if (gameManager.playerGold < requireMoney)
        {
            upgradeRequire.color = Color.red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desk State
            if (upgradeObject == UpgradeObject.UpgradeDesk && gameManager.playerGold >= requireMoney)
            {
                if (fillImage.fillAmount < 1.0f)
                {
                    var gold = Mathf.Clamp(requireMoney, 0, 300);
                    requireMoney = gold;
                    requireMoney -= 10.0f * Time.deltaTime;
                    upgradeRequire.text = requireMoney.ToString("0");
                    
                    fillImage.DOPlay();
                    if (!once)
                    {
                        fillImage.DOFillAmount(1, 5);
                        once = true;
                    }
                    GameEventHandler.current.GrapeUpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount >= .9f)
                {
                    activatedGameObject.SetActive(true);
                    if (once)
                    {
                        activatedGameObject.transform.DOShakeScale(.5f).SetEase(Ease.OutBounce);
                        once = false;
                    }
                
                    deactivatedObject.SetActive(false);
                }
                Debug.Log("Desk");
            }
            
            // Tree State
            else if (upgradeObject == UpgradeObject.UpgradeTree && gameManager.playerGold >= requireMoney)
            {
                if (fillImage.fillAmount < 1)
                {
                    var gold = Mathf.Clamp(requireMoney, 0, 300);
                    requireMoney = gold;
                    requireMoney -= 10.0f * Time.deltaTime;
                    upgradeRequire.text = requireMoney.ToString("0");
                    
                    fillImage.DOPlay();
                    if (!once)
                    {
                        //fillImage.fillAmount += .1f * Time.deltaTime;
                        fillImage.DOFillAmount(1, 2);
                        once = true;
                    }
                    GameEventHandler.current.GrapeUpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount == 1)
                {
                    activatedGameObject.SetActive(true);
                    if (once)
                    {
                        if (objectAnimation != null)
                            objectAnimation.Play();
                        
                        once = false;
                    }
                
                    deactivatedObject.SetActive(false);
                }
                Debug.Log("Tree");
            }
            
            // Smash State
            else if (upgradeObject == UpgradeObject.UpgradeSmash && gameManager.playerGold >= requireMoney)
            {
                if (fillImage.fillAmount < 1)
                {
                    var gold = Mathf.Clamp(requireMoney, 0, 300);
                    requireMoney = gold;
                    requireMoney -= 10.0f * Time.deltaTime;
                    upgradeRequire.text = requireMoney.ToString("0");
                    
                    fillImage.DOPlay();
                    if (!once)
                    {
                        //fillImage.fillAmount += .1f * Time.deltaTime;
                        fillImage.DOFillAmount(1, 10);
                        once = true;
                    }
                    GameEventHandler.current.GrapeUpgradeTriggerEnter();
                }
                else if (fillImage.fillAmount == 1)
                {
                    activatedGameObject.SetActive(true);
                    if (once)
                    {
                        if (objectAnimation != null)
                            objectAnimation.Play();
                        
                        once = false;
                    }
                
                    deactivatedObject.SetActive(false);
                }
                Debug.Log("Smash");
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
    
    
    //StartCoroutine(PayResourcesDuringBuild(100, 5.0f));
    // IEnumerator PayResourcesDuringBuild(int payAmount, float buildTime)
    // {
    //     int currentAmount = 0;
    //     int subtract = 0;
    //     float currentLerpTime = 0f;
    //     int alreadySubtracted = 0;
    //     while (payAmount != currentAmount)
    //     {
    //         currentLerpTime += Time.deltaTime;
    //         if (currentLerpTime > buildTime)
    //         {
    //             currentLerpTime = buildTime;
    //         }
    //         float t = currentLerpTime / buildTime;
    //         currentAmount = (int)Mathf.Lerp(0, payAmount, t);
    //         subtract = currentAmount - alreadySubtracted;
    //         totalResources -= subtract;
    //         alreadySubtracted += subtract;
    //         yield return null;
    //     }
    // }
}
