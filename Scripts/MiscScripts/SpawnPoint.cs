using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject archer;
    public GameObject blackMage;
    public GameObject monk;
    public GameObject paladin;
    public GameObject thief;
    public GameObject warrior;
    public GameObject whiteMage;

    //determine which character class is in each slot.  Go through each character to find the first one that is alive 
    //and spawn that character prefab
    private void Start()
    {
        string char1 = Manager.manager.GetPlayer("Player1").GetClass();
        string char2 = Manager.manager.GetPlayer("Player2").GetClass();
        string char3 = Manager.manager.GetPlayer("Player3").GetClass();
        string char4 = Manager.manager.GetPlayer("Player4").GetClass();

        int char1Hp = Manager.manager.GetPlayer("Player1").GetCurrentHP();
        int char2Hp = Manager.manager.GetPlayer("Player2").GetCurrentHP();
        int char3Hp = Manager.manager.GetPlayer("Player3").GetCurrentHP();

        if(char1Hp > 0)
        {
            switch(char1)
            {
                case "Archer":
                    Instantiate(archer, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "Black Mage":
                    Instantiate(blackMage, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "Monk":
                    Instantiate(monk, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "Paladin":
                    Instantiate(paladin, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "Thief":
                    Instantiate(thief, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "Warrior":
                    Instantiate(warrior, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case "White Mage":
                    Instantiate(whiteMage, gameObject.transform.position, gameObject.transform.rotation);
                    break;
            }
        }
        else if(char2Hp > 0)
        {
            switch (char2)
            {
                case "Archer":
                    Instantiate(archer, gameObject.transform.position, transform.rotation);
                    break;
                case "Black Mage":
                    Instantiate(blackMage, gameObject.transform.position, transform.rotation);
                    break;
                case "Monk":
                    Instantiate(monk, gameObject.transform.position, transform.rotation);
                    break;
                case "Paladin":
                    Instantiate(paladin, gameObject.transform.position, transform.rotation);
                    break;
                case "Thief":
                    Instantiate(thief, gameObject.transform.position, transform.rotation);
                    break;
                case "Warrior":
                    Instantiate(warrior, gameObject.transform.position, transform.rotation);
                    break;
                case "White Mage":
                    Instantiate(whiteMage, gameObject.transform.position, transform.rotation);
                    break;
            }
        }
        else if(char3Hp > 0)
        {
            switch (char3)
            {
                case "Archer":
                    Instantiate(archer, gameObject.transform.position, transform.rotation);
                    break;
                case "Black Mage":
                    Instantiate(blackMage, gameObject.transform.position, transform.rotation);
                    break;
                case "Monk":
                    Instantiate(monk, gameObject.transform.position, transform.rotation);
                    break;
                case "Paladin":
                    Instantiate(paladin, gameObject.transform.position, transform.rotation);
                    break;
                case "Thief":
                    Instantiate(thief, gameObject.transform.position, transform.rotation);
                    break;
                case "Warrior":
                    Instantiate(warrior, gameObject.transform.position, transform.rotation);
                    break;
                case "White Mage":
                    Instantiate(whiteMage, gameObject.transform.position, transform.rotation);
                    break;
            }
        }
        else
        {
            switch (char4)
            {
                case "Archer":
                    Instantiate(archer, gameObject.transform.position, transform.rotation);
                    break;
                case "Black Mage":
                    Instantiate(blackMage, gameObject.transform.position, transform.rotation);
                    break;
                case "Monk":
                    Instantiate(monk, gameObject.transform.position, transform.rotation);
                    break;
                case "Paladin":
                    Instantiate(paladin, gameObject.transform.position, transform.rotation);
                    break;
                case "Thief":
                    Instantiate(thief, gameObject.transform.position, transform.rotation);
                    break;
                case "Warrior":
                    Instantiate(warrior, gameObject.transform.position, transform.rotation);
                    break;
                case "White Mage":
                    Instantiate(whiteMage, gameObject.transform.position, transform.rotation);
                    break;
            }
        }
    }
}
