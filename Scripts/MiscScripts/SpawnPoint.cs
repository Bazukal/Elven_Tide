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
        string char1 = CharacterManager.charManager.character1.GetCharClass();
        string char2 = CharacterManager.charManager.character2.GetCharClass();
        string char3 = CharacterManager.charManager.character3.GetCharClass();
        string char4 = CharacterManager.charManager.character4.GetCharClass();

        int char1Hp = CharacterManager.charManager.character1.GetCharCurrentHp();
        int char2Hp = CharacterManager.charManager.character2.GetCharCurrentHp();
        int char3Hp = CharacterManager.charManager.character3.GetCharCurrentHp();

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
