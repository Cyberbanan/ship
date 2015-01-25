using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloodFade : MonoBehaviour {

	public float timeToFlood = 60.0f;
	public Text counterText;
	public float timeLeftToFlood;
	private bool isTimerActivated = false;
	private bool isPaused = true;

	public void activateTimer() {
		isTimerActivated = true;
		isPaused = false;
	}

	public void deactivateTimer() {
		isTimerActivated = false;
		isPaused = true;
	}

	public void setPause(bool status) {
		isPaused = status;
	}

	public void restart() {
		timeLeftToFlood = timeToFlood;
		activateTimer ();
	}

	// Use this for initialization
	void Start () {
		timeLeftToFlood = timeToFlood;
		renderer.material.SetColor ("_TintColor", new Color (0.5f, 0.5f, 0.5f, 0.0f));
		//gameObject.SetActive (false);
	}

	void FixedUpdate () {
		if (isTimerActivated) {
			if (!isPaused) {
				timeLeftToFlood -= Time.deltaTime;
			}

			if (timeLeftToFlood < 0) {
				timeLeftToFlood = 0;
				Player.main.GetComponent<PlayerMovement>().Die();
				restart ();
			}

			int minutes = (int)(Mathf.Floor (timeLeftToFlood / 60.0f));
			int seconds = (int)(timeLeftToFlood % 60.0f);
			string secondsStr = "";
			if (seconds < 10) {
					secondsStr += "0";
			}
			secondsStr += seconds;
			counterText.text = minutes + ":" + secondsStr;


			// Set the alpha depending on how much time is left.
			/*
			float timePassed = timeToFlood - timeLeftToFlood;
			float opacity = (timePassed / timeToFlood);

			float scaledOpacity = 0.0f;
			if (opacity >= 0.6f && opacity < 0.8f) {
					scaledOpacity = 0.6f;
			} else if (opacity >= 0.8f && opacity < 1) {
					scaledOpacity = 0.8f;
			} else if (opacity >= 1) {
					scaledOpacity = 1.0f;
			}

			scaledOpacity = scaledOpacity * 0.5f;
			renderer.material.SetColor ("_TintColor", new Color (0.5f, 0.5f, 0.5f, scaledOpacity));
			*/
		}
	}
}
