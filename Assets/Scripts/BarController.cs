using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarController : ObjectID
{
    private ObjectID _otherId;
    [SerializeField] private PlayerStackList _playerStackList;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private Transform glassSpawnPoint;
    private void OnTriggerStay(Collider other)
    {
        if (!_otherId || !_playerStackList)
        {
            _otherId = other.gameObject.GetComponent<ObjectID>();
            _playerStackList = other.gameObject.GetComponent<PlayerStackList>();
        }
            
        if (_otherId.Type == ObjectType.Player && FullBarrelArea.current.barIsWorkable && !_playerStackList.stackMax)
        {
            var glass = Instantiate(glassPrefab,glassSpawnPoint.position,glassPrefab.transform.rotation)as GameObject;

            var playerStackPoint = _playerStackList.stackList[_playerStackList.stackList.Count -1];
            glassPrefab.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.5f).SetEase(Ease.OutFlash);
            
            _playerStackList.stackList.Add(glass);
        }
    }
}
