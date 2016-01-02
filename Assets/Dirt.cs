using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Dirt : NetworkBehaviour {
	void Awake() {
		var tilePos = GridManager.PosToTile(transform.position);
		var atPos = GridManager.At(tilePos);
		if (atPos != null) {
			throw new UnityException("OMG! Created in the void (dirt)!");
		}
		var tile = new Tile();
		tile.ground = gameObject;
		GridManager.AddAt(tilePos, tile);
	}
}
