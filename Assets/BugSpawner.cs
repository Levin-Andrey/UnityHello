using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BugSpawner : NetworkBehaviour
{
    public GameObject BugPrefab;

    // Use this for initialization
    void Start () {
        var bug = (GameObject)Instantiate(BugPrefab, new Vector3(0, 0), Quaternion.identity);
        NetworkServer.Spawn(bug);
    }
	
	// Update is called once per frame
	void Update () {
        if (Random.value > 0.5) {
            // TODO: I am am am am ma
        }
	}
}
