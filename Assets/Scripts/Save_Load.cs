using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_Load : MonoBehaviour
{
    public static Save_Load current;

    private void Awake()
    {
        if (current==null)
        {
            current = this;
        }
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
