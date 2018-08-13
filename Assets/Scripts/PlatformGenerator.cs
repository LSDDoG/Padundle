using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private float distance = 20;

	[SerializeField]
	private float distanceMin;
	[SerializeField]
	private float distanceMax;

	private int platformSelector;
	private float[] platformWidths;
	//private float[] spikePlatformWidths;

	[SerializeField]
	private ObjectPooler[] platformPools;
	//[SerializeField]
	//private ObjectPooler[] spikePlatformPools;

	private float minHeight;
	[SerializeField]
	private Transform maxHeightPoint;
	private float maxHeight;
	[SerializeField]
	private float maxHeightChange;
	private float heightChange;

	private float lastWidth = 0f;

	void Start ()
	{
		platformWidths = new float[platformPools.Length];

		for (int i = 0; i < platformPools.Length; i++)
		{
			platformWidths[i] = platformPools[i].pooledObject.transform.GetChild(0).localScale.x;
		}

		// TODO
		/*spikePlatformWidths = new float[spikePlatformPools.Length];

		for (int i = 0; i < spikePlatformPools.Length; i++)
		{
			spikePlatformWidths[i] = spikePlatformPools[i].pooledObject.transform.GetChild(0).localScale.x;
		}
		*/

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
	}
	
	void Update ()
	{
		if(transform.position.x < spawnPoint.position.x)
		{
			distance = Mathf.Min(2 * lastWidth, Random.Range(distanceMin, distanceMax));
			platformSelector = Random.Range(0, platformPools.Length);

			heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);
			heightChange = Mathf.FloorToInt(Mathf.Clamp(heightChange, minHeight, maxHeight));

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2 + lastWidth / 2 + distance), heightChange, transform.position.z);

			lastWidth = platformWidths[platformSelector];

			GameObject newPlatform = platformPools[platformSelector].GetPooledObject();

			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);
		}
	}
}
