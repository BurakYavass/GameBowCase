using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket") && !grapeStackList.grapeStackMax)
        {
            grapeStackList.basketList.Add(other.gameObject);
        }
    }
}
