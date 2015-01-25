using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public int width;
	public int height;
	public Tile[,] tiles;
	
	public LevelGrid(int width, int height)
	{
		tiles = new Tile[width, height];
		this.width = width;
		this.height = height;
	}
}

public class Room
{
	public int x;
	public int y;
	
	public int width;
	public int height;
	public int doorCount;
};

public class LevelGenerator : MonoBehaviour {
	public int GridWidth;
	public int GridHeight;
	public static float TileSize = 0.32f;
	
	public Tile Ground = new Tile (Tile.TileType.Ground);
	public Tile Wall = new Tile (Tile.TileType.Wall);
	public Tile Water = new Tile (Tile.TileType.Water);
	public Tile Door = new Tile (Tile.TileType.Door);
	
	public GameObject GroundPrefab;
	public GameObject WallPrefab;
	public GameObject WaterPrefab;
	public GameObject DoorPrefab;
	
	private Queue<Room> roomQueue;
	
	// TODO: This should probably be a heap
	private Queue<Room>	emptySpaces;
	
	private List<Room> finishedRooms;
	
	private System.Random rand;
	
	private LevelGrid grid;
	
	public bool RoomFits(Room space, Room room)
	{
		// TODO: Allow rotation?
		return (room.width <= space.width && room.height <= space.height);
	}
	
	public void ProcessRoom()
	{
		// Grab next room
		Room room = roomQueue.Dequeue ();
		
		// Go through rooms until something fits
		// TODO: Do this with a heap & allow rotations?
		Room space = new Room();
		for (int i = 1; i <= emptySpaces.Count; i++)
		{
			space = emptySpaces.Dequeue();
			if (RoomFits(space, room))
				break;
			emptySpaces.Enqueue(space);
			
			if (i == emptySpaces.Count)
			{
				// TODO: We failed. Couldn't find a space large enough.
				// Need to handle this more gracefuully since it could be an
				// ending room.
				return;
			}
		}
		
		// Pick a random corner to anchor the room in
		int[] xVals = {0, space.width - room.width};
		int[] yVals = {0, space.height - room.height};
		
		int leftRightSel = rand.Next (2);
		int upDownSel = rand.Next (2);
		room.x = xVals [leftRightSel];
		room.y = yVals [upDownSel];
		finishedRooms.Add (room);
		
		// Add remaining spaces if appropriate
		int hallwayWidth = 0;
		
		if (Random.value < 0.33f)
			hallwayWidth = 2;
		else
			hallwayWidth = 3;
		
		int remainingWidth = space.width - hallwayWidth - room.width;
		int remainingHeight = space.height - hallwayWidth - room.height;
		
		// Iterate over each quadrant of the space
		for (int y = 0; y < 2; y++)
		{
			for (int x = 0; x < 2; x++)
			{
				// Skip the used section
				if (x == leftRightSel && y == upDownSel)
					continue;
				
				Room extraSpace = new Room();
				
				// TODO: Avoid quarters and combine 2 of the areas?
				if (x == leftRightSel)
				{
					extraSpace.width = room.width;
					extraSpace.x = room.x;
				}
				else
				{
					extraSpace.width = remainingWidth;
					extraSpace.x = space.x;
				}
				
				if (y == upDownSel)
				{
					extraSpace.height = room.height;
					extraSpace.y = room.y;
				}
				else
				{
					extraSpace.height = remainingHeight;
					extraSpace.y = space.y;
				}
				
				if (room.width < 4 || room.height < 4)
				{
					// Ain't no one got a use for tiny rooms
					continue;
				}
				
				emptySpaces.Enqueue(extraSpace);
			}
		}
	}
	
	public void BakeRooms()
	{
		// Everything is carpet to start with.
		for (int y = 0; y < grid.height; y++)
		{
			for (int x = 0; x < grid.width; x++)
			{
				grid.tiles[x, y] = Ground;
			}
		}
		
		foreach (Room room in finishedRooms)
			BakeRoom(room);
	}
	
	public void BakeRoom(Room room)
	{
		// Number of non-corner walls
		int wallCount = room.width * 2 + room.height * 2 - 8;
		
		int[] doorIndices = new int[room.doorCount];
		
		for (int i = 0; i < room.doorCount; i++)
		{
			bool foundGoodLocation = false;
			
			int doorLocation = 0;
			while (!foundGoodLocation)
			{
				foundGoodLocation = true;
				doorLocation = rand.Next(wallCount);
				
				// Make sure the doors are spread out
				for (int j = 0; j < i; j++)
				{
					if (Mathf.Abs(doorLocation - doorIndices[j]) <= 1)
					{
						foundGoodLocation = false;
					}
				}
			}
			
			doorIndices[i] = doorLocation;
		}
		
		int wallIndex = 0;
		for (int y = room.y; y < room.y + room.height; y++)
		{
			for (int x = room.x; x < room.x + room.width; x++)
			{
				bool topBottom = (y == room.y || y == (room.y + room.height - 1));
				bool leftRight = (x == room.x || x == (room.x + room.width - 1));
				
				if (topBottom || leftRight)
				{
					// Room wall
					grid.tiles[x, y] = Wall;
					if (topBottom != leftRight)
					{
						// Non-corner
						foreach (int doorIndex in doorIndices)
						{
							if (doorIndex != wallIndex)
								continue;
							
							grid.tiles[x, y] = Door;
						}
						
						wallIndex++;
					}
				}
				else
				{
					// Carpet
					// TODO: Get some special in-room tile
				}
			}
		}
	}
	
	public void Generate()
	{
		grid = new LevelGrid (GridWidth, GridHeight);
		
		// TODO: Probably not the best place for these bindings
		Ground.prefab = GroundPrefab;
		Wall.prefab = WallPrefab;
		Water.prefab = WaterPrefab;
		Door.prefab = DoorPrefab;

		// Generate the level layout
		rand = new System.Random (1);
		
		finishedRooms = new List<Room> ();
		roomQueue = new Queue<Room> ();
		emptySpaces = new Queue<Room> ();
		
		Room startingSpace = new Room ();
		startingSpace.x = 0;
		startingSpace.y = 0;
		startingSpace.width = GridWidth;
		startingSpace.height = GridHeight;
		emptySpaces.Enqueue (startingSpace);
		
		// Create some rooms 'n' stuff
		for (int i = 0; i < 32; i++)
		{
			Room room = new Room();
			room.width = rand.Next(4, 10);
			room.height = rand.Next(4, 10);
			roomQueue.Enqueue(room);
		}
		
		// Proccess the rooms
		for (int i = 0; i < 32; i++)
		{
			ProcessRoom();
		}
		
		BakeRooms ();
		
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
