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
    public event Action<float> OnUpgradeTriggerEnter;

    public event Action OnUpgradeTriggerExit;

    public event Action<int> OnPlayerGrapeDropping;

    public event Action OnPlayerBarrelDropping;

    public event Action BarrelGenerate;

    public event Action<Transform,Vector3> ActiveEmptyDesk;
    

    public void EmptyDesk(Transform desk, Vector3 deskRotation)
    {
        ActiveEmptyDesk?.Invoke(desk,deskRotation);
    }

    public void BarrelDropping()
    {
        OnPlayerBarrelDropping?.Invoke();
    }

    public void BarrelGenerator()
    {
        BarrelGenerate?.Invoke();
    }
    
    public void PlayerGrapeDropping(int value)
    {
        OnPlayerGrapeDropping?.Invoke(value);
    }

    public void UpgradeTriggerEnter(float value)
    {
        OnUpgradeTriggerEnter?.Invoke(value);
    }

    public void UpgradeTriggerExit()
    {
        OnUpgradeTriggerExit?.Invoke();
    }
}
