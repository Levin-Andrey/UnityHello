using UnityEngine;
using UnityEngine.Networking;

public class Avatar : NetworkBehaviour
{
	public float WalkingSpeed = 3.0f;
    public GameObject CarrotPrefab;
    public SharedInventory sharedInv;

    [SyncVar]
	private Vector3 tilePos;

	private float lastSyncedTime;

    void Awake() {
        sharedInv = GameObject.Find("SharedInventory").GetComponent<SharedInventory>();
    }

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
					var newTilePos = GridManager.PosToTile(transform.position);
					if (newTilePos != tilePos) {
						CmdSetTilePos(newTilePos);
					}
				}
			}
		} else {
			walkTowards(tilePos, dt);
		}
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
		var atPos = GridManager.At(tilePos);
		if (atPos == null || atPos.ground == null) return;
		if (atPos.content == null) {
            if (sharedInv.getCarrotsNum() == 0) {
                return;
            }
            sharedInv.setCarrotsNum(sharedInv.getCarrotsNum() - 1);
            var carrot = (GameObject)Instantiate(CarrotPrefab, tilePos, Quaternion.identity);
			NetworkServer.Spawn(carrot);
		} else {
			var carrot = atPos.content;
            int plusCarrots;
            if (carrot.GetComponent<Carrot>().carSt == CarrotState.final) {
                plusCarrots = 2;
            } else {
                plusCarrots = 1;
            }
            sharedInv.setCarrotsNum(sharedInv.getCarrotsNum() + plusCarrots);
            Destroy(carrot);
            atPos.content = null;
		}
	}
}