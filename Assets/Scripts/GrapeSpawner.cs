using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrapeSpawner : ObjectID
{
    private bool once = false;
    private bool growing = false;
    public bool active = false;
    public bool gatherable = false;
    [SerializeField] private bool defaultTree;

    public float grapeSpawnTime = 10.0f;
    public List<GameObject> Grapes = new List<GameObject>(2);
    
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform basketSpawnPoint;
    [SerializeField] private UpgradeArea upgradeArea;
    private PlayerStackList _playerStackList;
    private ObjectID _otherId;

    private void Start()
    {
        //GameEventHandler.current.OnPlayerGrapeDropping += RemoveClone;
        //GameEventHandler.current.OnObjectActive += OnActivate;
        _playerStackList = playerPoint.GetComponentInParent<PlayerStackList>();
        if (upgradeArea)
            upgradeArea.Activator += OnActivate;
    }

    private void OnDestroy()
    {
        //GameEventHandler.current.OnPlayerGrapeDropping -= RemoveClone;
        //GameEventHandler.current.OnObjectActive -= OnActivate;
        if (upgradeArea)
            upgradeArea.Activator -= OnActivate;
        
    }

    private void OnActivate()
    {
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            if (!defaultTree && !growing && !once)
            {
                StartCoroutine(GrapeCounter());
                once = true;
            }
        }
    }

    private void RemoveClone(int obj)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        
        if (_otherId.Type == ObjectType.Player && gatherable && !_playerStackList.stackMax)
        {
            var basket = Instantiate(basketPrefab,basketSpawnPoint.position,basketSpawnPoint.rotation)as GameObject;

            var playerStackPoint = _playerStackList.stackList[_playerStackList.stackList.Count -1];
            basketPrefab.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.25f).SetEase(Ease.OutFlash);
            
            _playerStackList.stackList.Add(basket);
            if (!growing)
            {
                StopCoroutine(GrapeCounter());
                StartCoroutine(GrapeCounter());
            }
            
            foreach (var grape in Grapes)
            {
                grape.SetActive(false);
            }
            gatherable = false;
        }
    }
    

    IEnumerator GrapeCounter()
    {
        growing = true;
        yield return new WaitForSeconds(grapeSpawnTime);
        foreach (var vaGrape in Grapes)
        {
            vaGrape.SetActive(true);
            vaGrape.transform.DOShakeScale(0.5f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);
        }
        gatherable = true;
        growing = false;
        yield return null;
    }
}