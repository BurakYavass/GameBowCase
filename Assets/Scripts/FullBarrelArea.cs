using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBarrelArea : ObjectID
{
    public List<GameObject> Barrels;

    public bool barrelsMax =false;

    private ObjectID _otherId;

    private int barrelCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (barrelCount == 6)
        {
            barrelsMax = true;
        }
        else
        {
            barrelsMax = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null)
            _otherId = other.gameObject.GetComponent<ObjectID>();
        if (_otherId.Type == ObjectType.Player && !barrelsMax)
        {
            GameEventHandler.current.BarrelDropping();
        }
    }
}
