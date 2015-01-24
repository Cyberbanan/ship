using UnityEngine;
using System.Collections;

public class WallResize : MonoBehaviour {

	private float currentScale = 1.0f;
	private const float GROWTH_FACTOR = 0.01f;
	private const float MAX_SCALE = 1.7f;

	void Update()
	{
		if(transform.localScale.x < MAX_SCALE)
		{
			currentScale += GROWTH_FACTOR * Time.deltaTime;
			transform.localScale = new Vector3(transform.localScale.x * currentScale, transform.localScale.y * currentScale, transform.localScale.z);
		}
		else
		{
			currentScale = MAX_SCALE;
			transform.localScale = new Vector3(MAX_SCALE, MAX_SCALE, transform.localScale.z);
			enabled = false;
		}
	}
}