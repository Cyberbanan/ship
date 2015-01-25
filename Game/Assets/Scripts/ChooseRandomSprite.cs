using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseRandomSprite : MonoBehaviour {

	public List<Sprite> sprites = new List<Sprite>();
	public static List<Sprite> activeSprites = new List<Sprite>();

	void Awake() {
		if(activeSprites.Count <= 0)
		{
			// Repopulate sprites
			activeSprites.AddRange(sprites);
		}

		int index = Random.Range(0, activeSprites.Count);
		GetComponent<SpriteRenderer>().sprite = activeSprites[index];
		activeSprites.RemoveAt(index);


	}
}