using UnityEngine;
using System.Collections;

public class HugController : MonoBehaviour {

	public float StartDelayTime;
	public float FadeInTime;
	public float HoldTime;
	public float FadeOutTime;

	public MeshRenderer FadePlane;

	float lastTimestamp;

	enum State {
		FadeIn,
		Hold,
		FadeOut
	}

	State state = State.FadeIn;

	// Use this for initialization
	void Start () {
		Reset ();
	}

	public void Reset() {
		lastTimestamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.FadeIn)
		{
			float t = Mathf.Clamp((Time.time - lastTimestamp - StartDelayTime) / FadeInTime, 0, 1) * Mathf.PI;
			float alpha = (Mathf.Cos(t) + 1.0f) / 2.0f;
			FadePlane.material.color = new Color(1, 1, 1, alpha);

			if (Time.time > lastTimestamp + StartDelayTime + FadeInTime)
			{
				state = State.Hold;
				lastTimestamp = Time.time;
			}
		}
		else if (state == State.Hold)
		{
			if (Time.time > lastTimestamp + HoldTime)
			{
				state = State.FadeOut;
				lastTimestamp = Time.time;
			}
		}
		else if (state == State.FadeOut)
		{
			float t = Mathf.Clamp((Time.time - lastTimestamp) / FadeOutTime, 0, 1) * Mathf.PI;
			float alpha = 1.0f - (Mathf.Cos(t) + 1.0f) / 2.0f;
			FadePlane.material.color = new Color(1, 1, 1, alpha);
			
			if (Time.time > lastTimestamp + FadeOutTime)
			{
				Application.LoadLevel("CreditScreen");
			}
		}
	}
}
