using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private const float MAX_ROTSPEED_PER_SEC = 1000.0f;

	private float accelerationAmount = 1.0f;
	private float maxSpeed = 1.0f;
	private float maxSpeedSqr;
	private bool dead = false;

	private Vector2 inputDir = Vector2.zero;

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

		if(rigidbody.velocity.sqrMagnitude > maxSpeedSqr)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}

		activateFloods();
	}

	public void Die()
	{
		Debug.Log ("DEATH");
		dead = true;
	}
	
	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			dead = false;
		}
		if (dead)
		{
			rigidbody.velocity = Vector3.zero;
			return;
		}

		// Receive input directions
		inputDir.x = Input.GetAxisRaw("Horizontal");
		inputDir.y = Input.GetAxisRaw("Vertical");

		if(inputDir.sqrMagnitude >= 0.01f)
		{
			// Normalize inputs
			inputDir.Normalize();

			// Apply force to the sprite
			Vector3 acceleration = new Vector3(inputDir.x * accelerationAmount, 0.0f, inputDir.y * accelerationAmount);
			rigidbody.AddForce(acceleration, ForceMode.Impulse);

			// Rotate sprite towards facing vector
			float angleY = Mathf.Atan2(-inputDir.x, -inputDir.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90.0f, angleY, 0.0f), MAX_ROTSPEED_PER_SEC * Time.fixedDeltaTime);
		}
		// Stop movement immediately if the player isn't pressing any directions
		else
		{
			rigidbody.velocity = Vector3.zero;
		}
	}

	void activateFloods()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
		foreach (Collider hit in hitColliders)
		{
			print (hit.gameObject.name);
			if (hit.gameObject.tag == "WaterTiled")
			{
				FloodFade ff = (FloodFade) hit.gameObject.GetComponent(typeof(FloodFade));
				ff.activateTimer();
			}
		}
	}
}