using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BarController : ObjectID
{
    public static BarController current;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private Transform glassSpawnPoint;
    [SerializeField] private Image FillImage;
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
            FillImage.DOPlay();
            spawnable = true;
            spawnCounter = Mathf.Clamp(spawnCounter + spawn, 0, spawnMax);
            var playerStackPoint = PlayerStackList.current.stackList[PlayerStackList.current.stackList.Count -1];
            FillImage.DOFillAmount(1,0.5f)
                .OnComplete((() =>
                        {
                            var _glass = Instantiate(glassPrefab, glassSpawnPoint.position, glassPrefab.transform.rotation) as GameObject;
                            _glass.transform.DOJump(playerStackPoint.transform.position, 5, 1, 0.5f)
                                .SetEase(Ease.OutFlash)
                                .OnComplete((() =>
                                {
                                    PlayerStackList.current.stackList.Add(_glass);
                                    FillImage.fillAmount = 0;
                                    _glass.transform.DOKill();
                                    spawnable = false;
                                }));
                        }
               ));
        }
    }

    public void WaiterOnBar(int spawn)
    {
        if (FullBarrelArea.current.barIsWorkable && spawnCounter <5 && !spawnable)
        {
            FillImage.DOPlay();
            spawnable = true;
            spawnCounter = Mathf.Clamp(spawnCounter+spawn, 0, spawnMax);
            var waiterStackPoint = WaiterStackList.current.stackList[WaiterStackList.current.stackList.Count -1];
            FillImage.DOFillAmount(1,0.5f)
                .OnComplete((() =>
                {
                    var _glass = Instantiate(glassPrefab, glassSpawnPoint.position, glassPrefab.transform.rotation) as GameObject;
                    _glass.transform.DOJump(waiterStackPoint.transform.position, 5, 1, 0.5f).SetEase(Ease.OutFlash)
                        .OnComplete((() =>
                        {
                            WaiterStackList.current.stackList.Add(_glass);
                            FillImage.fillAmount = 0;
                            _glass.transform.DOKill();
                            spawnable = false;
                        }));
                    
                }));
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
