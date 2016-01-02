using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class SharedInventory : NetworkBehaviour {
    [SyncVar]
    public int CarrotsNum;

    public Text carrotsNumText;

    public void Awake() {
        carrotsNumText = GameObject.Find("CarrotsNumText").GetComponent<Text>();
    }

    public int getCarrotsNum() {
        return CarrotsNum;
    }

    public void setCarrotsNum(int value) {
        this.CarrotsNum = value;
    }

	// Update is called once per frame
	void Update () {
        this.carrotsNumText.text = "Carrots: " + CarrotsNum;
    }
}
