using UnityEngine;
using System.Collections;

public class FadeFlash : MonoBehaviour {

	public float FadeTimeScale = 0.3f;

	// Use this for initialization
	void Start () {
	
	}
	public float currentOpacity = 0;

	// Update is called once per frame
	void Update () {
		var mesh = GetComponent<MeshRenderer> ();
		currentOpacity = (Mathf.Cos (Time.time / FadeTimeScale) + 1) / 2.0f; 
		mesh.material.color = new Color (0, 0, 0, currentOpacity);
	}
}
