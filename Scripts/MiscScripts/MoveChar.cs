using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour {

    public int position;

	// Use this for initialization
	void Start () {

        moveNow();
		
	}
	
	public void moveNow()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.Translate(gameObject.transform.position);
    }
}
