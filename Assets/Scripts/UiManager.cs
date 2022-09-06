using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager current;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject CloneImage;

    private void Awake()
    {
    }

    void Update()
    {
        goldText.text = GameManager.current.playerGold.ToString("0");
    }

    public void MoneyInstant()
    {
       // var clone = Instantiate(CloneImage, CloneImage.transform.position, CloneImage.transform.rotation);
    }
}
