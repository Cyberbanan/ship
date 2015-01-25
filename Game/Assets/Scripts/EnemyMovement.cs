using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	private const float MAX_ROTSPEED_PER_SEC = 1000.0f;
	private const float ROTATE_OFFSET = -75.0f;

	// Enemy movement
	private Player targetPlayer;
	private GameObject targetObject;
	public float randomizedMovementFrequency = 0.01f;
	private float accelerationAmount = 1.0f;
	
	// if the max distance is too far from the player
	// Need this because even if you're close, if you're stuck we
	// want you to teleport toward the player
	public float maxDistanceFromTarget = 4.0f;
	

	void Awake() {
	}

	void Start() {
		targetPlayer = Player.main;
		targetObject = targetPlayer.gameObject;
	}
	
	void FixedUpdate() {

		// Add a random lurch
		if (Random.value <= randomizedMovementFrequency) {
			Vector3 randomDirection = new Vector3 (Random.value - 0.5f, 0.0f, Random.value - 0.5f).normalized * 10.0f;
			Debug.DrawRay (transform.position, randomDirection * 2, Color.yellow);

			Vector3 randomAcceleration = new Vector3 (randomDirection.x * accelerationAmount, 0.0f, randomDirection.z * accelerationAmount);
			rigidbody.AddForce (randomAcceleration, ForceMode.Impulse);
		}

		if (targetObject) {
			Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
			Vector3 directionToTarget = vectorToTarget.normalized;

			PlayerMovement pm = targetPlayer.GetComponent<PlayerMovement>();

			// Only move toward the player if they're not in the glow
			if (!pm.isInGlow()) {

				Debug.DrawRay (transform.position, directionToTarget * 2, Color.white);
				Vector3 acceleration = new Vector3 (directionToTarget.x * accelerationAmount, 0.0f, directionToTarget.z * accelerationAmount);
				rigidbody.AddForce (acceleration, ForceMode.Impulse);
			}

			/*
						// if we're too far from the player, teleport the enemy a bit closer
						float currentDistanceFromTarget = vectorToTarget.magnitude;
						if (currentDistanceFromTarget > maxDistanceFromTarget) {
						}

			*/
			// Rotate towards character
			Vector3 inputDir = directionToTarget;
			float angleY = Mathf.Atan2(-inputDir.x, -inputDir.z) * Mathf.Rad2Deg + ROTATE_OFFSET;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90.0f, angleY, 0.0f), MAX_ROTSPEED_PER_SEC * Time.fixedDeltaTime);
		}


	}

}
