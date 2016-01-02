using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GridManager {
	private static Dictionary<Vector3, Tile> tiles = new Dictionary<Vector3, Tile>();

	public static Tile At(Vector3 pos) {
		Tile tile;
		if (tiles.TryGetValue(pos, out tile)) {
			return tile;
		} else {
			return null;
		}
	}

	public static void AddAt(Vector3 pos, Tile tile) {
		tiles.Add(pos, tile);
	}

	public static Vector3 PosToTile(Vector3 pos) {
		pos.x = Mathf.Floor(pos.x + 0.5f);
		pos.z = Mathf.Floor(pos.z + 0.5f);
		return pos;
	}
}
