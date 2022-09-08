using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager current;
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] private GameObject CloneImage;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private TextMeshProUGUI speedMax;
    [SerializeField] private TextMeshProUGUI stackMax;
    [SerializeField] private TextMeshProUGUI BarmenHire;
    [SerializeField] private TextMeshProUGUI WaiterHire;
    [SerializeField] private Button waiterButton;
    [SerializeField] private Button barmenButton;

    private void Awake()
    {
        if (current== null)
        {
            current = this;
        }
    }

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

        if (GameManager.current.speedMax)
        {
            speedMax.text = "Max";
        }

        if (GameManager.current.stackMax)
        {
            stackMax.text = "Max";
        }

        if (GameManager.current.barmenActive)
        {
            BarmenHire.text = "Hired";
            barmenButton.enabled = false;
        }
        
        if(GameManager.current.waiterUnlock)
        {
            WaiterHire.text = "$100";
            waiterButton.enabled = true;
            if (GameManager.current.waiterActive)
            {
                WaiterHire.text = "Hired";
                waiterButton.enabled = false;
            }
        }
        else
        {
            waiterButton.enabled = false;
            WaiterHire.text = "Lock";
        }
    }

    public void EarningMoney(Vector3 customerPos)
    {
        var instantiatePos = targetPos.position;
        Vector3 customerWorldPos = cam.WorldToScreenPoint(new Vector3(customerPos.x, customerPos.y, customerPos.z*-1));
        var coin = Instantiate(CloneImage, customerWorldPos, targetPos.rotation,targetPos.parent);

        coin.transform.DOScale(Vector3.one, .3f);
        coin.transform.DOMove(instantiatePos, .3f).OnComplete((() =>
        {
            coin.transform.DOKill();
            Destroy(coin);

        }));

    }
    
    public void SpendMoney()
    {
        var touchPos = Input.mousePosition;
        var instantiatePos = targetPos.position;
        var coin = Instantiate(CloneImage, instantiatePos, targetPos.rotation,targetPos.parent);

        coin.transform.DOScale(Vector3.one, .3f);
        coin.transform.DOMove(touchPos,.5f).OnComplete((() =>
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
