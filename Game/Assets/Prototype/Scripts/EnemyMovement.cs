using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	private float accelerationAmount = 1.0f;
	public float maxSpeed = 0.5f;
	private float maxSpeedSqr;
	
	public float randomizedMovementFrequency = 0.3f;
	
	// what the enemy wants to move toward
	public GameObject targetObject;
	
	// used by the enemy's pathfinding (follow walls)
	private bool isColliding = false;
	
	// if the max distance is too far from the player
	public float maxDistanceFromTarget = 4.0f;
	public float teleportDistance = 0.1f;
	
	void Awake() {
		maxSpeedSqr = maxSpeed * maxSpeed;
	}
	
	void FixedUpdate() {
		Vector3 directionToTravel = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 vectorToTarget = (targetObject.transform.position - transform.position);
		
		if (!isColliding) {
			// don't need to path find, so move toward the target
			directionToTravel = vectorToTarget.normalized;
			Debug.DrawRay(transform.position, directionToTravel * 2, Color.white);
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
			Debug.DrawRay(transform.position, randomDirection * 2, Color.yellow);
		}
		
		// add the forces
		Vector3 acceleration = new Vector3 (directionToTravel.x * accelerationAmount, 0.0f, directionToTravel.z * accelerationAmount);
		rigidbody.AddForce (acceleration, ForceMode.Impulse);
		
		// Cap the speed
		if (rigidbody.velocity.sqrMagnitude > maxSpeedSqr) {
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}
	}
	
	void OnCollisionStay(Collision collisionInfo) {
		// Goal is to move parallel to the wall
		isColliding = true;
		
		foreach (ContactPoint contact in collisionInfo.contacts) {
			// get a parallel angle to the wall
			Vector3 dir = contact.normal.normalized;
			Vector3 rotateAway = Quaternion.Euler(0, -90, 0) * dir;
			
			Debug.DrawRay(transform.position, rotateAway * 2, Color.red);
			
			// add the force and cap speed
			rigidbody.AddForce(rotateAway.x * 5.0f * accelerationAmount, 0.0f, rotateAway.z * 5.0f * accelerationAmount);
			if (rigidbody.velocity.sqrMagnitude > maxSpeedSqr) {
				rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
			}
		}
	}
	
	void OnCollisionExit(Collision collisionInfo) {
		isColliding = false;
	}
	
}
