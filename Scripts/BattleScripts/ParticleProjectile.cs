using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : MonoBehaviour {

    public float speed;

    Vector2 currentPos = new Vector2();
    Vector2 endingPos = new Vector2();

    public void positions(Vector2 end)
    {
        currentPos = GetComponent<RectTransform>().localPosition;
        endingPos = end;
        
        StartCoroutine(ObjectProject());
    }

    IEnumerator ObjectProject()
    {
        yield return new WaitForSeconds(0.5f);
        float distance = Vector2.Distance(currentPos, endingPos);
        while (distance > 1)
        {
            distance = Vector2.Distance(currentPos, endingPos);
            currentPos = GetComponent<RectTransform>().localPosition;
            GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(new Vector2(currentPos.x, currentPos.y), new Vector2(endingPos.x, endingPos.y), speed);
            yield return new WaitForEndOfFrame();           
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

        yield return new WaitForEndOfFrame();
    }
}
