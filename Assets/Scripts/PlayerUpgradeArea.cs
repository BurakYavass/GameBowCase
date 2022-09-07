using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeArea : ObjectID
{
    public static PlayerUpgradeArea current;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image fillImage2;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fillImage.DOFillAmount(1, .5f);
            fillImage2.DOFillAmount(1, .5f);
            UiManager.current.UiActivator();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        fillImage.fillAmount = 0;
        fillImage2.fillAmount = 0;
    }
}
