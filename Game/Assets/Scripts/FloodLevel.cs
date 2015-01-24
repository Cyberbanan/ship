using UnityEngine;
using System.Collections;
using System;

public class FloodLevel : MonoBehaviour {

	public GameObject targetObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var rand = new System.Random();
		var moveWater = rand.Next(3) == 0;
		if (moveWater) {

		}
	}
}
