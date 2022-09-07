using System;
using DG.Tweening;
using UnityEngine;

public class BarController : ObjectID
{
    public static BarController current;
    private ObjectID _otherId;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private Transform glassSpawnPoint;
    public int spawnCounter = 0;
    private bool spawnable = false;
    private int spawnMax = 4;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    public void PlayerOnBar(int spawn)
    {
        if (FullBarrelArea.current.barIsWorkable && spawnCounter <5 && !spawnable)
        {
            spawnable = true;
            spawnCounter = Mathf.Clamp(spawnCounter + spawn, 0, spawnMax);
            var glass = Instantiate(glassPrefab,glassSpawnPoint.position,glassPrefab.transform.rotation)as GameObject;
            var playerStackPoint = PlayerStackList.current.stackList[PlayerStackList.current.stackList.Count -1];
            glass.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.3f).SetEase(Ease.OutFlash)
                .OnComplete((() =>
                {
                    glass.transform.DOKill();
                    spawnable = false;
                }));
            
            PlayerStackList.current.stackList.Add(glass);
        }
    }

    public void WaiterOnBar(int spawn)
    {
        if (FullBarrelArea.current.barIsWorkable && spawnCounter <5 && !spawnable)
        {
            //spawnCounter++;
            spawnable = true;
            spawnCounter = Mathf.Clamp(spawnCounter+spawn, 0, spawnMax);
            var glass = Instantiate(glassPrefab,glassSpawnPoint.position,glassPrefab.transform.rotation)as GameObject;
            var waiterStackPoint = WaiterStackList.current.stackList[WaiterStackList.current.stackList.Count -1];
            glass.transform.DOJump(waiterStackPoint.transform.position, 5, 1, 0.5f).SetEase(Ease.OutFlash)
                .OnComplete((() =>
                {
                    glass.transform.DOKill();
                    spawnable = false;
                }));
            
            WaiterStackList.current.stackList.Add(glass);
        }
    }
    

    private void Update()
    {
        if (spawnCounter == spawnMax)
        {
            FullBarrelArea.current.BarrelControl(1);
            FullBarrelArea.current.Barrels[0].gameObject.SetActive(false);
            spawnCounter = 0;
        }
    }
}
