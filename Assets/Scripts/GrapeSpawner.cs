using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour
{
    public List<GameObject> Grapes = new List<GameObject>(2);

    public float grapeSpawnTime = 10.0f;

    public bool active = false;

    public bool gatherable = false;

    private void Start()
    {
        StartCoroutine(GrapeCounter());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;
        
        if (other.CompareTag("Player") && gatherable)
        {
            
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
