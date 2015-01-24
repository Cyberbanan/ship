using UnityEngine;
using System.Collections;
using System;

public class FloodLevel : MonoBehaviour {

	public GameObject targetObject;
	public GameObject WaterPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var rand = new System.Random();
		var createWaterTile = rand.Next(3) == 0;
		if (createWaterTile) {
			var TileSize = LevelGenerator.TileSize;
			var playerPosition = targetObject.transform.position;
			// lock playerPosition to grid
			var playerX = Math.Floor(playerPosition.x / TileSize);
			var playerY = Math.Floor(playerPosition.y / TileSize);
			// pick a random tile in the 3x3 radius around the player

			int x = 0;
			int y = 0;
			while (!(x == 0 && y == 0)) {
				x = rand.Next(-3, 3);
				y = rand.Next(-3, 3);
			}
			var level = GameObject.Find("Level");
			GameObject obj = (GameObject)GameObject.Instantiate(WaterPrefab);
			obj.transform.position = new Vector3((float) playerX + x, 0, (float) playerY + y) * TileSize;
			obj.transform.parent = level.transform;
		}
	}
}
