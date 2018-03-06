using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrowth : MonoBehaviour{

    public static LevelGrowth growth;

    private Dictionary<string, StatClass> charGrowth = new Dictionary<string, StatClass>();

	// Use this for initialization
	void Start () {
        growth = this;
	}
	
    //adds stat values for how much a stat grows based on character class to dictionary
    public void addDictionary(string charClass, StatClass stats)
    {
        charGrowth.Add(charClass, stats);
    }
	
    //gets stat growth for specific class
    public StatClass returnStats(string charClass)
    {
        return charGrowth[charClass];
    }
}
