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
    
    public event Action OnUpgradeTriggerEnter;

    public event Action OnPlayerGathering;

    public event Action PlayerGrapeStackMax;

    public void GrapeStackMax()
    {
        if (PlayerGrapeStackMax != null)
        {
            PlayerGrapeStackMax.Invoke();
        }
    }

    public void PlayerGathering()
    {
        if (OnPlayerGathering != null)
        {
            OnPlayerGathering.Invoke();
        }
    }

    public void UpgradeTriggerEnter()
    {
        if (OnUpgradeTriggerEnter != null)
        {
            OnUpgradeTriggerEnter.Invoke();
        }
    }
}
