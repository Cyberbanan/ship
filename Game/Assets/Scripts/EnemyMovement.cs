using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	// Enemy movement
	public GameObject targetObject;
	public float randomizedMovementFrequency = 0.01f;
	private float accelerationAmount = 1.0f;
	
	// if the max distance is too far from the player
	// Need this because even if you're close, if you're stuck we
	// want you to teleport toward the player
	public float maxDistanceFromTarget = 4.0f;
	

	void Awake() {
	}
	
	void FixedUpdate() {
		// Calculate out movement
		Vector3 directionToTravel = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector3 vectorToTarget = (targetObject.transform.position - transform.position);

		if (Random.value <= randomizedMovementFrequency) {
			Vector3 randomDirection = new Vector3 (Random.value - 0.5f, 0.0f, Random.value - 0.5f).normalized * 10.0f;
			Vector3 acceleration2 = new Vector3 (randomDirection.x * accelerationAmount, 0.0f, randomDirection.z * accelerationAmount);
			rigidbody.AddForce (acceleration2, ForceMode.Impulse);
			Debug.DrawRay (transform.position, randomDirection * 2, Color.yellow);
		}
		

	
			// don't need to path find, so move toward the target
			directionToTravel = vectorToTarget.normalized;
			Debug.DrawRay (transform.position, directionToTravel * 2, Color.white);

		
			// if we're too far from the player, teleport the enemy a bit closer
			float currentDistanceFromTarget = vectorToTarget.magnitude;
			if (currentDistanceFromTarget > maxDistanceFromTarget) {
			}

			// add the forces
			Vector3 acceleration = new Vector3 (directionToTravel.x * accelerationAmount, 0.0f, directionToTravel.z * accelerationAmount);
			rigidbody.AddForce (acceleration, ForceMode.Impulse);

	}

}
