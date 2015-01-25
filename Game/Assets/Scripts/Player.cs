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

	public List<Transform> startingPoints = new List<Transform>();

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

		MusicManager.main.NextLevel();
	}
	
	public void PrevLevel()
	{
		currentLevel--;
		GetComponent<SpriteRenderer>().sprite = sprites[currentLevel];
	}

	public void Restart()
	{
		transform.position = startingPoints[currentLevel].position;
		GetComponent<PlayerMovement>().Revive();
	}
}