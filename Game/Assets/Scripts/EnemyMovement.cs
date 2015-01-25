using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	private const float MAX_ROTSPEED_PER_SEC = 60.0f;
	private const float ROTATE_OFFSET = -75.0f;
	public const float distToTriggerRotate = 2.5f;

	// Enemy movement
	private Player targetPlayer;
	private GameObject targetObject;
	private PlayerMovement pm;
	public float randomizedMovementFrequency = 0.01f;
	private float accelerationAmount = 1.0f;

	// Activation variables
	public bool activeByDefault = false;
	private bool isActive;
	public float distToActivate = 1.0f;
	public float activationDelay = 1.0f;

	// if the max distance is too far from the player
	// Need this because even if you're close, if you're stuck we
	// want you to teleport toward the player
	public float maxDistanceFromTarget = 5.0f;

	void Awake() {
		isActive = activeByDefault;
	}

	void Start() {
		targetPlayer = Player.main;
		targetObject = targetPlayer.gameObject;
		pm = targetPlayer.GetComponent<PlayerMovement>();
	}
	
	void FixedUpdate() {

		if (targetObject == null) {
			return;
		}

		// if you're not active, stay rotated away from player. Activate when they get close.
		if (!isActive) {
			audio.mute = true;
			rotateTowardsTarget(false);
			if (distToTarget() <= distToTriggerRotate && !pm.isInGlow()) {
				isActive = true;
			}
			return;
		}

		// Before you truly activate, give a short delay #creepylaugh
		activationDelay -= Time.deltaTime;
		if (activationDelay > 0) {
			rotateTowardsTarget(false);
			return;
		}

		audio.mute = false;

		// If active, make sure you lurch randomly
		if (Random.value <= randomizedMovementFrequency) {
			Vector3 randomDirection = new Vector3 (Random.value - 0.5f, 0.0f, Random.value - 0.5f).normalized * 10.0f;
			Debug.DrawRay (transform.position, randomDirection * 2, Color.yellow);

			Vector3 randomAcceleration = new Vector3 (randomDirection.x * accelerationAmount, 0.0f, randomDirection.z * accelerationAmount);
			rigidbody.AddForce (randomAcceleration, ForceMode.Impulse);
		}

		// Move toward the player if they're not in the glow
		if (!pm.isInGlow()) {
			Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
			Vector3 directionToTarget = vectorToTarget.normalized;
			Debug.DrawRay (transform.position, directionToTarget * 2, Color.white);
			Vector3 acceleration = new Vector3 (directionToTarget.x * accelerationAmount, 0.0f, directionToTarget.z * accelerationAmount);
			rigidbody.AddForce (acceleration, ForceMode.Impulse);

			// if we're really far from the player, teleport closer
			if (distToTarget() > maxDistanceFromTarget) {
				// pick a random X near the user.
				float targetX = (Random.value - 0.5f) * maxDistanceFromTarget;

				// get a y distance such that you are maxDistanceFromTarget/2 away
				float targetDistance = maxDistanceFromTarget/2;
				float targetDistanceSq = targetDistance * targetDistance;
				float targetZ = Mathf.Sqrt( targetDistanceSq - targetX * targetX);
				if (Random.value > 0.5) {
					targetZ = targetZ * -1;
				}

				float newX = targetX + targetObject.transform.position.x;
				float newZ = targetZ + targetObject.transform.position.z;

				transform.position = new Vector3(newX, 0.0f, newZ);
			}

		}

		// if we're close to the player, rotate to show the mouth #creepy
		if (distToTarget() > distToTriggerRotate || pm.isInGlow()) {
			// if we're far from the player, rotate to hide the mouth #creepy
			rotateTowardsTarget(false);
		} else {
			rotateTowardsTarget(true);
		}
	}

	float distToTarget() {
		if (targetObject) {
			Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
			float currentDistanceFromTarget = vectorToTarget.magnitude;
			return currentDistanceFromTarget;
		}
		return 9999.0f;
	}

	void rotateTowardsTarget(bool towardsOrAway) {
		if (targetObject) {
			Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
			Vector3 directionToTarget = vectorToTarget.normalized;

			// if we're close to the player, rotate to show the mouth #creepy
			Vector3 inputDir = directionToTarget;

			if (!towardsOrAway) {
				// if we're far from the player, rotate to hide the mouth #creepy
				inputDir = directionToTarget * -1;
			}
			float angleY = Mathf.Atan2(-inputDir.x, -inputDir.z) * Mathf.Rad2Deg + ROTATE_OFFSET;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90.0f, angleY, 0.0f), MAX_ROTSPEED_PER_SEC * Time.fixedDeltaTime);
		}
	}

}
