using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelArea : ObjectID
{
    public List<GameObject> barrelPoint;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform barrelSpawnPoint;
    [SerializeField] private float stackDistance;
    
    
    void Start()
    {
        GameEventHandler.current.BarrelGenerate += CreateBarrel;
        GameEventHandler.current.OnCollectBarrel += OnCollectBarrel;
    }

    private void OnDestroy()
    {
        GameEventHandler.current.BarrelGenerate -= CreateBarrel;
        GameEventHandler.current.OnCollectBarrel -= OnCollectBarrel;
    }
    
    private void OnCollectBarrel()
    {
        Debug.Log("barrel collect event");
        for (int i = 0; i < barrelPoint.Count; i++)
        {
            Debug.Log("barrel collect for");
            if (barrelPoint.Count >0)
            {
                Debug.Log("barrel collect if");
                barrelPoint.RemoveAt(barrelPoint.Count-1);
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
