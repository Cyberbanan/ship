using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider other) {
		/*if (other.tag == "Player") {
			// Kill the player
			PlayerMovement player = (PlayerMovement) other.gameObject.GetComponent(typeof(PlayerMovement));
			player.Die();
		}*/
	}
}
