using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - target.position;
	}

	void Update ()
	{
		Vector3 newPos = target.position;
		newPos.y = transform.position.y;
		transform.position = newPos + offset;
	}
}
