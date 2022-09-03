using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour
{
    private bool once = false;
    private bool playerMax = false;
    public bool active = false;
    public bool gatherable = false;
    private bool growing = false;
    [SerializeField] private bool defaultTree;
    //private UpgradeArea upgradeArea;
    [SerializeField] private Transform playerPoint;
    
    public List<GameObject> Grapes = new List<GameObject>(2);
    [SerializeField] private GameObject basketPrefab;
    
    private Transform basketSpawnPoint;
    

    public float grapeSpawnTime = 10.0f;
    
    private void Awake()
    {
        basketSpawnPoint = GameObject.FindWithTag("BasketSpawn").transform;
    }

    private void Start()
    {
        GameEventHandler.current.PlayerGrapeStackMax += GatherableChanger;
        GameEventHandler.current.OnPlayerGrapeDropping += RemoveClone;
        GameEventHandler.current.OnObjectActive += OnActivate;
    }

    private void OnDestroy()
    {
        GameEventHandler.current.PlayerGrapeStackMax -= GatherableChanger;
        GameEventHandler.current.OnPlayerGrapeDropping -= RemoveClone;
        GameEventHandler.current.OnObjectActive -= OnActivate;
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

    private void GatherableChanger()
    {
        if (playerMax)
        {
            playerMax = false;
        }
        else if(!playerMax)
        {
            playerMax = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;
        
        if (other.CompareTag("Player") && gatherable && !playerMax)
        {
            GameObject basket = Instantiate(basketPrefab,basketSpawnPoint.position,basketSpawnPoint.rotation)as GameObject;

            var playerStackPoint = playerPoint.position;
            basket.transform.DOJump(playerStackPoint, 5, 1, 0.25f).SetEase(Ease.OutFlash);

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
