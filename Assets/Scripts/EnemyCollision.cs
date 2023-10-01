using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private LifeUIManager lifeUIManager;

    private void Start()
    {
        lifeUIManager = GameObject.Find("LifeUIManager").GetComponent<LifeUIManager>();

        if (lifeUIManager == null)
        {
            Debug.LogError("LifeUIManager not found.");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            lifeUIManager.HandleEnemyCollision(); // Call without passing any arguments
        }
    }
}
