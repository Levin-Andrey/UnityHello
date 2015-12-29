using UnityEngine;
using System.Collections;

public class TilePicker : MonoBehaviour {
	public GameObject Cursor;
	public Camera Camera;
	Plane ground;

	void Awake() {
		ground = new Plane(Vector3.up, new Vector3(0, 0, 0));
	}
	
	void Update() {
		Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
		float rayDistance;
		if (ground.Raycast(ray, out rayDistance)) {
			Cursor.transform.position = GridManager.PosToTile(ray.GetPoint(rayDistance));
		}
	}
}
