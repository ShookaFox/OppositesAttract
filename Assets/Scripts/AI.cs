using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public Transform Character; // Target Object to follow
	public float speed=0.1F; // Enemy speed
	private Vector3 directionOfCharacter;

	void Update() {
		directionOfCharacter = Character.transform.position - transform.position;
		directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards
		transform.Translate (directionOfCharacter * speed, Space.World);
	}
}