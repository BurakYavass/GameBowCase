using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSmashArea : MonoBehaviour
{
    public List<GameObject> GrapeSmashPoint = new List<GameObject>();

    [SerializeField] private PlayerGrapeStackList playerGrapeStackList;
    [SerializeField] private SmashBowlController smashBowlController;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerGrapeStackList.basketList.Count >1)
        {
            foreach (var smashPoint in GrapeSmashPoint)
            {
                if (smashPoint.activeInHierarchy)
                {
                    //smashPoint.GetComponent<SmashBowlController>();
                    smashBowlController = smashPoint.GetComponent<SmashBowlController>();
                    smashBowlController.grapeCounter += 1;
                }
            }
            GameEventHandler.current.PlayerGrapeDropping();
        }
        
    }
}
