using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSmashArea : ObjectID
{
    public List<SmashBowlController> GrapeSmashPoint;
    private ObjectID _otherId;

    [SerializeField] private PlayerStackList playerStackList;
    private SmashBowlController smashBowlController;

    private void OnTriggerStay(Collider other)
    {
        if (!_otherId || !playerStackList)
        {
            _otherId = other.gameObject.GetComponent<ObjectID>();
            playerStackList = other.gameObject.GetComponent<PlayerStackList>();
        }

        if (_otherId.Type == ObjectType.Player && playerStackList.basketList.Count >1)
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
