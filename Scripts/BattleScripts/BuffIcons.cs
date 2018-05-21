using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIcons : MonoBehaviour {

    public static BuffIcons buffIcons;

    public GameObject poisonImage;
    public GameObject confuseImage;
    public GameObject paralyzeImage;
    public GameObject blindImage;
    public GameObject muteImage;

    public GameObject agiImage;
    public GameObject defImage;
    public GameObject mindImage;
    public GameObject regenImage;
    public GameObject soulImage;
    public GameObject strImage;

    private Dictionary<string, GameObject> statusImages = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start ()
    {
        buffIcons = this;

        statusImages.Add("Poison", poisonImage);
        statusImages.Add("Confused", confuseImage);
        statusImages.Add("Paralyzed", paralyzeImage);
        statusImages.Add("Blind", blindImage);
        statusImages.Add("Mute", muteImage);

        statusImages.Add("Agility", agiImage);
        statusImages.Add("Defense", defImage);
        statusImages.Add("Mind", mindImage);
        statusImages.Add("Regen", regenImage);
        statusImages.Add("Soul", soulImage);
        statusImages.Add("Strength", strImage);

    }

    //returns the image of a buff or debuff to add to UI
    public GameObject getBuffIcon(string buffName)
    {
        return statusImages[buffName];
    }
}
