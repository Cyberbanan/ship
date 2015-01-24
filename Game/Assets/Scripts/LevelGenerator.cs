using UnityEngine;
using System.Collections;

public struct Tile {
	public enum TileType {
		Ground,
		Water,
		Wall,
		Door
	}

	public Tile(TileType tileType)
	{
		this.tileType = tileType;
		prefab = null;
	}

	public TileType tileType;
	public GameObject prefab;
}

public class LevelGrid
{
	public Tile[,] tiles;



	public LevelGrid(int width, int height)
	{
		tiles = new Tile[width, height];
	}
}

public class LevelGenerator : MonoBehaviour {
	public int GridWidth;
	public int GridHeight;
	public float TileSize = 0.32f;

	public Tile Ground = new Tile (Tile.TileType.Ground);
	public Tile Wall = new Tile (Tile.TileType.Wall);
	public Tile Water = new Tile (Tile.TileType.Water);
	public Tile Door = new Tile (Tile.TileType.Door);

	public GameObject GroundPrefab;
	public GameObject WallPrefab;
	public GameObject WaterPrefab;
	public GameObject DoorPrefab;

	public void Generate()
	{
		LevelGrid grid = new LevelGrid (GridWidth, GridHeight);

		// TODO: Probably not the best place for these bindings
		Ground.prefab = GroundPrefab;
		Wall.prefab = WallPrefab;
		Water.prefab = WaterPrefab;
		Door.prefab = DoorPrefab;

		Tile[] tileChoices = {
			Ground,
			Water,
			Wall,
			Door
		};
		
		// Generate the level layout
		var rand = new System.Random (1);		
		for (int y = 0; y < GridHeight; y++)
		{
			for (int x = 0; x < GridWidth; x++)
			{
				// TODO: Weight these choices in some way
				grid.tiles[x, y] = tileChoices[rand.Next(tileChoices.Length)];
			}
		}

		// Convert the level layout to game objects
		GameObject level = new GameObject("Level");
		for (int y = 0; y < GridHeight; y++)
		{
			for (int x = 0; x < GridWidth; x++)
			{
				GameObject obj = (GameObject)GameObject.Instantiate(grid.tiles[x, y].prefab);
				obj.transform.position = new Vector3(x, 0, y) * TileSize;
				obj.transform.parent = level.transform;
			}
		}
	}
}
