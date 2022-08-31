using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelArea : MonoBehaviour
{
    public List<GameObject> barrelPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < barrelPoint.Count; i++)
        {
            barrelPoint[i].SetActive(true);
        }
    }
}
