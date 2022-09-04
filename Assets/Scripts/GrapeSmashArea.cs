using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapeSmashArea : ObjectID
{
    public List<SmashBowlController> grapeSmashPoint;
    private ObjectID _otherId;

    private PlayerStackList _playerStackList;
    //private SmashBowlController _smashBowlController;

    private bool _find;
    private bool waiterr =false;

    private void OnTriggerStay(Collider other)
    {
        if (!_otherId || !_playerStackList)
        {
            _otherId = other.gameObject.GetComponent<ObjectID>();
            _playerStackList = other.gameObject.GetComponent<PlayerStackList>();
        }

        _find = _playerStackList.stackList.FirstOrDefault((x => x.CompareTag("Basket")));
       
        if (_otherId.Type == ObjectType.Player && _find)
        {
            for (var i = 0; i < grapeSmashPoint.Count; i++)
            {
                if (grapeSmashPoint[i].active && !grapeSmashPoint[i].working && !waiterr)
                {
                    waiterr = true;
                    GameEventHandler.current.PlayerGrapeDropping(1);
                    grapeSmashPoint[i].PlayerGrapeDropping(1);
                    StartCoroutine(waiter());
                    return;
                }
            }
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        waiterr = false;

    }
}
