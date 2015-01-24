using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 10.0f;
	public float maxSpeed = 0.1f;
	private float maxSpeedSqr;

	void Awake()
	{
		maxSpeedSqr = maxSpeed * maxSpeed;
	}

	void Start()
	{
		
	}

	void FixedUpdate()
	{
		HandleInput();

		/*if(rigidbody.velocity.sqrMagnitude > maxSpeedSqr)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}*/
	}
	
	private void HandleInput()
	{
		Vector2 inputDir = Vector2.zero;

		inputDir.x = Input.GetAxis("Horizontal");
		inputDir.y = Input.GetAxis("Vertical");
		inputDir.Normalize();

		//rigidbody.AddForce(acceleration, ForceMode.Impulse);

		rigidbody.velocity = inputDir * moveSpeed;
	}
}