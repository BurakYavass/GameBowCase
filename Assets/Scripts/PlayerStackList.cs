using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class PlayerStackList : MonoBehaviour
{
    public List<Transform> basketList = new List<Transform>();
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Transform dropPoint;
    private int stackCounter = 0;

    [SerializeField] private int grapeMaxStack = 5;
    [SerializeField] private float stackSpeed;
    [SerializeField] private float stackHeight;

    private bool once = false;
    public bool grapeStackMax = false;

    void Start()
    {
        GameEventHandler.current.OnPlayerGrapeDropping += OnPlayerGrapeDropping;
        DOTween.Init();
        basketList.Add(stackPoint);
    }

    private void OnDestroy()
    {
        GameEventHandler.current.OnPlayerGrapeDropping -= OnPlayerGrapeDropping;
    }

    private void OnPlayerGrapeDropping(int value)
    {
        stackCounter += value;
        if (basketList.Count > 1)
        {
            // basketList[stackCounter].DOJump(dropPoint.transform.position, 5, 1, 1).
            //             OnComplete((() => basketList.RemoveAt(stackCounter)));
            basketList[stackCounter].DOJump(dropPoint.transform.position, 7, 1, .5f).SetEase(Ease.OutFlash);
            basketList.RemoveAt(stackCounter);
            stackCounter -= value;
        }
    }
    
    void Update()
    {
        if (basketList.Count >= grapeMaxStack)
        {
            if (!once)
            {
                grapeStackMax = true;
                GameEventHandler.current.GrapeStackMax();
                once = true;
            }
        }
        else if (basketList.Count < grapeMaxStack)
        {
            if (once)
            {
                grapeStackMax = false;
                GameEventHandler.current.GrapeStackMax();
                once = false;
            }
        }
        
        if (basketList.Count > 1)
        {
            for (int i = 1; i < basketList.Count; i++)
            {
                var downGameObject = basketList[i-1];
                var currentBasket = basketList[i];
                var xPosition = Mathf.Lerp(currentBasket.transform.position.x, downGameObject.transform.position.x,
                    stackSpeed);
                currentBasket.transform.rotation = downGameObject.transform.rotation;
                currentBasket.transform.position = new Vector3(xPosition,
                    downGameObject.transform.position.y + stackHeight, downGameObject.transform.position.z);

            }
        }
    }
}