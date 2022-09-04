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

    private Transform find;
    private int findIndex;

    void Start()
    {
        GameEventHandler.current.OnPlayerGrapeDropping += OnPlayerGrapeDropping;
        DOTween.Init();
        stackList.Add(stackPoint);
    }

    private void OnDestroy()
    {
        GameEventHandler.current.OnPlayerGrapeDropping -= OnPlayerGrapeDropping;
    }

    private void OnPlayerGrapeDropping(int value)
    {
        find = stackList.FirstOrDefault((x => x.CompareTag("Basket")));
        stackCounter += value;
        if (stackList.Count > 0 && find)
        {
            //find = stackList.Find(x => x.name == Basket);
            if (!tweenbool)
            {
                tweenbool = true;
                findIndex = stackList.FindIndex(x => x.CompareTag("Basket"));
                //find = stackList.Find((x => x.CompareTag("Basket")));
                
                stackList[findIndex].transform.DOJump(dropPoint.transform.position, 7, 1, .5f).SetEase(Ease.OutFlash)
                     .OnComplete((() =>stackList.RemoveAt(findIndex)))
                        .OnUpdate((() => tweenbool = false));
                stackCounter -= value;
            }
            
            // stackCounter -= value;
        }
    }
    
    void Update()
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
                var downGameObject = stackList[i-1];
                var currentBasket = stackList[i];
                var xPosition = Mathf.Lerp(currentBasket.transform.position.x, downGameObject.transform.position.x,
                    stackSpeed);
                currentBasket.transform.rotation = downGameObject.transform.rotation;
                currentBasket.transform.position = new Vector3(xPosition,
                    downGameObject.transform.position.y + stackHeight, downGameObject.transform.position.z);

            }
        }
    }
}
