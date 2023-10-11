using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class ObjectCollision : MonoBehaviour
{
    [SerializeField]
    private Sprite[] energySprites;
    [SerializeField]
    private Image energyImage;
    [SerializeField]
    private PowerUpTimer powerUpTimer;


    private int energyCount = 0; // Ensure you have this variable declared
    private bool isPoweredUp = false;
    private float powerUpDuration = 10f;
    private float lastEnergyCollectedTime = 0f; // Track the time of the last energy collection
                                                
    private float energyDecrementInterval = 10f;// The time interval after which energy count will decrement
    public static bool PlayerIsPoweredUp = false;
    private EnergyRespawner respawner;
    private float powerUpEndTime;
    private void Start()
    {
        respawner = FindObjectOfType<EnergyRespawner>();
    }



    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a SimpleCollectibleScript
        SimpleCollectibleScript collectibleScript = other.GetComponent<SimpleCollectibleScript>();
        if (collectibleScript != null)
        {
                // Increase the energy count
                energyCount++;
            // Collect the energy without destroying it
            collectibleScript.Collect();
            CollectEnergy(other.gameObject);
            // Update the energy UI
            UpdateEnergyUI();
        }
    }

    void UpdateEnergyUI()
    {

        if (energyCount >= 0 && energyCount < energySprites.Length)
        {
            energyImage.sprite = energySprites[energyCount];
        }
        else if (energyCount >= energySprites.Length)
        {
            energyImage.sprite = energySprites[energySprites.Length - 1];
        }

        if (energyCount >= 3)
        {
            PowerUpPlayer();
        }
    }
    // Public method to check if player is powered up
    public bool IsPoweredUp()
    {
        return isPoweredUp;
    }
    public float GetPowerUpTimeLeft()
    {
        return powerUpEndTime - Time.time;
    }
    void PowerUpPlayer()
    {
        isPoweredUp = true;
        PlayerIsPoweredUp = true;
        if (powerUpTimer != null)
        {
            powerUpTimer.StartTimer(powerUpDuration);
        }
        StartCoroutine(PowerUpDuration());
    }

    IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(powerUpDuration);
        isPoweredUp = false;
        PlayerIsPoweredUp = false;
        // Reset the energy count here
        energyCount = 0;

        // Update the energy UI
        UpdateEnergyUI();
    }
    
    private void Update()
    {
        // Check if it's time to decrement energy count
        if (Time.time - lastEnergyCollectedTime >= energyDecrementInterval && energyCount > 0)
        {
            energyCount--;
            // Update the energy UI after decrementing
            UpdateEnergyUI();
        }

        // Check if energy count has reached zero, and reset the last collection time
        if (energyCount == 0)
        {
            lastEnergyCollectedTime = Time.time;
        }
    }
    // Called when an energy object is collected
    public void CollectEnergy(GameObject energyObject)
    {
        StartCoroutine(RespawnEnergy(energyObject));
    }

    private IEnumerator RespawnEnergy(GameObject energyObject)
    {
        yield return new WaitForSeconds(10f); // Adjust the respawn time as needed
        energyObject.SetActive(true);
        
        energyObject.GetComponent<SimpleCollectibleScript>().SetCollected(false);
    }
}

