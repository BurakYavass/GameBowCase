using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSmashArea : ObjectID
{
    public List<SmashBowlController> GrapeSmashPoint;
    private ObjectID _otherId;

    [SerializeField] private PlayerGrapeStackList playerGrapeStackList;
    private SmashBowlController smashBowlController;

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        
        if (_otherId.Type == ObjectType.Player && playerGrapeStackList.basketList.Count >1)
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
        }
        
    }
}
