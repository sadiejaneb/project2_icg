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
    void Update()
    {
        if (TreasureManager.permanentlyPoweredUp)
        {
            infiniteEnergyImage.gameObject.SetActive(true); // Set the image active
        }
        else
        {
            infiniteEnergyImage.gameObject.SetActive(false); // Set the image inactive
        }
    }
}
