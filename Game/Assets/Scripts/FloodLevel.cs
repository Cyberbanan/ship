using UnityEngine;
using System.Collections;

public class FloodLevel : MonoBehaviour {

	public GameObject targetObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float speed = Random.Range (0, 0.02f);
		transform.Translate(-speed, 0, 0);
	}
}
