using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public Sprite crimeInProgressSprite;
	public Sprite buildingTile;
	public Sprite buildingTile2;
	public Sprite roadTile;
	public Sprite emptyTile;
	public GameObject building;
	public GameObject building2;
    public GameObject tile_go;
    public Material[] tileMaterials;

    World world;

	// Use this for initialization
	void Start () {

		// Create a world with Empty tiles.
		world = new World ();

		// Create a GameObject for each of our tiles, so they show visually.
		for (int x = 0; x < world.Width; x++) {
			for (int y = 0; y < world.Height; y++) {
				Tile tile_data = world.GetTileAt (x, y);

                Instantiate(tile_go, new Vector3(tile_data.X, tile_data.Y, 0), Quaternion.identity);



                tile_go.name = "Tile_" + x + "_" + y;
				//tile_go.transform.SetParent(this.transform, true);
				tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, 0);

				// tile_data.RegisterTileTypeChangedCallback ( Foo );				// We need a Lamba function to add GameObject to function we're calling.
				// tile_data.RegisterTileTypeChangedCallback ( (tile) => { } );		// This is the same as the function example above.

				// tile_data.RegisterTileTypeChangedCallback ( (tile) => { OnTileTypeChanged(tile, tile_go); } );
			}
		}
			
		world.RandomizeTiles ();
		world.AddRoads ();
		AddBuildingsSmall ();	
		AddBuildingsMedium ();
       
	}



	public void AddBuildingsSmall(){
		for (int x = 0; x < world.Width; x++) {
			for (int y = 0; y < world.Height; y++) {
				Tile tile_data = world.GetTileAt (x, y);

				if (tile_data.Type == Tile.TileType.Building) {

					GameObject building_go = Instantiate (building, new Vector3 (tile_data.X, tile_data.Y, 0), Quaternion.identity);
					building_go.name = "Building_" + x + "_" + y;
					building_go.transform.SetParent (this.transform, true);

				} 
			}
		}
	}

	public void AddBuildingsMedium(){
		for (int x = 5; x < world.Width-5; x++) {
			for (int y = 5; y < world.Height-5; y++) {
				Tile tile_data = world.GetTileAt (x, y);
				Tile tile_dataX2 = world.GetTileAt (x + 1, y);
				Tile tile_dataY2 = world.GetTileAt (x, y + 1);
				Tile tile_dataXY2 = world.GetTileAt (x + 1, y + 1);

				if (tile_data.Type == Tile.TileType.Building2 &&
					tile_dataX2.Type == Tile.TileType.Building2 &&
					tile_dataY2.Type == Tile.TileType.Building2 &&
					tile_dataXY2.Type == Tile.TileType.Building2) {
					GameObject building_go2 = Instantiate (building2, new Vector3 (tile_data.X, tile_data.Y, 0), Quaternion.identity);
					building_go2.name = "MediumBuilding_" + x + "_" + y;
					building_go2.transform.SetParent (this.transform, true);
					tile_dataX2.Type = Tile.TileType.Empty;
					tile_dataY2.Type = Tile.TileType.Empty;
					tile_dataXY2.Type = Tile.TileType.Empty;
					//TODO Have building 2 check its neighbours so it will only instantiate if tile isnt already built on
					//TODO Have building make sure its not being drawn out of range.
				}
			}

		}
	}


	// float randomizeTileTimer = 2f;
	float crimeInProgressTimer = 4f;
	
	// Update is called once per frame
	void Update () {
		
		crimeInProgressTimer -= Time.deltaTime;

		if (crimeInProgressTimer < 0) {
			world.CrimeInProgress ();

			crimeInProgressTimer = 1f;
			Debug.Log ("Resetting crime timer");
		}


        UpdateTiles();
    }

	//void OnTileTypeChanged(Tile tile_data, GameObject tile_go) {
		
	//	if (tile_data.Type == Tile.TileType.Crime) {
	//		tile_go.GetComponent<SpriteRenderer> ().sprite = crimeInProgressSprite;
	//	} else if (tile_data.Type == Tile.TileType.Empty) {
	//		tile_go.GetComponent<SpriteRenderer> ().sprite = emptyTile;
	//	} else if (tile_data.Type == Tile.TileType.Road) {
	//		tile_go.GetComponent<SpriteRenderer> ().sprite = roadTile;
	//		//	Instantiate (building, new Vector3 (tile_data.X, tile_data.Y, 0), Quaternion.identity);
	//	} else if (tile_data.Type == Tile.TileType.Building) {
	//		tile_go.GetComponent<SpriteRenderer> ().sprite = buildingTile;
	//	} else if (tile_data.Type == Tile.TileType.Building2) {
	//		tile_go.GetComponent<SpriteRenderer> ().sprite = buildingTile2;
	//	} else {
	//		Debug.LogError ("OnTileTypeChanged - unrecognized tile type.");
	//	}

    void UpdateTiles()
    {
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);

                if (tile_data.Type == Tile.TileType.Crime)
                {
                    Debug.Log("Changing material to crime");
                    tile_data.GetComponentInParent<MeshRenderer>().material = tileMaterials[0];
                    //tile_go.GetComponent<SpriteRenderer>().sprite = crimeInProgressSprite;
                }
                else if (tile_data.Type == Tile.TileType.Empty)
                {
                    //tile_data.visual = emptyTile;
                }
                else if (tile_data.Type == Tile.TileType.Road)
                {
                    //tile_data.visual = roadTile;
                    //	Instantiate (building, new Vector3 (tile_data.X, tile_data.Y, 0), Quaternion.identity);
                }
                else if (tile_data.Type == Tile.TileType.Building)
                {
                    //tile_data.visual = buildingTile;
                }
                else if (tile_data.Type == Tile.TileType.Building2)
                {
                    //tile_data.visual = buildingTile2;
                }
                else
                {
                    Debug.LogError("OnTileTypeChanged - unrecognized tile type.");
                }

            }
        }

        
    }

   

}
