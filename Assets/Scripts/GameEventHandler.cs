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
    public event Action<int> OnUpgradeTriggerEnter;

    public event Action OnUpgradeTriggerExit;

    public event Action<int> OnPlayerGrapeDropping;
    

    public event Action PlayerGrapeStackMax;

    public event Action BarrelGenerate;

    public event Action OnObjectActive;


    public void ObjectActivator()
    {
        OnObjectActive?.Invoke();
    }

    public void BarrelGenerator()
    {
        BarrelGenerate?.Invoke();
    }

    public void GrapeStackMax()
    {
        PlayerGrapeStackMax?.Invoke();
    }

    public void PlayerGrapeDropping(int value)
    {
        OnPlayerGrapeDropping?.Invoke(value);
    }

    public void UpgradeTriggerEnter(int value)
    {
        OnUpgradeTriggerEnter?.Invoke(value);
    }

    public void UpgradeTriggerExit()
    {
        OnUpgradeTriggerExit?.Invoke();
    }
}
