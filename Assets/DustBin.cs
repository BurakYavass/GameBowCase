using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class DustBin : ObjectID
{
    public static DustBin current;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image fillImageBin;
    private bool tweenBool = false;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    public void FillImage(float duration)
    {
        if (!tweenBool)
        {
            fillImage.DOFillAmount(1, duration).OnComplete((() => fillImage.fillAmount = 0));
            fillImageBin.DOFillAmount(1, duration).OnComplete((() => fillImageBin.fillAmount = 0));
            tweenBool = false;

        }
    }
}
