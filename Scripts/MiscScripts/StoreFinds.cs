using UnityEngine;

public class StoreFinds : MonoBehaviour {

    public static StoreFinds stored;

    public GameObject player;
    public GameObject joyStick;

	// Use this for initialization
	void Start () {
        stored = this;

        player = GameObject.FindGameObjectWithTag("Player");
	}
}
