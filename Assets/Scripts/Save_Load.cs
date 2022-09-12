using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_Load : MonoBehaviour
{
    public static Save_Load current;
    [SerializeField] private GrapeSmashArea _grapeSmashArea;
    public int deger;

    private void Awake()
    {
        if (current==null)
        {
            current = this;
        }
    }
    private void Start()
    {
        GameEventHandler.current.SaveItem += OnSaveItem;
        
        if (PlayerPrefs.GetInt("UpgradeItem") >=1)
        {
            _grapeSmashArea.UnlockItem(1);
        }
    }

    private void OnEnable()
    {
        // if (PlayerPrefs.GetInt("UpgradeItem") >=1)
        // {
        //     _grapeSmashArea.UnlockItem(deger);
        // }
    }

    private void OnDisable()
    {
        GameEventHandler.current.SaveItem -= OnSaveItem;
    }
    
    private void OnSaveItem(int obj)
    {
        deger += obj;
        //PlayerPrefs.SetInt("deger",deger);
        PlayerPrefs.SetInt("UpgradeItem",deger);
    }

    public float PlayerGold
    {
        get => GameManager.current.playerGold;
        set
        {
            GameManager.current.playerGold = value;
            PlayerPrefs.SetFloat("playerGold",GameManager.current.playerGold);
        }
    }
}
