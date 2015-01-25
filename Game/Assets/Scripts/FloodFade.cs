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

		counterText.text = opacity + "%";

		opacity = opacity * 0.5f;
		print (opacity);

		renderer.material.SetColor ("_TintColor", new Color(0.5f, 0.5f, 0.5f, opacity));
	}
}
