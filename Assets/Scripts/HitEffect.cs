using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
	
	void Update ()
	{
		transform.localScale += new Vector3(0.02f, 0.02f, 0);
		if (transform.localScale.x > 0.25f)
		{
			transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			transform.SetParent(null);
			gameObject.SetActive(false);
		}
	}
}
