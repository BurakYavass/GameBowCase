using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerStackList : ObjectID
{
    public List<Transform> stackList = new List<Transform>();
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Transform dropPoint;
    private int stackCounter = 0;

    [SerializeField] private int grapeMaxStack = 5;
    [SerializeField] private float stackSpeed;
    [SerializeField] private float stackHeight;

    private bool once = false;
    private bool tweenbool = false;
    public bool grapeStackMax = false;

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
        var findIndex = stackList.FindIndex(x => x.CompareTag("Barrel"));
        if (stackList.Count > 0 && findIndex !=0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[findIndex].transform.DOJump(dropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                    .OnComplete((() =>
                    {
                        //stackList[findIndex].gameObject.SetActive(false);
                        stackList.RemoveAt(findIndex);
                    }))
                    .OnUpdate((() => tweenbool = false));
            }
        }
    }

    private void OnPlayerGrapeDropping(int value)
    {
        var findIndex = stackList.FindIndex(x => x.CompareTag("Basket"));
        stackCounter += value;
        if (stackList.Count > 0 && findIndex != 0)
        {
            if (!tweenbool)
            {
                tweenbool = true;
                stackList[findIndex].transform.DOJump(dropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                     .OnComplete((() =>
                     {
                         //stackList[findIndex].gameObject.SetActive(false);
                         stackList.RemoveAt(findIndex);
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
