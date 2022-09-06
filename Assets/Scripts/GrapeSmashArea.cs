using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapeSmashArea : ObjectID
{
    public static GrapeSmashArea current;
    public List<SmashBowlController> grapeSmashPoint;
    private ObjectID _otherId;

    private PlayerStackList _playerStackList;

    private bool _find;
    private bool waiterr =false;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_otherId || !_playerStackList)
        {
            _otherId = other.gameObject.GetComponent<ObjectID>();
            _playerStackList = PlayerStackList.current;
        }
    
        _find = _playerStackList.stackList.FirstOrDefault((x => x.CompareTag("Basket")));
       
         if (_otherId.Type == ObjectType.Player && _find)
         {
             for (var i = 0; i < grapeSmashPoint.Count; i++)
             {
                 if (grapeSmashPoint[i].active && !grapeSmashPoint[i].working && !waiterr)
                 {
                     waiterr = true;
                     _playerStackList.OnPlayerGrapeDropping();
                     grapeSmashPoint[i].PlayerGrapeDropping(1);
                     StartCoroutine(Waiter());
                     return;
                 }
             }
         }
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.3f);
        waiterr = false;
        yield return null;

    }
}
