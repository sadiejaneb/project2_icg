using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private LifeUIManager lifeUIManager;

    private void Start()
    {
        lifeUIManager = GameObject.Find("LifeUIManager").GetComponent<LifeUIManager>();
       
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy detected.");
            lifeUIManager.HandleEnemyCollision(); // Call without passing any arguments
        }
    }
}
