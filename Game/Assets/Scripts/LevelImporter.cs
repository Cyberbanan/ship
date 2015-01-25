using UnityEngine;
using System.Collections;

public class LevelImporter : MonoBehaviour {

	public Tile Ground = new Tile (Tile.TileType.Ground);
	public Tile Wall = new Tile (Tile.TileType.Wall);
	public Tile Water = new Tile (Tile.TileType.Water);
	public Tile Door = new Tile (Tile.TileType.Door);
	
	public GameObject GroundPrefab;
	public GameObject WallPrefab;
	public GameObject WaterPrefab;
	public GameObject DoorPrefab;

	public float TileSize = 0.32f;

	public Texture2D texture;

	public bool IsCloseColor(Color c1, Color c2)
	{
		float maxDist = 0.2f;

		Vector3 v1 = new Vector3 (c1.r, c1.g, c1.b);
		Vector3 v2 = new Vector3 (c2.r, c2.g, c2.b);

		float dist = Vector3.Distance (v1, v2);

		return dist < maxDist;
	}

	public void Import()
	{
		// TODO: Probably not the best place for these bindings
		Ground.prefab = GroundPrefab;
		Wall.prefab = WallPrefab;
		Water.prefab = WaterPrefab;
		Door.prefab = DoorPrefab;
		Tile NullTile = new Tile ();
		NullTile.prefab = null;

		int width = texture.width;
		int height = texture.height;
		LevelGrid grid = new LevelGrid (width, height);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				Color pixel = texture.GetPixel(x, y);

				// TODO: Should probably check distances, rather than absolute values
				if (IsCloseColor(pixel, Color.black))
				{
					grid.tiles[x, y] = Wall;
				}
				else if (IsCloseColor(pixel, Color.red))
				{
					grid.tiles[x, y] = Door;
				}
				else if (IsCloseColor(pixel, Color.white))
				{
					//grid.tiles[x, y] = Ground;
					grid.tiles[x, y] = NullTile;
				}
				else if (IsCloseColor(pixel, Color.blue))
				{
					grid.tiles[x, y] = Water;
				}
				else
				{
					Debug.LogError("Could not find match for color " + pixel + " at " + x
					               + ", " + y + "! Aborting!");
				}
			}
		}

		// Convert the level layout to game objects
		GameObject level = new GameObject("Level");

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (grid.tiles[x, y].prefab == null)
					continue;

				GameObject obj = (GameObject)GameObject.Instantiate(grid.tiles[x, y].prefab);
				obj.transform.position = new Vector3(x, 0, y) * TileSize;
				obj.transform.parent = level.transform;
			}
		}
	}
}
