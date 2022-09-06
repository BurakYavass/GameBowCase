using System;
using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    private void OnTriggerEnter(Collider other)
    {
        var otherId = other.gameObject.GetComponent<ObjectID>();
        
        if (otherId.Type == ObjectType.Customer)
        {
            var agentAI = other.GetComponent<AgentAI>();
            if (agentAI.waitingServe)
            {
                PlayerStackList.current.OnPlayerWineGlassDropping(agentAI.dropPoint,agentAI);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //var otherId = other.gameObject.GetComponent<ObjectID>();
    }
}
