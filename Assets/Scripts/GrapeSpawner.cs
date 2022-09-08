using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrapeSpawner : ObjectID
{
    [SerializeField] private List<Grape> Grapes = new List<Grape>(2);
    private bool once = false;
    private bool growing = false;
    private bool spawnable;
    public bool active = false;
    public bool gatherable = false;
    [SerializeField] private bool defaultTree;
    [SerializeField] private float grapeSpawnTime = 10.0f;
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private Transform basketSpawnPoint;
    [SerializeField] private UpgradeArea upgradeArea;
    private PlayerStackList _playerStackList;
    private ObjectID _otherId;
   

    private void Start()
    {
        _playerStackList = PlayerStackList.current;
        if (upgradeArea)
            upgradeArea.Activator += OnActivate;
    }

    private void OnDestroy()
    {
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

    public void PlayerGathering()
    {
        if (!active)
        {
            return;
        }
        if (gatherable && !_playerStackList.stackMax && !spawnable)
        {
            spawnable = true;
            var basket = Instantiate(basketPrefab,basketSpawnPoint.position,basketSpawnPoint.rotation)as GameObject;
            var playerStackPoint = _playerStackList.stackList[_playerStackList.stackList.Count -1];
            
            basket.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.25f).SetEase(Ease.OutFlash)
                                    .OnComplete((() => spawnable = false));
            _playerStackList.stackList.Add(basket);
            if (!growing)
            {
                StopCoroutine(GrapeCounter());
                StartCoroutine(GrapeCounter());
            }
            
            foreach (var grape in Grapes)
            {
                grape.gameObject.SetActive(false);
            }
            gatherable = false;
        }
    }


    private IEnumerator GrapeCounter()
    {
        growing = true;
        yield return new WaitForSeconds(grapeSpawnTime);
        foreach (var vaGrape in Grapes)
        {
            vaGrape.gameObject.SetActive(true);
            vaGrape.ParticlePlay();
            vaGrape.transform.DOShakeScale(0.5f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);
        }
        gatherable = true;
        growing = false;
        yield return null;
    }
}