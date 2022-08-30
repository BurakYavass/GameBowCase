using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI UpgradeRequire;

    private bool _collidePlayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("UpgradeArea");
            _collidePlayer = true; 
            GameEventHandler.current.grapeUpgradeTriggerEnter();
            fillImage.fillAmount += 1.0f  * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (fillImage.fillAmount >= 1.0f)
        {
            
        }
    }

    enum MyEnum
    {
        UpgradeOne,
        UpgradeTwo,
        UpgradeThree,
        UpgradeFour,
        UpgradeFive,
    }
}
