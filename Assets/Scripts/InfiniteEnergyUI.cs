using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class InfiniteEnergyUI : MonoBehaviour
{
    [SerializeField]
    private Image infiniteEnergyImage;

    // Start is called before the first frame update
    void Start()
    {
        // Set the image to inactive initially
        infiniteEnergyImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check the isPermanentlyPoweredUp boolean from TreasureManager
        if (TreasureManager.permanentlyPoweredUp)
        {
            // Set the image active if permenantly powered up
            infiniteEnergyImage.gameObject.SetActive(true);
        }
        else
        {
            // Set the image inactive if is not permenantly powered up
            infiniteEnergyImage.gameObject.SetActive(false);
        }
    }

}