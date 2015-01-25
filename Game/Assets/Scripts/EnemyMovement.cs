using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	// Enemy movement
	public GameObject targetObject;
	public float maxSpeed = 0.5f;
	public float randomizedMovementFrequency = 0.3f;
	private float accelerationAmount = 1.0f;
	private float maxSpeedSqr;
	
	// enemy's pathfinding (follow walls)
	private bool isColliding = false;
	
	// if the max distance is too far from the player
	// Need this because even if you're close, if you're stuck we
	// want you to teleport toward the player
	public float maxDistanceFromTarget = 4.0f;
	public float teleportDistance = 0.1f;

	// setting water collision flags
	/*
    // respawn flags
	public float teleportToWaterMaxDistance = 3.0f;
	public float teleportToWaterMinDistance = 1.0f;
	public float respawnTimeoutMax = 0.1f;
	private float respawnTimeout;
	
	private bool isActive;
	private float waterTriggerTimeout;
	private float waterTriggerTimeoutMax = 0.05f;
	*/
	
	void Awake() {
		maxSpeedSqr = maxSpeed * maxSpeed;
		/*
		isActive = true;
		waterTriggerTimeout = waterTriggerTimeoutMax;
		*/
	}
	
	void FixedUpdate() {
		/*
		waterTriggerTimeout -= Time.deltaTime;
		respawnTimeout -= Time.deltaTime;

		if (waterTriggerTimeout < 0 && isActive) {
			// need && isActive otherwise it'll keep calling setActive(false) repeatedly
			setActive(false);
		}
		*/

		/*
		if (isActive) {
		*/

			// Calculate out movement
			Vector3 directionToTravel = new Vector3 (0.0f, 0.0f, 0.0f);
			Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
		
			if (!isColliding) {
				// don't need to path find, so move toward the target
				directionToTravel = vectorToTarget.normalized;
				Debug.DrawRay (transform.position, directionToTravel * 2, Color.white);
			}
		
			// if we're too far from the player, teleport the enemy a bit closer
			float currentDistanceFromTarget = vectorToTarget.magnitude;
			if (currentDistanceFromTarget > maxDistanceFromTarget) {
				transform.position = transform.position + directionToTravel * teleportDistance;
				isColliding = false;
			}
		
			if (Random.value <= randomizedMovementFrequency) {
				// Add a random force
				Vector3 randomDirection = new Vector3 (Random.value - 0.5f, 0.0f, Random.value - 0.5f).normalized;
				directionToTravel += randomDirection;
				Debug.DrawRay (transform.position, randomDirection * 2, Color.yellow);
			}
		
			// add the forces
			Vector3 acceleration = new Vector3 (directionToTravel.x * accelerationAmount, 0.0f, directionToTravel.z * accelerationAmount);
			rigidbody.AddForce (acceleration, ForceMode.Impulse);
		
			// Cap the speed
			if (rigidbody.velocity.sqrMagnitude > maxSpeedSqr) {
				rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
			}
		/*
		} else {
			// Respawn
			if (respawnTimeout < 0) {
				Collider waterNearTarget = RandomNearbyWater(targetObject.transform.position, teleportToWaterMaxDistance, teleportToWaterMinDistance);
				if (waterNearTarget != null) {
					transform.position = waterNearTarget.transform.position; // will set active on trigger enter
				}
			}
		}
		*/
	}

	/**
	 * Path finding along walls
	 * */
	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.gameObject.tag == "Wall") {
			// Goal is to move parallel to the wall
			isColliding = true;
		
			foreach (ContactPoint contact in collisionInfo.contacts) {
				// get a parallel angle to the wall
				Vector3 dir = contact.normal.normalized;
				Vector3 rotateAway = Quaternion.Euler (0, -90, 0) * dir;
			
				Debug.DrawRay (transform.position, rotateAway * 2, Color.red);
			
				// add the force and cap speed
				rigidbody.AddForce (rotateAway.x * 5.0f * accelerationAmount, 0.0f, rotateAway.z * 5.0f * accelerationAmount);
				if (rigidbody.velocity.sqrMagnitude > maxSpeedSqr) {
					rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
				}
			}
		}
	}

	// If you're no longer colliding against walls
	void OnCollisionExit(Collision collisionInfo) {
		if (collisionInfo.gameObject.tag == "Wall") {
			isColliding = false;
		}
	}

	/**
	 * Returns a water tile that is <= radius and >= min distance
	 * */
	/*
	Collider RandomNearbyWater(Vector3 center, float maxRadius, float minRadius) {
		Collider[] hitColliders = Physics.OverlapSphere(center, maxRadius);
		List<Collider> waterColliders = new List<Collider>();
		foreach (Collider hit in hitColliders) {
			if (hit.gameObject.tag == "Water") {
				if ((hit.transform.position - targetObject.transform.position).magnitude >= minRadius) {
					waterColliders.Add(hit);
				}
			}
		}
		if (waterColliders.Count > 0) {
			return waterColliders [Random.Range (0, waterColliders.Count)];
		} else {
			return null;
		}
	}
	*/

	/**
	 * Water detection and only being active when on water
	 * */
	/*
	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Water") {
			setActive(true);
		}
	}
	*/

	/*
	void setActive(bool status) {
		print ("setActive(" + status + ")");
		isActive = status;
		gameObject.renderer.GetComponent<SpriteRenderer> ().enabled = isActive;
		if (!status) {
			respawnTimeout = respawnTimeoutMax;
		} else {
			waterTriggerTimeout = waterTriggerTimeoutMax;
		}
	}
	*/
	
}
