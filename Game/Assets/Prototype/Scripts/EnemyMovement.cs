using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private float accelerationAmount = 1.0f;
	private float maxSpeed = 1.0f;
	private float maxSpeedSqr;
	public GameObject targetObject;

	void Awake()
	{
		maxSpeedSqr = maxSpeed * maxSpeed;
	}

	void Start()
	{
		
	}

	void FixedUpdate()
	{
		Vector3 directionOfTarget = targetObject.transform.position - transform.position;
		print (directionOfTarget);
		directionOfTarget = directionOfTarget.normalized;
		
		Vector3 acceleration = new Vector3(directionOfTarget.x * accelerationAmount, 0.0f, directionOfTarget.z * accelerationAmount);
		rigidbody.AddForce(acceleration, ForceMode.Impulse);

		if(rigidbody.velocity.sqrMagnitude > maxSpeedSqr) {
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}
	}
}