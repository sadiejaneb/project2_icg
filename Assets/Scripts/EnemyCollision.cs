using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private LifeUIManager lifeUIManager;
    public AudioSource characterAudioSource;

    private void Start()
    {
        lifeUIManager = GameObject.Find("LifeUIManager").GetComponent<LifeUIManager>();
       
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
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
                Debug.Log("Playing audio clip: " + characterAudioSource.name);

                // Play the assigned audio clip
                characterAudioSource.Play();
            }
            Debug.Log("Enemy collision detected!");
            lifeUIManager.HandleEnemyCollision(); // Call without passing any arguments
        }
    }
}
