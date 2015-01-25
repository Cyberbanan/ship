using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseRandomSprite : MonoBehaviour {

	public List<Sprite> sprites = new List<Sprite>();
	public List<string> messages = new List<string>();
	public static List<Sprite> activeSprites = new List<Sprite>();
	public static List<string> activeMessages = new List<string>();

	void Awake() {
		if(activeSprites.Count <= 0)
		{
			// Repopulate sprites
			activeSprites.AddRange(sprites);
			activeMessages.AddRange(messages);
		}

		int index = Random.Range(0, activeSprites.Count);

		GetComponent<SpriteRenderer>().sprite = activeSprites[index];
		activeSprites.RemoveAt(index);

		MemoryObjectText mot = (MemoryObjectText) GetComponent<MemoryObjectText>();
		if (mot) {
			mot.setObjectText (activeMessages [index]);
		}
		activeMessages.RemoveAt(index);
	}
}