using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerStackList : ObjectID
{
    public static PlayerStackList current;
    public event Action<int> Dropping;
    public List<GameObject> stackList = new List<GameObject>();
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Transform grapeDropPoint;
    [SerializeField] private Transform barrelDropPoint;
    //private int stackCounter = 0;

    private int maxStack;
    [SerializeField] private float stackSpeed;
    [SerializeField] private float stackHeight;

    //private bool once = false;
    private bool tweenbool = false;
    public bool stackMax = false;

    private int barrelIndex;
    private int basketIndex;
    private int wineIndex;

    //private Transform find;
    //private int findIndex;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    void Start()
    {
        DOTween.Init();
        stackList.Add(stackPoint.gameObject);
    }
    
    public void OnPlayerBarrelDropping()
    {
        barrelIndex = stackList.FindLastIndex(x => x.name == "Barrel(Clone)");
        if (stackList.Count > 0 && barrelIndex>0 && !FullBarrelArea.current.barrelsMax)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[barrelIndex].transform.DOJump(barrelDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        //stackList[barrelIndex]SetActive(false);
                        Destroy(stackList[barrelIndex]);
                        stackList.RemoveAt(barrelIndex);
                        Dropping?.Invoke(1);
                        tweenbool = false;
                    }));
            }
        }
    }

    public void OnPlayerGrapeDropping()
    {
        basketIndex = stackList.FindLastIndex(x => x.name == "Basket(Clone)");
        if (stackList.Count > 0 && basketIndex > 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[basketIndex].transform.DOJump(grapeDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        stackList[basketIndex].transform.DOKill();
                        Destroy(stackList[basketIndex]);
                        stackList.RemoveAt(basketIndex);
                        tweenbool = false;
                    }));
            }
        }
    }

    public void OnPlayerWineGlassDropping(Transform dropPoint,AgentAI agent)
    {
        wineIndex = stackList.FindLastIndex(x => x.name == "WineGlass(Clone)");
        if (stackList.Count > 0 && wineIndex > 0 && agent.wine == 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[wineIndex].transform.DOJump(dropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        stackList[wineIndex].transform.DOKill();
                        agent.StateChange();
                        Destroy(stackList[wineIndex],1.0f);
                        stackList.RemoveAt(wineIndex);
                        tweenbool = false;
                    }));
            }
        }
    }

    public void OnPlayerDustBin(Vector3 binPos)
    {
        if (!tweenbool)
        {
            tweenbool = true;
            for (int i = 1; i < stackList.Count; i++)
            {
                if (stackList.Count >1)
                {
                    stackList[i].transform.DOJump(binPos, 7, 1, 0.3f).SetEase(Ease.OutFlash)
                        .OnComplete((() =>
                        {
                            DustBin.current.FillImage(0.3f);
                            stackList[i].transform.DOKill();
                            Destroy(stackList[i]);
                            stackList.RemoveAt(i);
                            tweenbool = false;
                        }));
                    return;
                }
            }
        }
    }

    private void Update()
    {
        maxStack = GameManager.playerMaxStack;
        if (stackList.Count >= maxStack)
        {
            stackMax = true;
        }
        else if (stackList.Count < maxStack)
        {
            stackMax = false;
        }
        
        if (stackList.Count > 1)
        {
            for (int i = 1; i < stackList.Count; i++)
            {
                var downGameObject = stackList[i-1].transform;
                var currentObject = stackList[i].transform;
                // var xPosition = Mathf.Lerp(currentBasket.transform.position.x, downGameObject.transform.position.x,
                //     stackSpeed);
                currentObject.rotation = currentObject.rotation;
                currentObject.transform.position = new Vector3(downGameObject.position.x,
                    downGameObject.position.y + stackHeight, downGameObject.position.z);

            }
        }
    }
    
}
