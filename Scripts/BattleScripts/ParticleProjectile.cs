using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : MonoBehaviour {

    public float speed;
    float start;

    // Use this for initialization
    void Start () {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 0);
        start = Time.time;
        StartCoroutine(ObjectProject());
    }

    IEnumerator ObjectProject()
    {
        yield return new WaitForSeconds(0.5f);
        float distance = Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 0));

        while(distance > 1)
        {
            float distCov = (Time.time - start) * speed;
            float journ = distCov / distance;
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(new Vector2(150, 0), new Vector2(0, 0), journ);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}
