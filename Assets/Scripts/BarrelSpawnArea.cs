using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarrelSpawnArea : ObjectID
{
    public List<GameObject> barrelPoint;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform barrelSpawnPoint;
    [SerializeField] private float stackDistance;

    private ObjectID _otherId;
    [SerializeField] private PlayerStackList _playerGrapeStackList;

    private bool once = false;

    public bool barrelAreaMax = false;


    void Start()
    {
        GameEventHandler.current.BarrelGenerate += CreateBarrel;
    }

    private void OnDestroy()
    {
        GameEventHandler.current.BarrelGenerate -= CreateBarrel;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!_otherId || !_playerGrapeStackList)
        {
            _otherId = other.GetComponent<ObjectID>();
            _playerGrapeStackList = other.GetComponent<PlayerStackList>();
        }
            
        if (_otherId.Type == ObjectType.Player)
        {
            if (!once)
            {
                OnCollectBarrel();
            }
            
        }
    }
    
    private void OnCollectBarrel()
    {
        for (int i = 0; i < barrelPoint.Count; i++)
        {
            if (barrelPoint.Count >=0 && !_playerGrapeStackList.stackMax)
            {
                once = true;
                var playerStackPoint = _playerGrapeStackList.stackList[_playerGrapeStackList.stackList.Count -1];
                barrelPoint[barrelPoint.Count - 1].transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.25f)
                                                                .SetEase(Ease.OutFlash)
                                                                    .OnComplete((() =>
                                                                    {
                                                                        once = false;
                                                                        _playerGrapeStackList.stackList.Add(barrelPoint[barrelPoint.Count-1].transform);
                                                                        barrelPoint.RemoveAt(barrelPoint.Count - 1);
                                                                    }));
                
                break;
            }
        }
    }

    private void CreateBarrel()
    {
        if (barrelPoint.Count < 5)
        {
            barrelPrefab = Instantiate(barrelPrefab,barrelSpawnPoint.position,barrelSpawnPoint.transform.rotation)as GameObject;
        
            barrelPoint.Add(barrelPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (barrelPoint.Count > 4)
        {
            barrelAreaMax = true;
        }
        else
        {
            barrelAreaMax = false;
        }
        if (barrelPoint.Count > 1)
        {
            for (int i = 1; i < barrelPoint.Count; i++)
            {
                var downGameObject = barrelPoint[i-1];
                var currentBasket = barrelPoint[i];
                // var xPosition = Mathf.Lerp(currentBasket.transform.position.x, downGameObject.transform.position.x,
                //     stackSpeed);
                currentBasket.transform.rotation = downGameObject.transform.rotation;
                currentBasket.transform.position = new Vector3(downGameObject.transform.position.x + stackDistance,
                    downGameObject.transform.position.y , downGameObject.transform.position.z);
        
            }
        }
    }
}
