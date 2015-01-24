using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveAcceleration = 5.0f;
	public float maxSpeed = 10.0f;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	private void HandleUpdate()
	{
		Vector3 acceleration = Vector3.zero;

		acceleration.x = Input.GetAxis("Horizontal") * 5.0f;
		acceleration.z = Input.GetAxis("Vertical") * 5.0f;

		rigidbody.AddForce(acceleration, ForceMode.Acceleration);
	}
}