using System;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager current;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject CloneImage;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private TextMeshProUGUI speedMax;
    [SerializeField] private TextMeshProUGUI stackMax;

    private void Start()
    {
        if (current== null)
        {
            current = this;
        }
        if (cam == null)
        {
            cam= Camera.main;
        }
    }

    void Update()
    {
        goldText.text = GameManager.current.playerGold.ToString("0");

        if (GameManager.current.speedMax)
        {
            speedMax.text = "Max";
        }

        if (GameManager.current.stackMax)
        {
            stackMax.text = "Max";
        }
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

    public void UiActivator()
    {
        UpgradePanel.SetActive(true);
        UpgradePanel.transform.DOMoveY(0, 1f);
    }

    public void UiDeactivator()
    {
        UpgradePanel.transform.DOMoveY(-960, 1f)
            .OnComplete((() => UpgradePanel.SetActive(false)));
    }
}
