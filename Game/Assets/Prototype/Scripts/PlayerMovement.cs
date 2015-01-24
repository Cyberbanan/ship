﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float accelerationAmount = 1.0f;
	private float maxSpeed = 1.0f;
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

		if(rigidbody.velocity.sqrMagnitude > maxSpeedSqr)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}
	}
	
	private void HandleInput()
	{
		Vector2 inputDir = Vector2.zero;

		inputDir.x = Input.GetAxisRaw("Horizontal");
		inputDir.y = Input.GetAxisRaw("Vertical");

		if(inputDir.sqrMagnitude >= 0.01f)
		{
			inputDir.Normalize();

			Vector3 acceleration = new Vector3(inputDir.x * accelerationAmount, 0.0f, inputDir.y * accelerationAmount);

			rigidbody.AddForce(acceleration, ForceMode.Impulse);
		}
		else
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
}