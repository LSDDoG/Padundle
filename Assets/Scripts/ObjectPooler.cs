using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public GameObject pooledObject;
	[SerializeField]
	private int pooledAmount;

	private List<GameObject> pooledObjects;
	
	void Start ()
	{
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject go = (GameObject)Instantiate(pooledObject);
			go.SetActive(false);
			pooledObjects.Add(go);
		}
	}
	
	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		GameObject go = (GameObject)Instantiate(pooledObject);
		go.SetActive(false);
		pooledObjects.Add(go);
		return go;
	}
}
