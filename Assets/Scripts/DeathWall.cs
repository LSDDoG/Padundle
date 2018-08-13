using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
	[SerializeField]
	private PaddleController paddle;
	[SerializeField]
	private float maxDistance = 5;
	[SerializeField]
	private float pitchRange = 0.2f;

	private AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		float h = paddle.HSpeed * 1.1f  * Time.deltaTime;
		if (paddle.transform.position.x - transform.position.x >= maxDistance)
		{
			h *= Mathf.Max(1, paddle.HSpeedMultiplier - 1.1f);
		}
		Vector2 moveAmount = new Vector2(h, 0);
		Vector3 newPos = transform.position + (Vector3)moveAmount;

		transform.position = newPos;

		audioSource.pitch = 0.8f + Random.Range(-pitchRange, pitchRange);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		GameManager.instance.GameOver();
	}
}
