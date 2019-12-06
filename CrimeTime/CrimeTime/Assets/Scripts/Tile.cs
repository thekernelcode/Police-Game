using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour {

    // We can add to this at a later date.  This is just an enum of available types to get started.
    public enum TileType {  Default, Empty, Building, Building2, Crime, Road };

	TileType type = TileType.Default;

    public MeshRenderer visual;

	// Action<Tile> cbTileTypeChanged;					// Set action to accept functions called cbTileTypeChanged with variable type Tile.

	public TileType Type {
		get {
			return type;
		}
		set {
			TileType oldType = type;
			type = value;

			//if(cbTileTypeChanged != null && oldType != type)	// We only need to redraw the tile if is actually different to what is was before.
			//	cbTileTypeChanged(this);				// Call the call back and let things know we've changed.
		}
	}

	//LooseObject looseObject;  						// We can have one loose object in a tile.
	//InstalledObject installedObject;				// We can have one installed object in a tile.

	World world;									// Tile aware of the world its in.

	int x;											// Tile aware of its x location.
	public int X {
		get {
			return x;
		}
	}

											
	int y;											// Tile aware of its y location.
	public int Y {
		get {
			return y;
		}
	}

											

	public void GameObject (int x, int y ) {		// Constructor for tile.  Tile must be passed world it is in and its location.
		
		this.x = x;
		this.y = y;
	}

	//public void RegisterTileTypeChangedCallback (Action<Tile> callback){       	// Call this function with another function as the type.  
	//																			// This sets new function to cbTileTypeChanged
	//	cbTileTypeChanged += callback;											// Multiple functions can be added to 'array of Actions'
	//}

	//public void UnRegisterTileTypeChangedCallback (Action<Tile> callback){      // Call this function with another function as the type.  
	//																			// This sets new function to cbTileTypeChanged
	//	cbTileTypeChanged -= callback;											// Multiple functions can be removed from 'array of Actions'
	//}
}
