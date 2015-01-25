using UnityEngine;
using System.Collections;

public class CreditController : MonoBehaviour {

	public GameObject NamesText;
	public GameObject GameJamText;

	public float StartingHeight;
	public float EndingHeight;
	public float ScrollSpeed;
	public float StartDelay;
	public float ShowNamesTime;
	public float NamesFadeTime;
	public float GameJamFadeTime;
	public float PauseBetweenTextTime;
	public float FinalTimeToReset;

	public float EaseOutScrollStart = 0.8f;

	public MeshRenderer FadePlane;

	float startTime;

	enum State {
		Scrolling,
		Waiting,
		FadeOut,
		Pause,
		FadeIn,
		WaitToReset
	}

	State state = State.Scrolling;

	// Use this for initialization
	void Start () {
		Reset ();
	}

	public void Reset() {
		startTime = Time.time;
	}

	float lastTimestamp = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if (state == State.Scrolling)
		{
			float scrollProgress = Mathf.Clamp ((Time.time - startTime - StartDelay) * ScrollSpeed, 0, 1);

			if (scrollProgress >= 1)
			{
				lastTimestamp = Time.time;
				state = State.Waiting;
			}


			if (scrollProgress > EaseOutScrollStart)
			{
				float easeOutLen = 1.0f - EaseOutScrollStart;
				float angle = (scrollProgress - EaseOutScrollStart) / easeOutLen;
				angle *= Mathf.PI / 4.0f;
				float angleOffset = Mathf.PI * 1.25f;
				angle += angleOffset;
				float sinOffset = Mathf.Sin(angleOffset);
				float easing = (-Mathf.Sin (angle) + sinOffset) / -sinOffset;
				scrollProgress = EaseOutScrollStart + easing * easeOutLen;
			}

			float height = StartingHeight * (1.0f - scrollProgress) + EndingHeight * scrollProgress;

			Vector3 namesPos = NamesText.transform.position;
			namesPos.y = height;
			NamesText.transform.position = namesPos;
		}
		else if (state == State.Waiting)
		{
			if (Time.time > lastTimestamp + ShowNamesTime)
			{
				lastTimestamp = Time.time;
				state = State.FadeOut;
			}
		}
		else if (state == State.FadeOut)
		{
			float t = Mathf.Clamp((Time.time - lastTimestamp) / NamesFadeTime, 0, 1) * Mathf.PI;
			float alpha = 1.0f - (Mathf.Cos(t) + 1.0f) / 2.0f;
			FadePlane.material.color = new Color(1, 1, 1, alpha);

			if (Time.time > lastTimestamp + NamesFadeTime)
			{
				state = State.Pause;
				GameJamText.SetActive(true);
				NamesText.SetActive(false);
				lastTimestamp = Time.time;
			}
		}
		else if (state == State.Pause)
		{
			if (Time.time > lastTimestamp + PauseBetweenTextTime)
			{
				state = State.FadeIn;
				lastTimestamp = Time.time;
			}
		}
		else if (state == State.FadeIn)
		{
			float t = Mathf.Clamp((Time.time - lastTimestamp) / GameJamFadeTime, 0, 1) * Mathf.PI;
			float alpha = (Mathf.Cos(t) + 1.0f) / 2.0f;
			FadePlane.material.color = new Color(1, 1, 1, alpha);

			if (Time.time > lastTimestamp + GameJamFadeTime)
			{
				state = State.WaitToReset;
				lastTimestamp = Time.time;
			}
		}
		else if (state == State.WaitToReset)
		{
			if (Input.anyKeyDown || Time.time > lastTimestamp + FinalTimeToReset)
			{
				Destroy(GameObject.Find("OutroMusic"));
				Application.LoadLevel("TitleScreen");
			}
		}
	}
}
