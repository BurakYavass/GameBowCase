using System;
using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    private void OnTriggerEnter(Collider other)
    {
        var otherId = other.gameObject.GetComponent<ObjectID>();
        

        if (otherId.Type == ObjectType.Customer)
        {
            var drop = other.GetComponent<AgentAI>().dropPoint;
            PlayerStackList.current.OnPlayerWineGlassDropping(drop);
        }
    }
}
