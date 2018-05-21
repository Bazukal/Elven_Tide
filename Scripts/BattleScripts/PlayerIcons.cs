using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcons : MonoBehaviour {

    private Dictionary<string, GameObject> buffIcons = new Dictionary<string, GameObject>();

	//add icons
    public void addIcons(GameObject icon)
    {
        GameObject showIcon = Instantiate(icon) as GameObject;
        showIcon.transform.SetParent(gameObject.transform, false);
        buffIcons.Add(showIcon.name, icon);
    }

    //remove icons
    public void removeIcons(string name)
    {
        Destroy(buffIcons[name]);
        buffIcons.Remove(name);
    }

    //remove all icons
    public void removeAllIcons()
    {
        foreach(KeyValuePair<string, GameObject> icon in buffIcons)
        {
            string key = icon.Key;

            Destroy(buffIcons[key]);
            buffIcons.Remove(key);
        }
    }
}
