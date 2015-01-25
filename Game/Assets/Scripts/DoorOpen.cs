using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}
}