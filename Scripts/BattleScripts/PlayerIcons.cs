using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcons : MonoBehaviour {

    private Dictionary<string, GameObject> buffIcons = new Dictionary<string, GameObject>();

	//add icons
    public void addIcons(GameObject icon)
    {
        string iconName = icon.name;
        Debug.Log(iconName + " Icon being Added to Panel");

        if(!buffIcons.ContainsKey(iconName))
        {
            GameObject showIcon = Instantiate(icon) as GameObject;
            showIcon.transform.SetParent(gameObject.transform, false);
            buffIcons.Add(iconName, showIcon);
        }
    }

    //remove icons
    public void removeIcons(string name)
    {
        Debug.Log(name + " Debuff Icon Being Removed");

        if(buffIcons.ContainsKey(name))
        {
            Debug.Log("Debuff Icon Found in Dictionary for: " + name);
            Destroy(buffIcons[name]);
            buffIcons.Remove(name);
        }
    }

    //remove all icons
    public void removeAllIcons()
    {
        List<string> keys = new List<string>();

        foreach(KeyValuePair<string, GameObject> icon in buffIcons)
        {
            keys.Add(icon.Key);
        }

        foreach(string key in keys)
        {
            Destroy(buffIcons[key]);
            buffIcons.Remove(key);
        }
    }
}
