using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSmashArea : ObjectID
{
    public List<SmashBowlController> GrapeSmashPoint;
    private ObjectID _objectID;

    [SerializeField] private PlayerGrapeStackList playerGrapeStackList;
    private SmashBowlController smashBowlController;

    private void OnTriggerStay(Collider other)
    {
        if (_objectID == null)
            _objectID = other.gameObject.GetComponent<ObjectID>();
        
        if (_objectID.Type == ObjectType.Player && playerGrapeStackList.basketList.Count >1)
        {
            // foreach (var smashPoint in GrapeSmashPoint)
            // {
            //     if (smashPoint.gameObject.activeInHierarchy && !smashPoint.working)
            //     {
            //         //smashPoint.GetComponent<SmashBowlController>();
            //         smashPoint.grapeCounter += 1;
            //     }
            // }

            // for (int i = 0; i < GrapeSmashPoint.Count; i++)
            // {
            //     if (GrapeSmashPoint[i].gameObject.activeInHierarchy && !GrapeSmashPoint[i].working)
            //     {
            //         GrapeSmashPoint[1].grapeCounter += 1;
            //     }
            // }
            GameEventHandler.current.PlayerGrapeDropping();
        }
        
    }
}
