using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public navigation_patrol npcScript;
    // A flag to check if the treasure was collected


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcScript.StartChasing(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if its linked treasure has been collected
            if (!npcScript.isTreasureCollected)
            {
                npcScript.StopChasing();
            }
        }
    }
}
