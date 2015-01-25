using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
	public GameObject Title;
	public HackyTextFade UpperText;
	public HackyTextFade LowerText;
	public float TimeAutoStart = 10.0f;

	enum State
	{
		Title,
		IntroText
	}

	State state = State.Title;
	float levelStartTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (state == State.Title)
		{
			if (Input.anyKeyDown)
			{
				state = State.IntroText;
				Title.SetActive(false);
				UpperText.Reset();
				LowerText.Reset();
				levelStartTimer = Time.time + TimeAutoStart;
			}
		}
		else
		{
			if (Time.time > levelStartTimer)
			{
				Application.LoadLevel("Level");
			}
		}
	}
}
