using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerStackList : ObjectID
{
    public event Action<int> Dropping;
    public List<Transform> stackList = new List<Transform>();
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Transform grapeDropPoint;
    [SerializeField] private Transform barrelDropPoint;
    private int stackCounter = 0;

    [SerializeField] private int grapeMaxStack = 5;
    [SerializeField] private float stackSpeed;
    [SerializeField] private float stackHeight;

    private bool once = false;
    private bool tweenbool = false;
    public bool grapeStackMax = false;

    private int barrelIndex;
    private int basketIndex;

    //private Transform find;
    //private int findIndex;

    void Start()
    {
        GameEventHandler.current.OnPlayerGrapeDropping += OnPlayerGrapeDropping;
        GameEventHandler.current.OnPlayerBarrelDropping += PlayerBarrelDropping;
        DOTween.Init();
        stackList.Add(stackPoint);
    }
    
    private void OnDestroy()
    {
        GameEventHandler.current.OnPlayerGrapeDropping -= OnPlayerGrapeDropping;
        GameEventHandler.current.OnPlayerBarrelDropping -= PlayerBarrelDropping;
    }
    private void PlayerBarrelDropping()
    {
        barrelIndex = stackList.FindIndex(x => x.CompareTag("Barrel"));
        if (stackList.Count > 0 && barrelIndex >0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[barrelIndex].transform.DOJump(barrelDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        //stackList[findIndex].gameObject.SetActive(false);
                        stackList.RemoveAt(barrelIndex);
                        Dropping?.Invoke(1);
                        tweenbool = false;
                    }));
            }
        }
    }

    private void OnPlayerGrapeDropping(int value)
    {
        basketIndex = stackList.FindIndex(x => x.CompareTag("Basket"));
        stackCounter += value;
        if (stackList.Count > 0 && basketIndex > 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[basketIndex].transform.DOJump(grapeDropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                     .OnComplete((() =>
                     {
                         //stackList[findIndex].gameObject.SetActive(false);
                         stackList.RemoveAt(basketIndex);
                     }))
                        .OnUpdate((() => tweenbool = false));
                stackCounter -= value;
            }
        }
    }

    private void Update()
    {
        if (stackList.Count >= grapeMaxStack)
        {
            if (!once)
            {
                grapeStackMax = true;
                GameEventHandler.current.GrapeStackMax();
                once = true;
            }
        }
        else if (stackList.Count < grapeMaxStack)
        {
            if (once)
            {
                grapeStackMax = false;
                GameEventHandler.current.GrapeStackMax();
                once = false;
            }
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
