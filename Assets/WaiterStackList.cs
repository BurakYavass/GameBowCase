using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterStackList : MonoBehaviour
{
    public static WaiterStackList current ;
    public List<GameObject> stackList = new List<GameObject>();
    private int stackCounter;
    
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    
}
