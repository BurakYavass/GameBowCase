using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    
    void Update()
    {
        goldText.text = GameManager.current.playerGold.ToString("0");
    }
}
