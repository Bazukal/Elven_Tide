using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHoleDungeon : MonoBehaviour {

    public List<GameObject> dungeons;

	// Use this for initialization
	void Start () {
        int dungeonSelect = Random.Range(0, dungeons.Count);

        Instantiate(dungeons[dungeonSelect], gameObject.transform.position, Quaternion.identity);

        BattleScript.battleOn.setTerrain();
	}
}
