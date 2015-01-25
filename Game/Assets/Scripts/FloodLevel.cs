using UnityEngine;
using System.Collections;

public class FloodLevel : MonoBehaviour {

	public GameObject waterPrefab;
	public GameObject targetObject;

	public int gridWidth = 20;
	public int gridHeight = 20;

	private float tileSize = 0.32f;

	void Awake()
	{
		// Create grid
		for(int y = 0; y < gridHeight; y++)
		{
			for(int x = 0; x < gridWidth; x++)
			{
				GameObject newTile = (GameObject)Instantiate(waterPrefab);
				newTile.transform.parent = transform;
				newTile.transform.localPosition = new Vector3(x * tileSize, 0.0f, y * tileSize);
			}
		}
	}

	void Update()
	{
		float speed = Random.Range (0, 0.02f);
		transform.Translate(-speed, 0, 0);
	}
}