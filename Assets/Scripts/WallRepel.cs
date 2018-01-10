using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRepel : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		Vector3 newVelocity = collision.relativeVelocity.normalized;
		newVelocity.x = -newVelocity.x * 2;
		newVelocity.y = -newVelocity.y * 2;
		newVelocity.z = -newVelocity.z * 2;
		collision.collider.attachedRigidbody.velocity = newVelocity;
	}
}
