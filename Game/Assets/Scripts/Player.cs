using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	// Singleton
	private static Player instance = null;
	public static Player main
	{
		get { return instance; }
	}

	public List<Sprite> sprites = new List<Sprite>();
	
	[System.NonSerialized]
	public int currentLevel = 0;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public void NextLevel()
	{
		currentLevel++;
		GetComponent<SpriteRenderer>().sprite = sprites[currentLevel];
	}
	
	public void PrevLevel()
	{
		currentLevel--;
		GetComponent<SpriteRenderer>().sprite = sprites[currentLevel];
	}
}