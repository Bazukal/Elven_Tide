using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimation : MonoBehaviour {

    public float speed;
    
    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 5);
    }

    // Update is called once per frame
    void Update () {

        float ping = Mathf.PingPong(Time.time * speed, 5);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ping + 5);   
	}
}
