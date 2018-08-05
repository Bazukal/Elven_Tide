using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

    public List<ScriptablePlayerClasses> charClasses = new List<ScriptablePlayerClasses>();
    private Dictionary<string, ScriptablePlayerClasses> classDict = new Dictionary<string, ScriptablePlayerClasses>();

    //determines how many characters are completed
    private int charsCreated = 0;

    //id being assigned to equipment created
    private int itemID = 1;

    //loading panel objects
    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    //information strings
    private string attackPriority = "The characters higher on the list are more prone to attacks than those lower on the list.  Putting your weaker characters near the bottom will help with their survival.";

    private Dictionary<string, string> descStrings = new Dictionary<string, string>();
    private string archerDesc = "An Elf that has trained to use the bow and arrow to shoot from a far.  From training, the Archer has the chance to shoot more than one arrow at a time, to hit more enemies.";
    private string blackDesc = "An Elf taught in the arts of destructive magic.  Casts devastating spells to damage single or multiple enemies.";
    private string monkDesc = "Unarmed Elf that prefers to use their hands and feet to fight with.  Can hit an enemy multiple times with its fast fists.";
    private string palDesc = "An Elf that prefers to take damage and shield their allies.  Wears a weapon and a shield for extra protection, and can cast weaker White Mage Spells.";
    private string thiefDesc = "A sneaky Elf that lingers in the shadows.  Can only use daggers, however is able to poison the dagger for extra damage.  May find extra gold from battles.";
    private string warDesc = "An all around fighter that can use any type of melee weapon.  Is able to use two weapons at once, or a weapon and a shield.";
    private string whiteDesc = "This Elf prefers to stay back from the fighting, and tend to others wounds.  Can heal damage that its allies has sustained in fighting.";

    //error strings
    private string nameError = "Please Enter a Name for the Character.";
    private string classError = "You Have Not Chosen a Class Yet.";
    
    //character creation strings
    private string classChosen = null;
    private string nameChosen = null;

    //stores what input is typed in for the character name
    public InputField charInput;

    //stores Toggle Objects
    public Toggle archerTog;
    public Toggle blackTog;
    public Toggle monkTog;
    public Toggle palTog;
    public Toggle thiefTog;
    public Toggle warTog;
    public Toggle whiteTog;

    //stores toggles in a dictionary
    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();

    //text fields that can change
    public Text classDescText;
    public Text infoText;
    public Text buttonText;
    public Text titleText;

    //stores list of beginning equipment for characters
    Dictionary<string, EquipableItem> equipables = new Dictionary<string, EquipableItem>();

    // Use this for initialization
    void Start () {

        foreach(ScriptablePlayerClasses classes in charClasses)
        {
            classDict.Add(classes.charClass, classes);
        }

        //assign beginning items to equipables variable
        List<EquipableItem> tempItems = Manager.manager.getEquipableInRange(1);

        foreach(EquipableItem item in tempItems)
        {            
            equipables.Add(item.name, item);
        }

        descStrings.Add("Archer", archerDesc);
        descStrings.Add("Black Mage", blackDesc);
        descStrings.Add("Monk", monkDesc);
        descStrings.Add("Paladin", palDesc);
        descStrings.Add("Thief", thiefDesc);
        descStrings.Add("Warrior", warDesc);
        descStrings.Add("White Mage", whiteDesc);

        toggles.Add("Archer", archerTog);
        toggles.Add("Black Mage", blackTog);
        toggles.Add("Monk", monkTog);
        toggles.Add("Paladin", palTog);
        toggles.Add("Thief", thiefTog);
        toggles.Add("Warrior", warTog);
        toggles.Add("White Mage", whiteTog);
    }

    //takes the name that the player inputs into game and stores it into variable
    public void SetCharName()
    {
        nameChosen = charInput.text;
        titleText.text = nameChosen;
    }

    //stores what class was selected
    public void SetCharClass(string whichClass)
    {
        classChosen = whichClass;
        classDescText.text = descStrings[whichClass];
    }

    //saves Character created
    public void saveChar()
    {
        if (nameChosen == null)
        {
            infoText.text = nameError;
            infoText.color = Color.red;
        }            
        else if (classChosen == null)
        {
            infoText.text = classError;
            infoText.color = Color.red;
        }            
        else
        {
            ScriptablePlayerClasses classSelected = ScriptableObject.CreateInstance(typeof(ScriptablePlayerClasses)) as ScriptablePlayerClasses;
            ScriptablePlayerClasses chosen = classDict[classChosen];

            EquipableItem weapon = null;
            EquipableItem weapTemp;

            if(chosen.weapon != null)
            {
                weapTemp = equipables[chosen.weapon.name];
                weapon = createEquip(weapTemp);
            }                

            EquipableItem offHand = null;
            EquipableItem offHandTemp;

            if (chosen.offHand != null)
            {
                offHandTemp = equipables[chosen.offHand.name];
                offHand = createEquip(offHandTemp);
            }

            EquipableItem armor = null;
            EquipableItem armorTemp;

            if (chosen.armor != null)
            {
                armorTemp = equipables[chosen.armor.name];
                armor = createEquip(armorTemp);
            }

            EquipableItem accessory = null;
            EquipableItem accessoryTemp;

            if (chosen.accessory != null)
            {
                accessoryTemp = equipables[chosen.accessory.name];
                accessory = createEquip(accessoryTemp);
            }

            classSelected.Init(nameChosen, classChosen, 1, 0, chosen.classHead, chosen.battleSprite,
                chosen.maxArmor, chosen.expChart, chosen.levelHp, chosen.levelMp, chosen.levelStrength,
                chosen.levelAgility, chosen.levelMind, chosen.levelSoul, chosen.levelDefense, false,
                chosen.canShield, chosen.canDuelWield, chosen.canSword, chosen.canDagger, chosen.canMace,
                chosen.canBow, chosen.canStaff, chosen.canRod, chosen.canFists, weapon,
                offHand, armor, accessory, chosen.knownSkills, null, null);

            Manager.manager.newCharacters(classSelected);
            charsCreated++;

            checkCreated();
        }
    }

    private EquipableItem createEquip(EquipableItem template)
    {
        EquipableItem equipTemp = ScriptableObject.CreateInstance(typeof(EquipableItem)) as EquipableItem;
        equipTemp.Init(template.name, template.type, template.equipType, template.rarity,
                    template.damage, template.armor, template.strength, template.agility,
                    template.mind, template.soul, template.minLevel, template.maxLevel, template.buyValue,
                    template.sellValue, template.bought, template.chest, template.maxUpgrades, 
                    template.upgradeCosts, template.minRange, template.maxRange);
        equipTemp.ID = itemID;
        itemID++;

        return equipTemp;
    }

    //checks if more chars needs to be created
    private void checkCreated()
    {
        if (charsCreated == 3)
            buttonText.text = "Continue";

        if (charsCreated < 4)
        {
            toggles[classChosen].isOn = false;
            toggles[classChosen].interactable = false;
            classDescText.text = "";
            classChosen = null;

            infoText.text = attackPriority;

            titleText.text = string.Format("Character {0}", charsCreated + 1);
            charInput.text = null;
            nameChosen = null;
        }
        else
        {
            loadScreen.SetActive(true);
            Manager.manager.setGold(100);
            Manager.manager.SetTownStage(1);
            StartCoroutine(IntroScene());
        }
    }

    //loads intro scene
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
