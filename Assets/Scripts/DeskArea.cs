using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskArea : ObjectID
{
    public static DeskArea current;
    public List<GameObject> Desks;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
