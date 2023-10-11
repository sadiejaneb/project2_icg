using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class TreasureManager : MonoBehaviour
{
    private TreasureUIManager treasureUIManager;

    public delegate void TreasureCollectedAction();
    public static event TreasureCollectedAction OnTreasureCollected;
    public static bool permanentlyPoweredUp = false;
    public bool rotate;
    public float rotationSpeed;
    public AudioClip collectSound;
    public GameObject collectEffect;

    private bool collected = false;
    public navigation_patrol linkedNPC;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        // Find the TreasureUIManager instance in the scene
        treasureUIManager = FindObjectOfType<TreasureUIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        Collect();
        if (treasureUIManager != null)
        {
            treasureUIManager.UpdateTreasureCount();
            // Check for the power-up condition here and trigger any related game logic if needed
            if (treasureUIManager.treasureCount >= 3 && !permanentlyPoweredUp)
            {
                permanentlyPoweredUp = true;
                Debug.Log("All chests collected. PermanentlyPoweredUp is set to true.");
            }
        }
        gameManager.NotifyChestCollected();

        TriggerKeepChasing(other);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Collect()
    {
        if (collected)
            return;

        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            
       
        collected = true;

        gameObject.SetActive(false);
    }

        private void TriggerKeepChasing(Collider other)
    {
       
            // Notify linkedNPC if available
            if (linkedNPC)
            {
                linkedNPC.NotifyTreasureCollected();
                linkedNPC.keepChasing = true;
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                linkedNPC.StartChasing(playerTransform);
            }
        }
}
