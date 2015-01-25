using UnityEngine;
using System.Collections;

public class HackyTextFade : MonoBehaviour {
	public float FadeDelay = 5.0f;
	public float FadeTime = 2.0f;
	float startedTime = 0.0f;
	// Use this for initialization
	void Start () {
		Reset ();
	}

	public void Reset() {
		startedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Mathf.Clamp((Time.time - startedTime - FadeDelay) / FadeTime, 0.0f, 1.0f) * Mathf.PI;
		float transparency = (Mathf.Cos (t) + 1) / 2.0f;
		var mesh = GetComponent<MeshRenderer> ();
		mesh.material.color = new Color (0, 0, 0, transparency);
	}
}
