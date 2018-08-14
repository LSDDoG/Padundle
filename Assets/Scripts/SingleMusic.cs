using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMusic : MonoBehaviour
{
	private static SingleMusic instance = null;
	public static SingleMusic Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (SingleMusic)FindObjectOfType(typeof(SingleMusic));
			}
			return instance;
		}
	}

	void Awake ()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
