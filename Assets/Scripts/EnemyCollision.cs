using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private LifeUIManager lifeUIManager;
    public AudioSource characterAudioSource;
    public AudioSource enemyAudioSource;
    private ObjectCollision objectCollision;
    private TreasureManager treasureManager;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        lifeUIManager = GameObject.Find("LifeUIManager").GetComponent<LifeUIManager>();
        objectCollision = GetComponent<ObjectCollision>();
        GameObject treasureManagerObject = GameObject.Find("YourTreasureManagerObjectName");

        // Check if the GameObject and component were found
        if (treasureManagerObject != null)
        {
            treasureManager = treasureManagerObject.GetComponent<TreasureManager>();
        }

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            if (objectCollision.IsPoweredUp() || TreasureManager.permanentlyPoweredUp)
            {
                if (gameManager != null)
                {
                    gameManager.NotifyNPCDefeated();
                }
                if (enemyAudioSource != null)
                {
                    // Play the assigned audio clip
                    enemyAudioSource.Play();
                }
                // Eliminate the guard
                hit.gameObject.SetActive(false);
            }
            else {
                this.gameObject.SetActive(false);
                Animator npcAnimator = hit.gameObject.GetComponent<Animator>();

                // Deactivate the Animator component
                if (npcAnimator != null)
                {
                    npcAnimator.enabled = false;
                }
                // Check if the audioSource field is assigned
                if (characterAudioSource != null)
                {
                    // Play the assigned audio clip
                    characterAudioSource.Play();
                }
                lifeUIManager.HandleEnemyCollision(); // Call without passing any arguments
            }
            
        }
    }
}
