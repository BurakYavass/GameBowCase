using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelArea : MonoBehaviour
{
    public List<GameObject> barrelPoint;
    
    void Start()
    {
        GameEventHandler.current.BarrelGenerate += CreateBarrel;
    }

    private void CreateBarrel()
    {
        var i = 0;
        if (!barrelPoint[i].activeInHierarchy)
        {
            barrelPoint[i].SetActive(true);
        }
        else if (barrelPoint[i].activeInHierarchy)
        {
            i++;
            barrelPoint[i].SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // for (int i = 0; i < barrelPoint.Count; i++)
        // {
        //     barrelPoint[i].SetActive(true);
        // }
    }
}
