using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
//using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public GameObject Block;
	public GameObject Pellet;

	public const string wall = "W";
	public const string space = "0";

	public int gridWidth = 20;
	public int gridHeight = 20;

	private Vector3 blockSize;

	private float blockWidth;
	private float blockHeight;

	void setBlock() {

		blockWidth = Block.GetComponent<BoxCollider2D>().size.x;
		blockHeight = Block.GetComponent<BoxCollider2D>().size.y;
	}

	Vector3 calculateMapPosition(Vector2 mapPosition) {

		Vector3 initialPosition;

		initialPosition = new Vector3(-gridWidth / 2,  -gridHeight / 2, 0);
		Vector3 blockPosition;

		blockPosition = new Vector3 (initialPosition.x + (mapPosition.x * blockWidth), initialPosition.y + (mapPosition.y * blockHeight), 0);

		//Debug.Log (blockWidth);

		return blockPosition;

	}

	string[][] readMap(string txtMap){

		string text = System.IO.File.ReadAllText(txtMap);
		string[] lines = Regex.Split(text, "\n");

		int rows = lines.Length;
		string[][] levelBase = new string[rows][];

		for (int i = 0; i < lines.Length; i++)  {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}

		return levelBase;
	}

	void createGrid() {

		GameObject grid = new GameObject ("grid");

		for (float y = 0; y < gridHeight; y++)
		{
			for (float x = 0; x < gridWidth; x++)
			{
				GameObject block = (GameObject)Instantiate(Block);
				Vector2 mapPosition = new Vector2(x, y);
				block.transform.position = calculateMapPosition(mapPosition);
				block.transform.parent = grid.transform;
			}
		}
	}

	// Use this for initialization
	void Start () {

		//get the size of a platform
		//create an array of[10][10]
		//(1,1),(2,1),(3,1),(4,1),(5,1),(6,1),(7,1),(8,1),(9,1),(10,1),
		//(1,2),(2,2),(3,2),(4,2),(5,2),(6,2),(7,2),(8,2),(9,2),(10,2),
		//(1,3),(2,3),(3,3),(4,3),(5,3),(6,3),(7,3),(8,3),(9,3),(10,3),
		//(1,4),(2,4),(3,4),(4,4),(5,4),(6,4),(7,4),(8,4),(9,4),(10,4),
		//(1,5),(2,5),(3,5),(4,5),(5,5),(6,5),(7,5),(8,5),(9,5),(10,5),
		//(1,6),(2,6),(3,6),(4,6),(5,6),(6,6),(7,6),(8,6),(9,6),(10,6),
		//(1,7),(2,7),(3,7),(4,7),(5,7),(6,7),(7,7),(8,7),(9,7),(10,7),
		//(1,8),(2,8),(3,8),(4,8),(5,8),(6,8),(7,8),(8,8),(9,8),(10,8),
		//(1,9),(2,9),(3,9),(4,9),(5,9),(6,9),(7,9),(8,9),(9,9),(10,9),
		//(1,10),(2,10),(3,10),(4,10),(5,10),(6,10),(7,10),(8,10),(9,10),(10,10),
		//i can set the coordinate to a prefab
		//i need to make sure that every tunnel has at least 2 exits (inside edges are allowed to touch, if 1x or 1y is less than the max
		//i should be able to do this for the positive coordinates and then flip three times to get a symetrical map
	
		setBlock ();
		//createGrid ();
		//C:\Users\Mat\Documents\GitHub\code-foo-2014\Code_Foo_Pac\Assets\Maps
		string[][] coordinates = readMap("../Code_Foo_Pac/Assets/Maps/map.txt");

		//Debug.Log(coordinates[1][1]);

		Vector2 mapPosition;
		GameObject block;
		GameObject pellet;
		GameObject map = new GameObject ("map");
		
		// create planes based on matrix
		for (int y = 0; y < coordinates.Length; y++) {
			for (int x = 0; x < coordinates[0].Length; x++) {
				//Debug.Log(y);
				switch (coordinates[y][x]){
				case wall:
					block = (GameObject)Instantiate(Block);
					mapPosition = new Vector2(x, y);
					block.transform.position = calculateMapPosition(mapPosition);
					block.transform.parent = map.transform;
					break;
				case space:
					pellet = (GameObject)Instantiate(Pellet);
					mapPosition = new Vector2(x, y);
					pellet.transform.position = calculateMapPosition(mapPosition);
					pellet.transform.parent = map.transform;
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
