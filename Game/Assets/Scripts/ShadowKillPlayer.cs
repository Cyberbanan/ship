using UnityEngine;
using System.Collections;

public class ShadowKillPlayer : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerMovement>().Die();
		}
	}
}