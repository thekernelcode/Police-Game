using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	Tile[,] tiles;

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

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = new Tile(this, x, y);
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
			int numX = (Random.Range (0, width));
			int numY = (Random.Range (0, height));

			//TODO: Add check to see if neighbouring tile is a road, if it is then skip this iteration.

			for (int x = numX; x < width; x++) {
				tiles [x, numY].Type = Tile.TileType.Road;
			}
			for (int x2 = numX; x2 >= 0; x2--) {
				tiles [x2, numY].Type = Tile.TileType.Road;
			}
		}				

		for (int i2 = 0; i2 < numVerRoads; i2++) {
			int numX = (Random.Range (0, width));
			int numY = (Random.Range (0, height));
			
			for (int y = numY; y < height; y++) {
				tiles [numX, y].Type = Tile.TileType.Road;
			}
			for (int y2 = numY; y2 >= 0; y2--) {
				tiles [numX, y2].Type = Tile.TileType.Road;
			}
		} 

	}


	public void CrimeInProgress(){

			int numX = (Random.Range (0, width));
			int numY = (Random.Range (0, height));

		if (tiles[numX, numY].Type == Tile.TileType.Building ||
			tiles[numX, numY].Type == Tile.TileType.Building2 ||
			tiles[numX, numY].Type == Tile.TileType.Empty){
			tiles [numX, numY].Type = Tile.TileType.Crime;

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

}
