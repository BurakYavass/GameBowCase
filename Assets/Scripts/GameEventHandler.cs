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

    public event Action OnPlayerGrapeDropping;

    public event Action PlayerGrapeStackMax;

    public void GrapeStackMax()
    {
        if (PlayerGrapeStackMax != null)
        {
            PlayerGrapeStackMax.Invoke();
        }
    }

    public void PlayerGrapeDropping()
    {
        if (OnPlayerGrapeDropping != null)
        {
            OnPlayerGrapeDropping.Invoke();
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
