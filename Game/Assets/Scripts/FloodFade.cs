using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloodFade : MonoBehaviour {

	public float timeToFlood = 120.0f;
	public Text counterText;
	private float timeLeftToFlood;

	// Use this for initialization
	void Start () {
		timeLeftToFlood = timeToFlood;
	}

	void FixedUpdate () {
		timeLeftToFlood -= Time.deltaTime;

		// Set the alpha depending on how much time is left.
		float timePassed = timeToFlood - timeLeftToFlood;
		float opacity = (timePassed / timeToFlood);
		counterText.text = (opacity*100) + "%";

		float scaledOpacity = 0.0f;
		if (opacity >= 0.6f && opacity < 0.8f) {
			scaledOpacity = 0.6f;
		} else if (opacity >= 0.8f && opacity < 1) {
			scaledOpacity = 0.8f;
		} else if (opacity >= 1) {
			scaledOpacity = 1.0f;
		}

		scaledOpacity = scaledOpacity * 0.5f;
		renderer.material.SetColor ("_TintColor", new Color(0.5f, 0.5f, 0.5f, scaledOpacity));
	}
}
