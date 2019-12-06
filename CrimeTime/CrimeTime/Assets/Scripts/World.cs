using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public Sprite tileVisualDefault;

	Tile[,] tiles;
    Sprite[,] tileVisuals;

	int width;

	public int Width {
		get {
			return width;
		}
	}

	int height;
	public int Height {
		get {
			return height;
		}
	}

	public World(int width = 30, int height = 30){
		
		this.width = width;
		this.height = height;

		tiles = new Tile[width, height];
        tileVisuals = new Sprite[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = new Tile(this, x, y);
                tileVisuals[x, y] = tileVisualDefault;
			}
		}

		Debug.Log ("World created with " + (width * height) + " tiles.");
	}

	public void RandomizeTiles(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int buildingType = Random.Range (0, 3);
				if (buildingType == 0) {
					tiles [x, y].Type = Tile.TileType.Building;
				} else if (buildingType == 1) {
					tiles [x, y].Type = Tile.TileType.Building2;
				}
				else {
					tiles [x, y].Type = Tile.TileType.Empty;
				}

			}
		}

	}

	public void AddRoads(){

		int numHorRoads = (Random.Range (1, 5));
		int numVerRoads = (Random.Range (1, 5));


		//tiles [numX, numY].Type = Tile.TileType.Road;

		for (int i = 0; i < numHorRoads; i++) {
			int roadPositionXSeed = (Random.Range (0, width));
			int roadPositionYSeed = (Random.Range (0, height));

			//TODO: Add check to see if neighbouring tile is a road, if it is then skip this iteration.

            // Change tiles from X seed (to the right till the map edge) to road
			for (int x = roadPositionXSeed; x < width; x++) {
				tiles [x, roadPositionYSeed].Type = Tile.TileType.Road;
			}

            // Change tiles from X see (to the left till map edge) to road
			for (int x2 = roadPositionXSeed; x2 >= 0; x2--) {
				tiles [x2, roadPositionYSeed].Type = Tile.TileType.Road;
			}
		}				

		for (int i2 = 0; i2 < numVerRoads; i2++) {
			int roadPositionXSeed = (Random.Range (0, width));
			int roadPositionYSeed = (Random.Range (0, height));

            // Change tiles from Y seed (up till the map edge) to road
            for (int y = roadPositionYSeed; y < height; y++) {
				tiles [roadPositionXSeed, y].Type = Tile.TileType.Road;
			}

            // Change tiles from Y seed (down till the map edge) to road
            for (int y2 = roadPositionYSeed; y2 >= 0; y2--) {
				tiles [roadPositionXSeed, y2].Type = Tile.TileType.Road;
			}
		} 

	}


	public void CrimeInProgress(){

			int crimePositionX = (Random.Range (0, width));
			int crimePositoionY = (Random.Range (0, height));

		if (tiles[crimePositionX, crimePositoionY].Type == Tile.TileType.Building ||
			tiles[crimePositionX, crimePositoionY].Type == Tile.TileType.Building2 ||
			tiles[crimePositionX, crimePositoionY].Type == Tile.TileType.Empty)
        
        {
			tiles [crimePositionX, crimePositoionY].Type = Tile.TileType.Crime;

			Debug.Log ("Crime in Progress.");
		}

	}


	public Tile GetTileAt (int x, int y) {	

		if (x > width || x < 0 || y > height || y < 0) {
			Debug.LogError ("Tile (" + x + "," + y + ") is out of range.");
			return null;
		}
		return tiles [x, y];
	}

    public GameObject GetGameObjectAt (int x, int y)
    {
        if (x > width || x < 0 || y > height || y < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }
        return tileVisuals[x, y];
    }
}
