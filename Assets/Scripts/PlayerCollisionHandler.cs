using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Basket") && !grapeStackList.grapeStackMax)
    //     {
    //         other.gameObject.GetComponent<Collider>().isTrigger = false;
    //         grapeStackList.basketList.Add(other.gameObject.transform);
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Basket") && !grapeStackList.grapeStackMax)
        {
            collision.collider.isTrigger = true;
            grapeStackList.basketList.Add(collision.gameObject.transform);
        }
    }
}
