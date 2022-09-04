using System;
using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    [SerializeField] private PlayerStackList stackList;
    private ObjectID _otherId;

    private void OnTriggerEnter(Collider other)
    {
        if (_otherId == null)
            _otherId = other.GetComponent<ObjectID>();
        if (_otherId.Type == ObjectType.BarrelArea)
        {
            Debug.Log("other Barrel");
            GameEventHandler.current.BarrelCollect();
        }
        
    }
}
