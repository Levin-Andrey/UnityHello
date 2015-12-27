using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	[SyncVar]
	public Vector3 tilePos;

	void Update()
	{
		var dt = Time.deltaTime;
		if (isLocalPlayer) {
		var speed = 3.0f;
			var move = new Vector3(
				Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
				0,
				Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0
			).normalized * speed * dt;
			transform.Translate(move);
			Vector3 pos = transform.position;
			pos.x = Mathf.Floor(pos.x + 0.5f);
			pos.z = Mathf.Floor(pos.z + 0.5f);
			tilePos = pos;
		} else {
			transform.position = Vector3.MoveTowards(
				transform.position,
				tilePos,
				3.0f * dt
			);
		}
	}
}