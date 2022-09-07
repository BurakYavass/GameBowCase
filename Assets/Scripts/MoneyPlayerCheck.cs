using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPlayerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
        }
    }
}
