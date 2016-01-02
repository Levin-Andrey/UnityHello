using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public enum CarrotState {
    initial, final
};

public class Carrot : NetworkBehaviour {
	GameObject initialSprite;
	GameObject finalSprite;
	float creationTime;
    [SyncVar]
    public CarrotState carSt = CarrotState.initial;

	void Awake() {
		creationTime = Time.realtimeSinceStartup;
		initialSprite = gameObject.transform.Find("CarrotStageInitial").gameObject;
		finalSprite = gameObject.transform.Find("CarrotStageFinal").gameObject;
		var atPos = GridManager.At(GridManager.PosToTile(transform.position));
		if (atPos == null || atPos.ground == null || atPos.content != null) {
			throw new UnityException("OMG! Created in the void!");
		}
		atPos.content = gameObject;
	}

	[ServerCallback]
	void Update() {
		var curtime = Time.realtimeSinceStartup;
		if ((curtime - creationTime) > 5.0f) {
            carSt = CarrotState.final;
            RpcSetSpriteFinal();
		}
	}

	[ClientRpc]
	void RpcSetSpriteFinal() {
		initialSprite.SetActive(false);
		finalSprite.SetActive(true);
	}
}
