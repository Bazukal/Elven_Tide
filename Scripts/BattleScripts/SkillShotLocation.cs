using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShotLocation : MonoBehaviour {

    public int distance;

	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(distance, 0);
    }
}
