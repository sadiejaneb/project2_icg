using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public navigation_patrol npcScript;  // Drag and drop the NPC's navigation_patrol script here in the inspector.

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the zone: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            npcScript.StartChasing(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited the zone: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            npcScript.StopChasing();
        }
    }

}
