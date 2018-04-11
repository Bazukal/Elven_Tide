using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChosenClasses : MonoBehaviour {

    //character classes after characters are chosen
    public CharacterClass character1;
    public CharacterClass character2;
    public CharacterClass character3;
    public CharacterClass character4;

    //stores what classes player chooses for each character.  set to default archer in case player does not change class
    private string char1Class = "Archer";
    private string char2Class = "Archer";
    private string char3Class = "Archer";
    private string char4Class = "Archer";

    //descriptions of each class that will display when player selects them from the drop down
    private string archerDesc = "An Elf that has trained to use the bow and arrow to shoot from a far.  From training, the Archer has the chance to shoot more than one arrow at a time, to hit more enemies.";
    private string blackDesc = "An Elf taught in the arts of desctructive magic.  Casts devastating spells to damage single or multiple enemies.";
    private string monkDesc = "Unarmed Elf that prefers to use their hands and feet to fight with.  Can hit an enemy multiple times with its fast fists.";
    private string palDesc = "An Elf that prefers to take damage and shield their allies.  Wears a weapon and a shield for extra protection, and can cast weaker White Mage Spells.";
    private string thiefDesc = "A sneaky Elf that lingers in the shadows.  Can only use daggers, however is able to poison the dagger for extra damage.  May find extra gold from battles.";
    private string warDesc = "An all around fighter that can use any type of melee weapon.  Is able to use two weapons at once, or a weapon and a shield.";
    private string whiteDesc = "This Elf prefers to stay back from the fighting, and tend to others wounds.  Can heal damage that its allies has sustained in fighting.";

    //stores what input is typed in for the character name
    public InputField char1Input;
    public InputField char2Input;
    public InputField char3Input;
    public InputField char4Input;

    //stores what name the player gives to the characters
    private string char1Name = "";
    private string char2Name = "";
    private string char3Name = "";
    private string char4Name = "";

    //drop down boxes for choosing class
    public Transform char1DropDown;
    public Transform char2DropDown;
    public Transform char3DropDown;
    public Transform char4DropDown;

    //display of class description or error if produced
    public Text descriptionDisplay;
    public Text errorDisplay;

    //sets character Sprites
    public Sprite archerSprite;
    public Sprite blackMageSprite;
    public Sprite monkSprite;
    public Sprite paladinSprite;
    public Sprite thiefSprite;
    public Sprite warSprite;
    public Sprite whiteMageSprite;

    //stores list of beginning equipment for characters
    List<EquipableItemClass> equipables;


    //takes the name that the player inputs into game and stores it into variable
    public void SetChar1Name() { char1Name = char1Input.text; }
    public void SetChar2Name() { char2Name = char2Input.text; }
    public void SetChar3Name() { char3Name = char3Input.text; }
    public void SetChar4Name() { char4Name = char4Input.text; }

    private void Start()
    {
        //assign beginning items to equipables variable
        equipables = GameItems.gItems.getEquipableInRange(1);
    }

    //sets the class of character when the player selects a class from the drop down box.  Displays the class description
    public void SetCharClass(int character)
    {
        int index = 0;
        List<Dropdown.OptionData> menuOptions;

        switch (character)
        {
            case 1:
                index = char1DropDown.GetComponent<Dropdown>().value;
                menuOptions = char1DropDown.GetComponent<Dropdown>().options;
                char1Class = menuOptions[index].text;
                break;
            case 2:
                index = char2DropDown.GetComponent<Dropdown>().value;
                menuOptions = char2DropDown.GetComponent<Dropdown>().options;
                char2Class = menuOptions[index].text;
                break;
            case 3:
                index = char3DropDown.GetComponent<Dropdown>().value;
                menuOptions = char3DropDown.GetComponent<Dropdown>().options;
                char3Class = menuOptions[index].text;
                break;
            case 4:
                index = char4DropDown.GetComponent<Dropdown>().value;
                menuOptions = char4DropDown.GetComponent<Dropdown>().options;
                char4Class = menuOptions[index].text;
                break;
        }

        switch (index)
        {
            case 0:
                descriptionDisplay.text = archerDesc;
                break;
            case 1:
                descriptionDisplay.text = blackDesc;
                break;
            case 2:
                descriptionDisplay.text = monkDesc;
                break;
            case 3:
                descriptionDisplay.text = palDesc;
                break;
            case 4:
                descriptionDisplay.text = thiefDesc;
                break;
            case 5:
                descriptionDisplay.text = warDesc;
                break;
            case 6:
                descriptionDisplay.text = whiteDesc;
                break;
        }
    }

    //actions that take place when the player clicks on the Start Game Button
    public void StartGame()
    {
        if(char1Name == "")
        {
            descriptionDisplay.text = "Enter a name for Character 1.";
        }
        else if(char2Name == "")
        {
            descriptionDisplay.text = "Enter a name for Character 2.";
        }
        else if (char3Name == "")
        {
            descriptionDisplay.text = "Enter a name for Character 3.";
        }
        else if (char4Name == "")
        {
            descriptionDisplay.text = "Enter a name for Character 4.";
        }
        else
        {
            switch(char1Class)
            {
                case "Archer":
                    character1 = ArcherChosen(1);
                    break;
                case "Black Mage":
                    character1 = BlackMageChosen(1);
                    break;
                case "Monk":
                    character1 = MonkChosen(1);
                    break;
                case "Paladin":
                    character1 = PaladinChosen(1);
                    break;
                case "Thief":
                    character1 = ThiefChosen(1);
                    break;
                case "Warrior":
                    character1 = WarriorChosen(1);
                    break;
                case "White Mage":
                    character1 = WhiteMageChosen(1);
                    break;
            }

            switch (char2Class)
            {
                case "Archer":
                    character2 = ArcherChosen(2);
                    break;
                case "Black Mage":
                    character2 = BlackMageChosen(2);
                    break;
                case "Monk":
                    character2 = MonkChosen(2);
                    break;
                case "Paladin":
                    character2 = PaladinChosen(2);
                    break;
                case "Thief":
                    character2 = ThiefChosen(2);
                    break;
                case "Warrior":
                    character2 = WarriorChosen(2);
                    break;
                case "White Mage":
                    character2 = WhiteMageChosen(2);
                    break;
            }

            switch (char3Class)
            {
                case "Archer":
                    character3 = ArcherChosen(3);
                    break;
                case "Black Mage":
                    character3 = BlackMageChosen(3);
                    break;
                case "Monk":
                    character3 = MonkChosen(3);
                    break;
                case "Paladin":
                    character3 = PaladinChosen(3);
                    break;
                case "Thief":
                    character3 = ThiefChosen(3);
                    break;
                case "Warrior":
                    character3 = WarriorChosen(3);
                    break;
                case "White Mage":
                    character3 = WhiteMageChosen(3);
                    break;
            }

            switch (char4Class)
            {
                case "Archer":
                    character4 = ArcherChosen(4);
                    break;
                case "Black Mage":
                    character4 = BlackMageChosen(4);
                    break;
                case "Monk":
                    character4 = MonkChosen(4);
                    break;
                case "Paladin":
                    character4 = PaladinChosen(4);
                    break;
                case "Thief":
                    character4 = ThiefChosen(4);
                    break;
                case "Warrior":
                    character4 = WarriorChosen(4);
                    break;
                case "White Mage":
                    character4 = WhiteMageChosen(4);
                    break;
            }

            CharacterManager.charManager.loadCharacters(character1, character2, character3, character4);

            CharacterManager.charManager.setGold(100);

            QuestClass currentClass = QuestListing.qListing.getQuest(1, "Master");

            QuestListing.qListing.setQuestPart(currentClass, 1);

            StartCoroutine(IntroScene());
        }
    }

    //creates character classes based on input from player
    private CharacterClass ArcherChosen (int charNum)
    {
        CharacterClass charClass;
        EquipableItemClass archerWeap = null;
        EquipableItemClass archerArmor = null;
        EquipableItemClass archerAccessory = null;

        foreach(EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Cracked Bow"))
                archerWeap = item;

            else if (name.Equals("Leather Jerkin"))
                archerArmor = item;

            else if (name.Equals("Quick Ring"))
                archerAccessory = item;
        }

        switch(charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Leather", false, 1, 12, 7, 4, 4, 1, 2, 4,  
                    archerWeap, null, archerArmor, archerAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Leather", false, 1, 12, 7, 4, 4, 1, 2, 4,  
                    archerWeap, null, archerArmor, archerAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Leather", false, 1, 12, 7, 4, 4, 1, 2, 4,  
                    archerWeap, null, archerArmor, archerAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Leather", false, 1, 12, 7, 4, 4, 1, 2, 4,  
                    archerWeap, null, archerArmor, archerAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass BlackMageChosen (int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass blackWeap = null;
        EquipableItemClass blackArmor = null;
        EquipableItemClass blackAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Cracked Rod"))
                blackWeap = item;

            else if (name.Equals("Cloth Robe"))
                blackArmor = item;

            else if (name.Equals("Smart Ring"))
                blackAccessory = item;
        }

        switch (charNum)
        {
            case 1:                
                charClass = new CharacterClass(char1Name, char1Class, "Cloth", false, 1, 9, 15, 2, 2, 6, 4, 3, 
                    blackWeap, null, blackArmor, blackAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Cloth", false, 1, 9, 15, 2, 2, 6, 4, 3,  
                    blackWeap, null, blackArmor, blackAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Cloth", false, 1, 9, 15, 2, 2, 6, 4, 3,  
                    blackWeap, null, blackArmor, blackAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Cloth", false, 1, 9, 15, 2, 2, 6, 4, 3, 
                    blackWeap, null, blackArmor, blackAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass MonkChosen(int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass monkArmor = null;
        EquipableItemClass monkAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Cloth Shirt"))
                monkArmor = item;

            else if (name.Equals("Strength Ring"))
                monkAccessory = item;
        }

        switch (charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Leather", false, 1, 15, 8, 5, 3, 1, 1, 5, 
                    null, null, monkArmor, monkAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Leather", false, 1, 15, 8, 5, 3, 1, 1, 5,  
                    null, null, monkArmor, monkAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Leather", false, 1, 15, 8, 5, 3, 1, 1, 5,  
                    null, null, monkArmor, monkAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Leather", false, 1, 15, 8, 5, 3, 1, 1, 5, 
                    null, null, monkArmor, monkAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass PaladinChosen(int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass palWeap = null;
        EquipableItemClass palOffHand = null;
        EquipableItemClass palArmor = null;
        EquipableItemClass palAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Rusty Mace"))
                palWeap = item;

            else if (name.Equals("Wooden Shield"))
                palOffHand = item;

            else if (name.Equals("Rusty Plate"))
                palArmor = item;

            else if (name.Equals("Defense Bracelet"))
                palAccessory = item;
        }

        switch (charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Plate", true, 1, 20, 5, 4, 3, 2, 3, 7, 
                    palWeap, palOffHand, palArmor, palAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Plate", true, 1, 20, 5, 4, 3, 2, 3, 7,  
                    palWeap, palOffHand, palArmor, palAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Plate", true, 1, 20, 5, 4, 3, 2, 3, 7, 
                    palWeap, palOffHand, palArmor, palAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Plate", true, 1, 20, 5, 4, 3, 2, 3, 7,  
                    palWeap, palOffHand, palArmor, palAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass ThiefChosen(int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass thiefWeap = null;
        EquipableItemClass thiefOffHand = null;
        EquipableItemClass thiefArmor = null;
        EquipableItemClass thiefAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Rusty Dagger"))
                thiefWeap = item;

            if (name.Equals("Rusty Dagger"))
                thiefOffHand = item;

            else if (name.Equals("Leather Jerkin"))
                thiefArmor = item;

            else if (name.Equals("Quick Ring"))
                thiefAccessory = item;
        }

        switch (charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Leather", false, 1, 15, 5, 3, 5, 1, 2, 5,  
                    thiefWeap, thiefOffHand, thiefArmor, thiefAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Leather", false, 1, 15, 5, 3, 5, 1, 2, 5, 
                    thiefWeap, thiefOffHand, thiefArmor, thiefAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Leather", false, 1, 15, 5, 3, 5, 1, 2, 5, 
                    thiefWeap, thiefOffHand, thiefArmor, thiefAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Leather", false, 1, 15, 5, 3, 5, 1, 2, 5,
                    thiefWeap, thiefOffHand, thiefArmor, thiefAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass WarriorChosen(int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass warWeap = null;
        EquipableItemClass warOffHand = null;
        EquipableItemClass warArmor = null;
        EquipableItemClass warAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Rusty Sword"))
                warWeap = item;

            else if (name.Equals("Wooden Shield"))
                warOffHand = item;

            else if (name.Equals("Rusty Plate"))
                warArmor = item;

            else if (name.Equals("Strength Ring"))
                warAccessory = item;
        }

        switch (charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Plate", true, 1, 17, 6, 5, 3, 1, 2, 6,  
                    warWeap, warOffHand, warArmor, warAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Plate", true, 1, 17, 6, 5, 3, 1, 2, 6,  
                    warWeap, warOffHand, warArmor, warAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Plate", true, 1, 17, 6, 5, 3, 1, 2, 6,  
                    warWeap, warOffHand, warArmor, warAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Plate", true, 1, 17, 6, 5, 3, 1, 2, 6,  
                    warWeap, warOffHand, warArmor, warAccessory);
                return charClass;
        }

        return null;
    }

    private CharacterClass WhiteMageChosen(int charNum)
    {
        CharacterClass charClass;

        EquipableItemClass whiteWeap = null;
        EquipableItemClass whiteArmor = null;
        EquipableItemClass whiteAccessory = null;

        foreach (EquipableItemClass item in equipables)
        {
            string name = item.GetName();

            if (name.Equals("Cracked Staff"))
                whiteWeap = item;

            else if (name.Equals("Cloth Robe"))
                whiteArmor = item;

            else if (name.Equals("Soul Necklace"))
                whiteAccessory = item;
        }

        switch (charNum)
        {
            case 1:
                charClass = new CharacterClass(char1Name, char1Class, "Cloth", true, 1, 11, 15, 2, 2, 2, 6, 4, 
                    whiteWeap, null, whiteArmor, whiteAccessory);
                return charClass;
            case 2:
                charClass = new CharacterClass(char2Name, char2Class, "Cloth", true, 1, 11, 15, 2, 2, 2, 6, 4,  
                    whiteWeap, null, whiteArmor, whiteAccessory);
                return charClass;
            case 3:
                charClass = new CharacterClass(char3Name, char3Class, "Cloth", true, 1, 11, 15, 2, 2, 2, 6, 4, 
                    whiteWeap, null, whiteArmor, whiteAccessory);
                return charClass;
            case 4:
                charClass = new CharacterClass(char4Name, char4Class, "Cloth", true, 1, 11, 15, 2, 2, 2, 6, 4, 
                    whiteWeap, null, whiteArmor, whiteAccessory);
                return charClass;
        }

        return null;
    }

    IEnumerator IntroScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("IntroScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

