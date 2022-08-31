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
    
    public event Action OngrapeUpgradeTriggerEnter;

    public void GrapeUpgradeTriggerEnter()
    {
        if (OngrapeUpgradeTriggerEnter != null)
        {
            OngrapeUpgradeTriggerEnter();
        }
    }
}
