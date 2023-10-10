using System.Collections.Generic;
using UnityEngine;

public class EnergyRespawner : MonoBehaviour
{
    public GameObject energyPrefab; // Prefab of the energy object
    public float respawnInterval = 10f; // Time interval for respawning each energy object

    private Dictionary<GameObject, float> objectsToRespawn = new Dictionary<GameObject, float>();

    private void Update()
    {
        List<GameObject> respawnedObjects = new List<GameObject>();

        // Iterate through each object that is waiting to be respawned
        foreach (var entry in objectsToRespawn)
        {
            if (Time.time >= entry.Value)
            {
                // Respawn object
                Instantiate(energyPrefab, entry.Key.transform.position, Quaternion.identity);
                respawnedObjects.Add(entry.Key);
            }
        }

        // Remove respawned objects from the dictionary
        foreach (var obj in respawnedObjects)
        {
            objectsToRespawn.Remove(obj);
            Destroy(obj);  // Destroy the original inactive object
        }
    }

    public void StartRespawnTimer(GameObject obj)
{
    if (objectsToRespawn.ContainsKey(obj))
    {
        objectsToRespawn.Remove(obj);
    }
    objectsToRespawn.Add(obj, Time.time + respawnInterval);
}
}
