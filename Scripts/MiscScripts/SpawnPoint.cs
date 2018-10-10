using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject character;

    //spawns the character onto the map at the designated location
    private void Start()
    {      
        Instantiate(character, gameObject.transform.position, gameObject.transform.rotation);        
    }
}
