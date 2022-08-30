using System;
using DG.Tweening;
using DG.Tweening.Core;
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
                    var upgradetextint = 50.0f;

                    upgradetextint = Mathf.Lerp(50.0f, 0, 50);
                    
                    
                    upgradeRequire.text = upgradetextint.ToString();
                }
                
                if (!once)
                {
                    fillImage.DOFillAmount(1, 5);
                    once = true;
                }
                
                //var clamp = Mathf.Clamp(Int32.Parse(upgradeRequire.text), 0,500);
                //upgradeRequire.text = clamp.ToString();

                //var upgradetextint = float.Parse(upgradeRequire.text);

                //upgradetextint -= 1.0f * Time.deltaTime;
                //upgradeRequire.text = upgradetextint.ToString();


                //GameEventHandler.current.grapeUpgradeTriggerEnter();
            }
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
