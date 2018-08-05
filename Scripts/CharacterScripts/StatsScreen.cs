using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StatsScreen : MonoBehaviour {

    //static variable
    public static StatsScreen stats;

    //screen objects
    public GameObject StatScreen;
    public GameObject EquippedScreen;
    public GameObject SkillsScreen;

    public GameObject InventoryScreen;
    public GameObject EquipmentScreen;
    public GameObject CurrencyScreen;

    //Buttons and Canvas's for Buttons
    public Canvas StatCanvas;
    public Button StatButton;
    public Canvas EquippedCanvas;
    public Button EquippedButton;
    public Canvas SkillCanvas;
    public Button SkillButton;
    public Canvas InvCanvas;
    public Button InvButton;
    public Canvas EquipCanvas;
    public Button EquipButton;
    public Canvas CurrencyCanvas;
    public Button CurrencyButton;

    //Character Name Text
    public Text CharNameValue;

    //texts in the Stat Screen
    public Text ClassValue;
    public Text LevelValue;
    public Text HealthValue;
    public Text ManaValue;
    public Text StrValue;
    public Text AgiValue;
    public Text MindValue;
    public Text SoulValue;
    public Text DefValue;
    public Text ExpValue;

    //objects for Skill Screen
    public GameObject skillScroll;
    public GameObject skillButton;

    //objects for Inventory Screen
    public GameObject invScroll;
    public GameObject invButton;

    //objects for Equipment Screen
    public GameObject equipScroll;
    public GameObject equipButton;

    //texts in the Currency Screen
    public Text GoldValue;
    public Text SmallKlogyteValue;
    public Text MediumKlogyteValue;
    public Text RegularKlogyteValue;
    public Text LargeKlogyteValue;
    public Text GiganticKlogyteValue;
    public Text SmallFormatyteValue;
    public Text MediumFormatyteValue;
    public Text RegularFormatyteValue;
    public Text LargeFormatyteValue;
    public Text GiganticFormatyteValue;
    public Text SmallRagalyteValue;
    public Text MediumRagalyteValue;
    public Text RegularRagalyteValue;
    public Text LargeRagalyteValue;
    public Text GiganticRagalyteValue;

    //Characters
    private List<ScriptablePlayerClasses> characters = new List<ScriptablePlayerClasses>();

    //Currently Selected Player
    private int charSelected;

    // Use this for initialization
    void Start () {
        stats = this;

        characters.Add(Manager.manager.GetPlayer("Player1"));
        characters.Add(Manager.manager.GetPlayer("Player2"));
        characters.Add(Manager.manager.GetPlayer("Player3"));
        characters.Add(Manager.manager.GetPlayer("Player4"));

        gameObject.SetActive(false);
    }

    //Getter for character selected
    public int GetCharSelect() { return charSelected; }

    //Character Casting spell
    public ScriptablePlayerClasses GetPlayerObject() { return characters[charSelected]; }

    //what happens when screen is opened
    public void ScreenOpened()
    {
        charSelected = 0;
        SetToStatScreen();
        SetToInvScreen();
    }

    //closes Stat Screen
    public void CloseScreen()
    {
        StoreFinds.stored.BattleDeactivate();
        gameObject.SetActive(false);
    }

    //Set to Stat Screen
    public void SetToStatScreen()
    {
        ToggleStat(true);
        ToggleEquipped(false);
        ToggleSkill(false);

        StatScreen.SetActive(true);
        EquippedScreen.SetActive(false);
        SkillsScreen.SetActive(false);

        SetStats();
    }

    //Set to Equipped Screen
    public void SetToEquippedScreen()
    {
        ToggleStat(false);
        ToggleEquipped(true);
        ToggleSkill(false);

        StatScreen.SetActive(false);
        EquippedScreen.SetActive(true);
        SkillsScreen.SetActive(false);

        EquippedButtonEvent.butEvent.SetButtonSprites();
    }

    //Set to Skill Screen
    public void SetToSkillScreen()
    {
        ToggleStat(false);
        ToggleEquipped(false);
        ToggleSkill(true);

        StatScreen.SetActive(false);
        EquippedScreen.SetActive(false);
        SkillsScreen.SetActive(true);

        refreshSkills();
        SetSkills();
    }

    //Set to Inventory Screen
    public void SetToInvScreen()
    {
        ToggleInv(true);
        ToggleEquip(false);
        ToggleCurrency(false);

        InventoryScreen.SetActive(true);
        EquipmentScreen.SetActive(false);
        CurrencyScreen.SetActive(false);

        ClearInventory();
        PopulateInv();
    }

    //Set to Equipment Screen
    public void SetToEquipScreen()
    {
        ToggleInv(false);
        ToggleEquip(true);
        ToggleCurrency(false);

        InventoryScreen.SetActive(false);
        EquipmentScreen.SetActive(true);
        CurrencyScreen.SetActive(false);

        ClearEquipment();
        PopulateEquipment();
    }

    //Set to Currency Screen
    public void SetToCurrencyScreen()
    {
        ToggleInv(false);
        ToggleEquip(false);
        ToggleCurrency(true);

        InventoryScreen.SetActive(false);
        EquipmentScreen.SetActive(false);
        CurrencyScreen.SetActive(true);

        SetCurrencyValue();
    }

    //toggles stat button
    private void ToggleStat(bool toggled)
    {
        if (toggled == true)
        {
            StatButton.interactable = false;
            StatCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30,30);
            StatCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            StatButton.interactable = true;
            StatCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0,0);
            StatCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //toggles Equipped button
    private void ToggleEquipped(bool toggled)
    {
        if (toggled == true)
        {
            EquippedButton.interactable = false;
            EquippedCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            EquippedCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            EquippedButton.interactable = true;
            EquippedCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            EquippedCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //toggles stat button
    private void ToggleSkill(bool toggled)
    {
        if (toggled == true)
        {
            SkillButton.interactable = false;
            SkillCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            SkillCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            SkillButton.interactable = true;
            SkillCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            SkillCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //toggles stat button
    private void ToggleInv(bool toggled)
    {
        if (toggled == true)
        {
            InvButton.interactable = false;
            InvCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            InvCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            InvButton.interactable = true;
            InvCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            InvCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //toggles Equipped button
    private void ToggleEquip(bool toggled)
    {
        if (toggled == true)
        {
            EquipButton.interactable = false;
            EquipCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            EquipCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            EquipButton.interactable = true;
            EquipCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            EquipCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //toggles stat button
    private void ToggleCurrency(bool toggled)
    {
        if (toggled == true)
        {
            CurrencyButton.interactable = false;
            CurrencyCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            CurrencyCanvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        else
        {
            CurrencyButton.interactable = true;
            CurrencyCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            CurrencyCanvas.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    //Changes to previous character
    public void PreviousChar()
    {
        if (charSelected == 0)
            charSelected = 3;
        else
            charSelected--;

        ResetCharScreen();
    }

    //Changes to next character
    public void NextChar()
    {
        if (charSelected == 3)
            charSelected = 0;
        else
            charSelected++;

        ResetCharScreen();
    }

    //Sets current character
    private void ResetCharScreen()
    {
        bool statSelected = StatScreen.activeSelf;
        bool equippedSelected = EquippedScreen.activeSelf;
        bool skillSelected = SkillsScreen.activeSelf;

        ScriptablePlayerClasses selectedChar = characters[charSelected];

        CharNameValue.text = selectedChar.name;

        if (statSelected == true)
            SetStats();
        else if(skillSelected == true)
        {
            refreshSkills();
            SetSkills();
        }
        else
            EquippedButtonEvent.butEvent.SetButtonSprites();
    }

    //Populates Stat Screen
    public void SetStats()
    {
        ScriptablePlayerClasses selectedChar = characters[charSelected];

        CharNameValue.text = selectedChar.name;
        ClassValue.text = selectedChar.charClass;
        int charLevel = selectedChar.level;
        LevelValue.text = charLevel.ToString();
        HealthValue.text = string.Format("{0} / {1}", selectedChar.currentHp, selectedChar.levelHp[charLevel]);
        ManaValue.text = string.Format("{0} / {1}", selectedChar.currentMp, selectedChar.levelMp[charLevel]);
        StrValue.text = selectedChar.GetStrength().ToString();
        AgiValue.text = selectedChar.GetAgility().ToString();
        MindValue.text = selectedChar.GetMind().ToString();
        SoulValue.text = selectedChar.GetSoul().ToString();
        DefValue.text = selectedChar.GetStatDefense().ToString();
        ExpValue.text = string.Format("{0} / {1}", selectedChar.currentExp, selectedChar.expChart.experience[charLevel]);
    }

    

    //Populates Characters Skills
    private void SetSkills()
    {
        Dictionary<string, SkillScriptObject> charSkills = characters[charSelected].GetAllSkills();

        foreach (KeyValuePair<string, SkillScriptObject> skills in charSkills)
        {
            string key = skills.Key;

            if(!key.Equals("Attack"))
            {
                GameObject skillBut = (GameObject)Instantiate(skillButton) as GameObject;
                skillBut.transform.SetParent(skillScroll.transform, false);

                PopulateSkillName skillNames = skillBut.GetComponent<PopulateSkillName>();
                skillNames.skillName(charSkills[key]);
            }            
        }
    }

    //destroys objects that holds skills, meant to remove one classes skills when player selects another character
    private void refreshSkills()
    {
        foreach (Transform child in skillScroll.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Adds Inventory Items into Inventory Screen
    public void PopulateInv()
    {
        Dictionary<string, UsableItem> heldUsableInventory = Manager.manager.getUsableInventory();

        foreach (KeyValuePair<string, UsableItem> item in heldUsableInventory)
        {
            string key = item.Key;

            GameObject invenItem = (GameObject)Instantiate(invButton) as GameObject;
            invenItem.transform.SetParent(invScroll.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateUsableData(heldUsableInventory[key]);
        }
    }

    //clears out the inventory list
    public void ClearInventory()
    {
        foreach (Transform child in invScroll.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Adds Equipment into Equipment Screen
    public void PopulateEquipment()
    {
        SortedDictionary<int, EquipableItem> heldEquipableInventory = Manager.manager.getEquipableInventory();

        foreach (KeyValuePair<int, EquipableItem> item in heldEquipableInventory)
        {
            int key = item.Key;

            GameObject invenItem = (GameObject)Instantiate(equipButton) as GameObject;
            invenItem.transform.SetParent(equipScroll.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateEquipableData(heldEquipableInventory[key]);
        }
    }

    //clears out the equipment list
    public void ClearEquipment()
    {
        foreach (Transform child in equipScroll.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Sets Currency Values
    private void SetCurrencyValue()
    {
        int gold = Manager.manager.GetGold();
        GoldValue.text = gold.ToString();

        Dictionary<string, int> ores = Manager.manager.GetAllOres();

        SmallKlogyteValue.text = ores["Small Klogyte"].ToString();
        MediumKlogyteValue.text = ores["Medium Klogyte"].ToString();
        RegularKlogyteValue.text = ores["Klogyte"].ToString();
        LargeKlogyteValue.text = ores["Large Klogyte"].ToString();
        GiganticKlogyteValue.text = ores["Gigantic Klogyte"].ToString();

        SmallFormatyteValue.text = ores["Small Formatyte"].ToString();
        MediumFormatyteValue.text = ores["Medium Formatyte"].ToString();
        RegularFormatyteValue.text = ores["Formatyte"].ToString();
        LargeFormatyteValue.text = ores["Large Formatyte"].ToString();
        GiganticFormatyteValue.text = ores["Gigantic Formatyte"].ToString();

        SmallRagalyteValue.text = ores["Small Ragalyte"].ToString();
        MediumRagalyteValue.text = ores["Medium Ragalyte"].ToString();
        RegularRagalyteValue.text = ores["Ragalyte"].ToString();
        LargeRagalyteValue.text = ores["Large Ragalyte"].ToString();
        GiganticRagalyteValue.text = ores["Gigantic Ragalyte"].ToString();
    }
}
