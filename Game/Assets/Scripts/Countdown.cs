using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {
	public Font textFont;
	public int textSize;
	public float countdownStart = 150.0f;
	private float countdown;

	// Use this for initialization
	void Start () {
		countdown = countdownStart;
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0) {
			countdown = 0;
		}
	}

	void OnGUI () {

		int minutes = (int) (Mathf.Floor(countdown / 60.0f));
		int seconds = (int) (countdown % 60.0f);
		string secondsStr = "";
		if (seconds < 10) {
			secondsStr += "0";
		}
		secondsStr += seconds;

		GUIStyle centeredTextStyle = new GUIStyle("label");
		centeredTextStyle.alignment = TextAnchor.MiddleCenter;
		GUI.skin.font = textFont;
		GUI.skin.label.fontSize = textSize;
		centeredTextStyle.normal.textColor = new Color (1f, 1f, 1f, 0.2f);
		GUI.Label(new Rect(0, Screen.height/4, Screen.width, Screen.height), minutes+":"+secondsStr, centeredTextStyle);
	}
}
