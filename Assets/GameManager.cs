using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private TreasureUIManager treasureUIManager;
    [SerializeField]
    private TextMeshProUGUI youWinText;
    private int deadNPCs = 0;
    // Start is called before the first frame update
    void Start()
    {
        treasureUIManager = FindObjectOfType<TreasureUIManager>();
        youWinText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
    }
    // Call this method when an NPC is defeated
    public void NotifyNPCDefeated()
    {
        deadNPCs++;
        Debug.Log("Total defeated: " + deadNPCs);

        // Check the win condition again when an NPC is defeated
        if (treasureUIManager.treasureCount >= 3 && deadNPCs >= 3)
        {
            youWin();
        }
    }

    private void youWin() {
        youWinText.enabled = true;
        Invoke("QuitGame", 5f);
    }
    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
