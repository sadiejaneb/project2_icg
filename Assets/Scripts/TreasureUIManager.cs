using UnityEngine;
using TMPro;

public class TreasureUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI treasureText;

    public int treasureCount = 0;

    private void Start()
    {
        // Initialize the UI text element
        treasureText.text = treasureCount.ToString();
    }

    public void UpdateTreasureCount()
    {
        treasureCount++;
        treasureText.text = treasureCount.ToString();

    }

}
