using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : RaycastController
{
	private const int slideIndex = 0;
	private const int forwardIndex = 9;
	private const int jumpIndex = 17;

	[SerializeField]
	private Material jumpMaterial;
	[SerializeField]
	private Material forwardMaterial;
	[SerializeField]
	private Material slideMaterial;

	[SerializeField]
	private float vSpeed;
	[SerializeField]
	private float hSpeed;
	[SerializeField]
	private float maxHSpeed;
	[SerializeField]
	private float power;
	[SerializeField]
	private float hitCooldown = 1f;

	[SerializeField]
	private ObjectPooler hitEffectPool;

	[SerializeField]
	private SpriteRenderer arrowUp;
	[SerializeField]
	private SpriteRenderer arrowRight;
	[SerializeField]
	private SpriteRenderer arrowDown;

	private Color arrowsColor;

	[SerializeField]
	private float hitPitchRange = 0.2f;

	private float maxVerticalMovement;

	private float vSpeedMultiplier = 1f;
	private float hSpeedMultiplier = 1f;

	private float nextHit;

	private AudioSource audioSource;

	public float HSpeed
	{
		get { return hSpeed; }
	}
	public float HSpeedMultiplier
	{
		get { return hSpeedMultiplier; }
	}

	protected override void Start()
	{
		base.Start();

		maxVerticalMovement = Camera.main.orthographicSize + (boxCollider.bounds.size.y / 4);
		nextHit = Time.time;

		audioSource = GetComponent<AudioSource>();
		arrowsColor = arrowUp.color;

		StartCoroutine(SpeedUp());
	}

	private void Update()
	{
		arrowsColor.a = Mathf.Clamp01(Input.GetAxisRaw("Vertical"));
		arrowUp.color = arrowsColor;

		arrowsColor.a = Mathf.Clamp01(Input.GetAxis("Horizontal"));
		arrowRight.color = arrowsColor;

		arrowsColor.a = Mathf.Clamp01(-Input.GetAxisRaw("Vertical"));
		arrowDown.color = arrowsColor;

	}

	void FixedUpdate ()
	{
		Move();
	}
	public void Move()
	{
		CalculateRaySpacing();
		UpdateRaycastOrigins();

		hSpeedMultiplier = Input.GetAxis("Horizontal") + 1.1f;

		float v = Input.GetAxisRaw("Vertical") * vSpeed * vSpeedMultiplier * Time.deltaTime;
		float h = hSpeed * hSpeedMultiplier * Time.deltaTime;

		Vector2 moveAmount = new Vector2(h, v);

		HorizontalCollisions(ref moveAmount);

		Vector3 newPos = transform.position + (Vector3)moveAmount;
		newPos.y = Mathf.Clamp(newPos.y, -maxVerticalMovement, maxVerticalMovement);

		transform.position = newPos;
	}

	void HorizontalCollisions(ref Vector2 moveAmount)
	{
		float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

		if (Mathf.Abs(moveAmount.x) < skinWidth)
		{
			rayLength = 2 * skinWidth;
		}

		Rigidbody2D rb = null;
		int lastHitI = 0;

		Vector2 firstHitPos = Vector2.zero;
		Vector2 lastHitPos = Vector2.zero;

		for (int i = 0; i < horizontalRayCount; i++)
		{
			Vector2 rayOrigin = raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * rayLength, Color.red);

			if (hit)
			{
				if (hit.distance == 0)
				{
					if(firstHitPos == Vector2.zero)
					{
						firstHitPos = hit.point;
					}

					rb = hit.rigidbody;
					lastHitI = i;
					lastHitPos = hit.point;
				}
			}
		}

		if (rb != null)
		{
			HitPlayer(rb, lastHitI);
			HitEffect((firstHitPos + lastHitPos) / 2);
			HitSound();
		}
	}

	void HitPlayer(Rigidbody2D rb, int i)
	{
		if (nextHit <= Time.time)
		{
			Vector2 direction = Vector2.right;
			if (i >= jumpIndex)
			{
				rb.GetComponent<MeshRenderer>().material.color = jumpMaterial.color;
				direction.y = (i - jumpIndex) * (1.5f / 8) + 0.1f;
			}
			else if (i >= forwardIndex)
			{
				rb.GetComponent<MeshRenderer>().material.color = forwardMaterial.color;
			}
			else if (i >= slideIndex)
			{
				rb.GetComponent<MeshRenderer>().material.color = slideMaterial.color;
				direction.y = -((i - slideIndex) * (0.2f / 4) + 0.1f);
			}

			direction.Normalize();

			rb.AddForce(direction * power * hSpeed * (hSpeedMultiplier / 2f));
			rb.AddTorque(Random.Range(-2f, 2f));
			nextHit = Time.time + hitCooldown;
		}
	}

	void HitSound()
	{
		audioSource.pitch = 1.5f + Random.Range(-hitPitchRange, hitPitchRange);
		audioSource.Play();
	}

	void HitEffect(Vector3 pos)
	{
		pos.z = -2;
		GameObject newEffect = hitEffectPool.GetPooledObject();
		newEffect.transform.SetParent(transform);
		newEffect.transform.position = pos;
		newEffect.SetActive(true);
	}

	IEnumerator SpeedUp()
	{
		while(hSpeed < maxHSpeed)
		{
			hSpeed += 1/1000f;
			yield return new WaitForSeconds(1 / 1000f);
		}
	}
}
