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
    [SerializeField] private bool defaultTree;
    
    public List<GameObject> Grapes = new List<GameObject>(2);
    [SerializeField] private GameObject basketPrefab;
    
    private Transform basketSpawnPoint;
    private GameObject playerObject;

    public float grapeSpawnTime = 10.0f;

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        basketSpawnPoint = GameObject.FindWithTag("BasketSpawn").transform;
    }

    private void Start()
    {
        GameEventHandler.current.PlayerGrapeStackMax += GatherableChanger;
        if (!defaultTree && !once)
        {
            StartCoroutine(GrapeCounter());
            once = true;
        }
        
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

            var playerPosition = playerObject.transform.position;
            basket.transform.DOJump(playerPosition, 10, 1, 1.5f);
            //GameEventHandler.current.PlayerGathering(basket);
            
            StartCoroutine(GrapeCounter());
            
            foreach (var grape in Grapes)
            {
                grape.SetActive(false);
            }
            gatherable = false;
        }
    }
    

    IEnumerator GrapeCounter()
    {
        yield return new WaitForSeconds(grapeSpawnTime);
        foreach (var vaGrape in Grapes)
        {
            gatherable = true;
            vaGrape.SetActive(true);
        }
        yield return null;
    }
}
