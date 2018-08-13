using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
	[SerializeField]
	private Transform destructionPoint;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.gameObject.SetActive(false);
	}
}
