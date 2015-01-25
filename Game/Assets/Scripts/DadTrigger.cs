using UnityEngine;
using System.Collections;

public class DadTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			// Aww... how sweet. You win! Hug! Hug! Hug!

			ScreenFade.main.FadeToWhite();
			ScreenFade.main.CreditsNext();
		}
	}
}
