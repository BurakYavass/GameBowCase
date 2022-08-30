using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private Image fillImage;

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
            fillImage.fillAmount += 1.0f  * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (fillImage.fillAmount >= 1.0f)
        {
            
        }
    }
}
