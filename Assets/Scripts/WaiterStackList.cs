using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaiterStackList : MonoBehaviour
{
    public static WaiterStackList current ;
    public List<GameObject> stackList = new List<GameObject>();
    private bool tweenbool = false;
    
    private int stackCounter;
    private float stackHeight = 1.5f;
    
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    private void Update()
    {
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

    public void ServeGlass(Transform dropPoint,AgentAI agent)
    {
        if (!tweenbool)
        {
            tweenbool = true;
            stackList[stackList.Count-1].transform.DOJump(dropPoint.transform.position, 7, 1, .3f).SetEase(Ease.OutFlash)
                .OnComplete((() =>
                {
                    stackList[stackList.Count-1].transform.DOKill();
                    agent.StateChange();
                    Destroy(stackList[stackList.Count-1],1.0f);
                    stackList.RemoveAt(stackList.Count-1);
                    tweenbool = false;
                }));
        }
    }
}
