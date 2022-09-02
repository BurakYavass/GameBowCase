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

    private bool allIsWorking = false;

    private void OnTriggerStay(Collider other)
    {
        if (_objectID == null)
            _objectID = other.gameObject.GetComponent<ObjectID>();
        
        if (_objectID.Type == ObjectType.Player && playerGrapeStackList.basketList.Count >1)
        {
            for (int i = 0; i < GrapeSmashPoint.Count; i++)
            {
                if (GrapeSmashPoint[i].active && !GrapeSmashPoint[i].working)
                {
                    GameEventHandler.current.PlayerGrapeDropping(1);
                    GrapeSmashPoint[i].PlayerGrapeDropping(1);
                    break;
                }
            }

            // if (GrapeSmashPoint[0].active && !GrapeSmashPoint[0].working)
            // {
            //     GameEventHandler.current.PlayerGrapeDropping(1);
            // }
            // else if(GrapeSmashPoint[1].active && !GrapeSmashPoint[1].working)
            // {
            //     GameEventHandler.current.PlayerGrapeDropping(1);
            // }
            // else if (GrapeSmashPoint[2].active && !GrapeSmashPoint[2].working)
            // {
            //     GameEventHandler.current.PlayerGrapeDropping(1);
            // }
            // else if (GrapeSmashPoint[3].active && !GrapeSmashPoint[3].working)
            // {
            //     GameEventHandler.current.PlayerGrapeDropping(1);
            // }
            // else
            // {
            //     allIsWorking = true;
            // }
            //
            // foreach (var point in GrapeSmashPoint)
            // {
            //     
            // }
        }
        
    }
}
