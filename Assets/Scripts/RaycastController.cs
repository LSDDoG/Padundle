using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
	protected const float skinWidth = .015f;

	[SerializeField]
	protected LayerMask collisionMask;

	[SerializeField]
	protected int horizontalRayCount;

	protected float horizontalRaySpacing;

	protected BoxCollider2D boxCollider;
	protected RaycastOrigins raycastOrigins;

	protected virtual void Awake()
	{
		boxCollider = GetComponent<BoxCollider2D>();
	}

	protected virtual void Start()
	{
		CalculateRaySpacing();
	}

	public void UpdateRaycastOrigins()
	{
		Bounds bounds = boxCollider.bounds;
		bounds.Expand(skinWidth * -2);

		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	public void CalculateRaySpacing()
	{
		Bounds bounds = boxCollider.bounds;
		bounds.Expand(skinWidth * -2);

		float boundsHeight = bounds.size.y;

		horizontalRaySpacing = boundsHeight / (horizontalRayCount - 1);
	}

	public struct RaycastOrigins
	{
		public Vector2 topRight;
		public Vector2 bottomRight;
	}
}
