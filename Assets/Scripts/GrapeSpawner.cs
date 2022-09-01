using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour
{
    public List<GameObject> Grapes = new List<GameObject>(2);
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private Transform basketSpawnPoint;
    
    private Transform playerTransform;
    
    public float grapeSpawnTime = 10.0f;

    public bool active = false;

    public bool gatherable = false;

    private void Start()
    {
        StartCoroutine(GrapeCounter());
        playerTransform = GameObject.FindWithTag("Player").transform;
        basketSpawnPoint = GameObject.FindWithTag("BasketSpawn").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;
        
        if (other.CompareTag("Player") && gatherable)
        {
            GameObject basket = Instantiate(basketPrefab,basketSpawnPoint.position,basketSpawnPoint.rotation)as GameObject;

            var playerPosition = playerTransform.transform.position;
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
            vaGrape.SetActive(true);
            gatherable = true;
        }
        yield return null;
    }
}
