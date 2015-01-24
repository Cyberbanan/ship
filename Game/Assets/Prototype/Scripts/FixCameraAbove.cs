using UnityEngine;
using System.Collections;

public class FixCameraAbove : MonoBehaviour {

	public GameObject target;
	public Vector3 positionOffset = new Vector3(0.0f, 8.0f, 0.0f);

	void Start()
	{
		transform.position = target.transform.position;
	}

	void LateUpdate()
	{
		transform.position = target.transform.position + positionOffset;
	}
}