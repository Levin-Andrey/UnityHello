using UnityEngine;
using UnityEngine.Networking;

public class Avatar : NetworkBehaviour
{
	public float WalkingSpeed = 3.0f;

	[SyncVar]
	private Vector3 tilePos;

	void Update()
	{
		var dt = Time.deltaTime;
		if (isLocalPlayer) {
			var move = new Vector3(
				Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
				0,
				Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0
			);
			transform.Translate(move.normalized * WalkingSpeed * dt);
			Vector3 newTilepos = posToTile();
			if (newTilepos != tilePos) {
				CmdSetTilePos(posToTile());
			}
		} else {
			walkTowards(tilePos, dt);
		}
	}

	private Vector3 posToTile() {
		var tile = transform.position;
		tile.x = Mathf.Floor(tile.x + 0.5f);
		tile.z = Mathf.Floor(tile.z + 0.5f);
		return tile;
	}

	private void walkTowards(Vector3 target, float dt) {
		transform.position = Vector3.MoveTowards(
			transform.position,
			tilePos,
			WalkingSpeed * dt
		);
	}

	[Command]
	private void CmdSetTilePos(Vector3 newPos) {
		tilePos = newPos;
	}
}