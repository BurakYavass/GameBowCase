using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerGrapeStackList : MonoBehaviour
{
    public List<Transform> basketList;
    [SerializeField] private Transform stackPoint;

    [SerializeField] private int grapeMaxStack = 5;
    [SerializeField] private float stackSpeed;
    [SerializeField] private float stackHeight;

    private bool once = false;
    public bool grapeStackMax = false;

    void Start()
    {
        GameEventHandler.current.OnPlayerGathering += OnPlayerGathering;
        DOTween.Init();
        basketList.Add(stackPoint);
    }

    private void OnPlayerGathering()
    {
        //basketList.Add(basket);
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
