using System;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager current;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject CloneImage;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Camera cam;

    private void Start()
    {
        if (cam == null)
        {
            cam= Camera.main;
        }
    }

    void Update()
    {
        goldText.text = GameManager.current.playerGold.ToString("0");
    }

    public void MoneyInstant(Vector3 customerPos)
    {
        //var playerPos = customerPos;
        var instantiatePos = targetPos.position;
        //Vector3 cloneImagePos = cam.WorldToScreenPoint(new Vector3(instantiatePos.x, instantiatePos.y, instantiatePos.z*-1));
        Vector3 customerWorldPos = cam.WorldToScreenPoint(new Vector3(customerPos.x, customerPos.y, customerPos.z*-1));
        var coin = Instantiate(CloneImage, customerWorldPos, targetPos.rotation,targetPos.parent);

        coin.transform.DOScale(Vector3.one, .3f);
        coin.transform.DOMove(instantiatePos,.3f).OnComplete((() =>
        {
            coin.transform.DOKill();
            Destroy(coin);
        }));
    }
}
