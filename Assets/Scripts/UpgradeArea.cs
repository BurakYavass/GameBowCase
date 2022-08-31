using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI upgradeRequire;

    public UpgradeState upgradeState;

    private bool _collidePlayer;

    private bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        Upgrade(UpgradeState.UpgradeOne);
        DOTween.Init();
    }

    private void Upgrade(UpgradeState state)
    {
        upgradeState = state;
        
        switch (state)
        {
            case UpgradeState.UpgradeOne:
                upgradeRequire.text = 50.ToString();
                break;
            case UpgradeState.UpgradeTwo:
                upgradeRequire.text = 100.ToString();
                break;
            case UpgradeState.UpgradeThree:
                upgradeRequire.text = 150.ToString();
                break;
            case UpgradeState.UpgradeFour:
                upgradeRequire.text = 200.ToString();
                break;
            case UpgradeState.UpgradeFive:
                upgradeRequire.text = 250.ToString();
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
            _collidePlayer = true;

            if (fillImage.fillAmount < 1)
            {
                if (upgradeState == UpgradeState.UpgradeOne)
                { 
                    var upgradetextint = Int32.Parse(upgradeRequire.text);

                    upgradetextint -= 2 * 2;
                    
                    upgradetextint = Mathf.Clamp(upgradetextint, 0, 250);
                    
                    upgradeRequire.text = upgradetextint.ToString("D");
                }

                //fillImage.fillAmount += 1.0f * Time.deltaTime;
                fillImage.DOPlay();
                
                if (!once)
                {
                    fillImage.DOFillAmount(1, 10);
                    once = true;
                }
                
                GameEventHandler.current.grapeUpgradeTriggerEnter();
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

    private void Update()
    {
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
}
