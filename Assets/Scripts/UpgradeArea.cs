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

    public UpgradeObject upgradeObject;

    private bool once = false;
    private bool filling = false;

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

    private void Upgrade(UpgradeObject @object)
    {
        upgradeObject = @object;
        
        switch (@object)
        {
            case UpgradeObject.UpgradeDesk:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
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
        if (other.CompareTag("Player") && gameManager.playerGold >= requireMoney)
        {
            var gold = Mathf.Clamp(requireMoney, 0, 300);
            requireMoney = gold;
            requireMoney -= 10.0f * Time.deltaTime;
            upgradeRequire.text = requireMoney.ToString("0");

            if (fillImage.fillAmount < 1)
            {
                fillImage.DOPlay();
                if (!once)
                {
                    //fillImage.fillAmount += .1f * Time.deltaTime;
                    fillImage.DOFillAmount(1, 5);
                    once = true;
                }
                GameEventHandler.current.GrapeUpgradeTriggerEnter();
            }
            else if (fillImage.fillAmount == 1)
            {
                filling = true;
                activatedGameObject.SetActive(true);
                if (once)
                {
                    activatedGameObject.transform.DOShakeScale(.5f).SetEase(Ease.OutBounce);
                    //activatedGameObject.transform.DOScale(1.0f, 1).SetEase(Ease.OutBounce);
                    once = false;
                }
                
                deactivatedObject.SetActive(false);
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
