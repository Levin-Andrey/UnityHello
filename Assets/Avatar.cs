using UnityEngine;
using UnityEngine.Networking;

public class Avatar : NetworkBehaviour
{
	public float WalkingSpeed = 3.0f;
	public GameObject CarrotPrefab;

	[SyncVar]
	private Vector3 tilePos;

	private float lastSyncedTime;

	void Update()
	{
		var curtime = Time.realtimeSinceStartup;
		var dt = Time.deltaTime;
		if (isLocalPlayer) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				CmdPlant();
			} else {
				var move = new Vector3(
					Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
					0,
					Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0
				);
				move = Quaternion.AngleAxis(45, Vector3.up) * move;
				transform.Translate(move.normalized * WalkingSpeed * dt);
				if (lastSyncedTime == 0.0f || (curtime - lastSyncedTime) > (1.41 / WalkingSpeed)) {
					lastSyncedTime = curtime;
					var newTilePos = posToTile();
					if (newTilePos != tilePos) {
						CmdSetTilePos(newTilePos);
					}
				}
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

	[Command]
	private void CmdPlant() {
		var carrot = (GameObject)Instantiate(CarrotPrefab, tilePos, Quaternion.identity);
		NetworkServer.Spawn(carrot);
	}
}