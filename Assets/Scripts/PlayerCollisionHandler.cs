using System;
using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    private void OnTriggerEnter(Collider enter)
    {
        var otherId = enter.gameObject.GetComponent<ObjectID>();
        
        if (otherId.Type == ObjectType.Customer)
        {
            var wineIndex = PlayerStackList.current.stackList.FindLastIndex(x => x.name == "WineGlass(Clone)");
            var agentAI = enter.GetComponent<AgentAI>();
            if (agentAI.waitingServe && wineIndex>0)
            {
                PlayerStackList.current.OnPlayerWineGlassDropping(agentAI.dropPoint,agentAI);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var _otherId = other.gameObject.GetComponent<ObjectID>();
        if (_otherId.Type == ObjectType.Grape)
        {
            var grapeSpawner = other.GetComponent<GrapeSpawner>();
            grapeSpawner.PlayerGathering();
        }
        else if (_otherId.Type == ObjectType.BarrelSpawnArea)
        {
            var barrelSpawnArea= other.GetComponent<BarrelSpawnArea>();
            barrelSpawnArea.OnCollectBarrel();
        }
        else if (_otherId.Type == ObjectType.BarrelArea)
        {
            PlayerStackList.current.OnPlayerBarrelDropping();
        }
        else if (_otherId.Type == ObjectType.Bar)
        {
            var bar = other.GetComponent<BarController>();
            if (!PlayerStackList.current.stackMax)
            {
                bar.PlayerOnBar(1);
            }
        }
        else if (_otherId.Type == ObjectType.DustBin)
        {
            if (PlayerStackList.current.stackList.Count>1)
            {
                var dustBinPos = other.gameObject.transform.position;
                PlayerStackList.current.OnPlayerDustBin(dustBinPos);
            }
        }
    }
        
}

