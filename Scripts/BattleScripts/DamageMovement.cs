using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMovement : MonoBehaviour {

    private List<Vector2> movePoints = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 16),
        new Vector2(2, 30), new Vector2(3, 42), new Vector2(6, 52), new Vector2(10, 60) };
    private float speed = 5f;

    public void movement(int damage)
    {
        int newDam = damage;

        if (damage < 0)
        {
            newDam = damage * -1;
            gameObject.GetComponent<Text>().color = Color.red;
        }
        else
            gameObject.GetComponent<Text>().color = Color.green;

        gameObject.GetComponent<Text>().text = newDam.ToString();

        StartCoroutine(moveNow());
    }

    IEnumerator moveNow()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 current = new Vector2();

        for(int i = 0;i < movePoints.Count - 1; i++)
        {
            current = movePoints[i];
            float distance = Vector2.Distance(current, movePoints[i + 1]);
            while (distance > 1)
            {
                distance = Vector2.Distance(current, movePoints[i + 1]);
                current = GetComponent<RectTransform>().localPosition;
                GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(new Vector2(current.x, current.y), new Vector2(movePoints[i + 1].x, movePoints[i + 1].y), speed);
                yield return new WaitForEndOfFrame();
            }
        }

        Destroy(gameObject);
    }
}
