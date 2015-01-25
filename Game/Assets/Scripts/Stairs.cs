using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

	public GameObject targetTeleport;

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			//ScreenFade.main.FadeToBlack();
			//ScreenFade.main.NextLevelNext();

			// Teleport to next area
			col.gameObject.transform.position = targetTeleport.transform.position;

			Player.main.NextLevel();
		}
	}
}