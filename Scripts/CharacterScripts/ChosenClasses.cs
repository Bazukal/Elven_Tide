using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChosenClasses : MonoBehaviour {

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    //character classes after characters are chosen
    public PlayerClass character1;
    public PlayerClass character2;
    public PlayerClass character3;
    public PlayerClass character4;

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
    List<EquipmentClass> equipables;

    //takes the name that the player inputs into game and stores it into variable
    public void SetChar1Name() { char1Name = char1Input.text; }
    public void SetChar2Name() { char2Name = char2Input.text; }
    public void SetChar3Name() { char3Name = char3Input.text; }
    public void SetChar4Name() { char4Name = char4Input.text; }

    private Dictionary<string, PlayerClass> classes = new Dictionary<string, PlayerClass>();
    PlayerClass charClass;

    private void Start()
    {
        //assign beginning items to equipables variable
        equipables = GameItems.gItems.getEquipableInRange(1);

        CharacterContainer cc = CharacterContainer.Load("characters");

        foreach(Character character in cc.characters)
        {
            EquipmentClass weapon = null;
            EquipmentClass offHand = null;
            EquipmentClass armor = null;
            EquipmentClass accessory = null;

            foreach (EquipmentClass item in equipables)
            {
                string name = item.GetName();

                if (name.Equals(character.weapon))
                    weapon = item;

                if (name.Equals(character.offHand))
                    offHand = item;

                if (name.Equals(character.armor))
                    armor = item;

                if (name.Equals(character.accessory))
                    accessory = item;
            }

            string characterClass = character.charClass;

            charClass = new PlayerClass(character.charClass, character.maxArmor, character.maxHP,
                character.maxMP, character.strength, character.agility, character.mind, character.soul,
                character.defense, character.canShield, weapon, offHand, armor, accessory);

            classes.Add(characterClass, charClass);
        }  
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
            loadScreen.SetActive(true);

            character1 = classes[char1Class];
            character1.SetName(char1Name);

            character2 = classes[char2Class];
            character2.SetName(char2Name);

            character3 = classes[char3Class];
            character3.SetName(char3Name);

            character4 = classes[char4Class];
            character4.SetName(char4Name);

            Manager.manager.inputPlayers(character1, character2, character3, character4);

            Manager.manager.setGold(100);

            QuestClass currentClass = QuestListing.qListing.getQuest(1, "Master");

            QuestListing.qListing.setQuestPart(currentClass, 1);

            StartCoroutine(IntroScene());
        }
    }  

    IEnumerator IntroScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("IntroScene");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}

