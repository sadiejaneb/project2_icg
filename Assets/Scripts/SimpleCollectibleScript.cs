using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
	public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 };

	public CollectibleTypes CollectibleType;
	public bool rotate;
	public float rotationSpeed;
	public AudioClip collectSound;
	public GameObject collectEffect;

	private bool collected = false;


	private void Start()
	{
		collected = false;
	}

	void Update()
	{
		if (rotate)
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	public void Collect()
	{
		if (collected)
			return;
		
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
			
		if (collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		// Below is space to add in your code for what happens based on the collectible type
		switch (CollectibleType)
		{
			case CollectibleTypes.NoType:
				// Add code for NoType
				break;
			case CollectibleTypes.Type1:
				// Add code for Type1
				break;
			case CollectibleTypes.Type2:
				// Add code for Type2
				break;
			case CollectibleTypes.Type3:
				// Add code for Type3
				break;
			case CollectibleTypes.Type4:
				// Add code for Type4
				break;
			case CollectibleTypes.Type5:
				// Add code for Type5
				break;
		}

		collected = true;

		gameObject.SetActive(false);
	}
	public void SetCollected(bool isCollected)
	{
		collected = isCollected;
	}
}



