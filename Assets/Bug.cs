using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {
    Transform target;

    public float WalkinSpeed = 1.2f;

	void Update () {
        var carrots = GameObject.FindGameObjectsWithTag("Carrot");
        if (target == null) {
            // TODO: that's just lame
            foreach (GameObject carrotObject in carrots) {
                var carrot = carrotObject.GetComponent<Carrot>();
                target = carrot.transform;
            }
        } else {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GridManager.PosToTile(target.position),
                WalkinSpeed * Time.deltaTime
            );
            var tile = GridManager.PosToTile(transform.position);
            var tileValue = GridManager.At(tile);
            if (tileValue != null || tileValue.content != null) {
                Destroy(tileValue.content);
                tileValue.content = null;
                target = null;
            }
        }
    }
}
