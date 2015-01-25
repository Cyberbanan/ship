using UnityEngine;
using System.Collections;

public class DadTrigger : MonoBehaviour {

	public GameObject Shadow;

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
			Shadow.SetActive(false);
			ScreenFade.main.FadeToWhite();
			ScreenFade.main.CreditsNext();
		}
	}
}
