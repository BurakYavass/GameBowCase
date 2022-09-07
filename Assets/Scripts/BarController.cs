using DG.Tweening;
using UnityEngine;

public class BarController : ObjectID
{
    private ObjectID _otherId;
    [SerializeField] private PlayerStackList _playerStackList;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private Transform glassSpawnPoint;
    public int spawnCounter = 0;
    private bool spawnable = false;
    private int spawnMax = 4;

    public void PlayerOnBar(int spawn)
    {
        if (FullBarrelArea.current.barIsWorkable && spawnCounter <5 && !spawnable)
        {
            spawnable = true;
            spawnCounter = Mathf.Clamp(spawnCounter + spawn, 0, spawnMax);
            var glass = Instantiate(glassPrefab,glassSpawnPoint.position,glassPrefab.transform.rotation)as GameObject;
            var playerStackPoint = _playerStackList.stackList[_playerStackList.stackList.Count -1];
            glass.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.3f).SetEase(Ease.OutFlash)
                                                    .OnComplete((() => spawnable = false));
            
            PlayerStackList.current.stackList.Add(glass);
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
