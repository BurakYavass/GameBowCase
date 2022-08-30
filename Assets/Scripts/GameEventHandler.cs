using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler current;

    private void Awake()
    {
        current = this;
    }

    public event Action ongrapeUpgradeTriggerEnter;

    public void grapeUpgradeTriggerEnter()
    {
        if (ongrapeUpgradeTriggerEnter != null)
        {
            ongrapeUpgradeTriggerEnter();
        }
    }
}
