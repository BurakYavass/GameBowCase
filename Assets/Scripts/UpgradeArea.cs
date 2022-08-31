using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private GameObject activeGameObject;
    [SerializeField] private Image fillImage;
    [SerializeField] public TextMeshProUGUI upgradeRequire;
    [SerializeField] private float requireMoney = 50.0f;

    public UpgradeState upgradeState;

    private bool once = false;
    private bool filling = false;

    // Start is called before the first frame update
    void Start()
    {
        if (upgradeState == UpgradeState.UpgradeOne)
        {
            Upgrade(UpgradeState.UpgradeOne);
        }
        else if (upgradeState == UpgradeState.UpgradeTwo)
        {
            Upgrade(UpgradeState.UpgradeTwo);
        }
        else if (upgradeState == UpgradeState.UpgradeThree)
        {
            Upgrade(UpgradeState.UpgradeThree);
        }
        else if (upgradeState == UpgradeState.UpgradeFour)
        {
            Upgrade(UpgradeState.UpgradeFour);
        }
        else if (upgradeState == UpgradeState.UpgradeFive)
        {
            Upgrade(UpgradeState.UpgradeFive);
        }
        else if (upgradeState == UpgradeState.UpgradeMax)
        {
            Upgrade(UpgradeState.UpgradeMax);
        }
        
        DOTween.Init();
    }

    private void Upgrade(UpgradeState state)
    {
        upgradeState = state;
        
        switch (state)
        {
            case UpgradeState.UpgradeOne:
                upgradeRequire.text = requireMoney.ToString("0");
                break;
            case UpgradeState.UpgradeTwo:
                upgradeRequire.text = (requireMoney = requireMoney * 2).ToString("0");
                break;
            case UpgradeState.UpgradeThree:
                upgradeRequire.text = (requireMoney = requireMoney * 3).ToString("0");
                break;
            case UpgradeState.UpgradeFour:
                upgradeRequire.text = (requireMoney = requireMoney * 4).ToString("0");
                break;
            case UpgradeState.UpgradeFive:
                upgradeRequire.text = (requireMoney = requireMoney * 5).ToString("0");
                break;
            case UpgradeState.UpgradeMax:
                upgradeRequire.text = "MAX";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(upgradeState), upgradeState, null);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
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
                    //StartCoroutine(PayResourcesDuringBuild(100, 5.0f));
                    //fillImage.fillAmount += .1f * Time.deltaTime;
                    fillImage.DOFillAmount(1, 5);
                    once = true;
                }
                GameEventHandler.current.GrapeUpgradeTriggerEnter();
            }
            else if (fillImage.fillAmount == 1)
            {
                filling = true;
                activeGameObject.SetActive(true);
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

    public enum UpgradeState
    {
        UpgradeOne,
        UpgradeTwo,
        UpgradeThree,
        UpgradeFour,
        UpgradeFive,
        UpgradeMax,
    }
    
    
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
