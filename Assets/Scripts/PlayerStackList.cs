using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerStackList : ObjectID
{
    public static PlayerStackList current;
    public event Action<int> Dropping;
    public List<Transform> stackList = new List<Transform>();
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Transform grapeDropPoint;
    [SerializeField] private Transform barrelDropPoint;
    private int stackCounter = 0;

    [SerializeField] private int maxStack = 5;
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
        GameEventHandler.current.OnPlayerGrapeDropping += OnPlayerGrapeDropping;
        GameEventHandler.current.OnPlayerBarrelDropping += OnPlayerBarrelDropping;
        DOTween.Init();
        stackList.Add(stackPoint);
    }
    
    private void OnDestroy()
    {
        GameEventHandler.current.OnPlayerGrapeDropping -= OnPlayerGrapeDropping;
        GameEventHandler.current.OnPlayerBarrelDropping -= OnPlayerBarrelDropping;
    }
    private void OnPlayerBarrelDropping()
    {
        barrelIndex = stackList.FindLastIndex(x => x.CompareTag("Barrel"));
        if (stackList.Count > 0 && barrelIndex >0 && !FullBarrelArea.current.barrelsMax)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[barrelIndex].transform.DOJump(barrelDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        //tackList[barrelIndex].gameObject.SetActive(false);
                        //Destroy(stackList[barrelIndex].gameObject,1.0f);
                        stackList.RemoveAt(barrelIndex);
                        Dropping?.Invoke(1);
                        tweenbool = false;
                    }));
            }
        }
    }

    private void OnPlayerGrapeDropping(int value)
    {
        basketIndex = stackList.FindLastIndex(x => x.CompareTag("Basket"));
        stackCounter += value;
        if (stackList.Count > 0 && basketIndex > 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[basketIndex].transform.DOJump(grapeDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                     .OnComplete((() =>
                     {
                         //stackList[basketIndex].gameObject.SetActive(false);
                         //Destroy(stackList[basketIndex].gameObject,1.0f);
                         stackList.RemoveAt(basketIndex);
                     }))
                        .OnUpdate((() => tweenbool = false));
                stackCounter -= value;
            }
        }
    }

    public void OnPlayerWineGlassDropping(Transform dropPoint)
    {
        wineIndex = stackList.FindLastIndex(x => x.CompareTag("Wine"));
        if (stackList.Count > 0 && wineIndex > 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[wineIndex].transform.DOJump(dropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        //stackList[wineIndex].gameObject.SetActive(false);
                        //Destroy(stackList[wineIndex],1.0f);
                        stackList.RemoveAt(wineIndex);
                    }))
                    .OnUpdate((() => tweenbool = false));
            }
        }
    }

    private void Update()
    {
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
