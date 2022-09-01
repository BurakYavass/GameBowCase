using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Basket") && !grapeStackList.grapeStackMax)
        {
            collision.collider.isTrigger = true;
            grapeStackList.basketList.Add(collision.gameObject.transform);
        }
    }
}
