using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatScreen : MonoBehaviour {

    public static CharacterStatScreen stats;

    public Text charName;
    public Text classValue;
    public Text LvlValue;
    public Text HPValue;
    public Text MPValue;
    public Text StrValue;
    public Text AgiValue;
    public Text MindValue;
    public Text SoulValue;
    public Text DefValue;
    public Text ExpValue;

    public GameObject skillButton;
    public GameObject skillScroll;

    public Button char1;
    public Button char2;
    public Button char3;
    public Button char4;

    PlayerClass character1;
    PlayerClass character2;
    PlayerClass character3;
    PlayerClass character4;

    private Dictionary<string, SkillClass> charSkills = null;

    private PlayerClass charSelected;

    private void Start()
    {
        character1 = Manager.manager.GetPlayer("Player1");
        character2 = Manager.manager.GetPlayer("Player2");
        character3 = Manager.manager.GetPlayer("Player3");
        character4 = Manager.manager.GetPlayer("Player4");

        char1.GetComponent<Image>().sprite = Manager.manager.getCharSprite(character1.GetClass());
        char2.GetComponent<Image>().sprite = Manager.manager.getCharSprite(character2.GetClass());
        char3.GetComponent<Image>().sprite = Manager.manager.getCharSprite(character3.GetClass());
        char4.GetComponent<Image>().sprite = Manager.manager.getCharSprite(character4.GetClass());

        stats = this;
    }

    //opens character stat screen
    public void displayCharScreen()
    {
        StoreFinds.stored.BattleActivate();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        changeChar(1);
    }

    //closes character stat screen
    public void closeCharScreen()
    {
        StoreFinds.stored.BattleDeactivate();

        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //sets which character has been selected to view
    public void changeChar(int charNum)
    {        
        switch (charNum)
        {
            case 1:
                charStats(character1);
                charSelected = character1;
                break;
            case 2:
                charStats(character2);
                charSelected = character2;
                break;
            case 3:
                charStats(character3);
                charSelected = character3;
                break;
            case 4:
                charStats(character4);
                charSelected = character4;
                break;
        }

        refreshSkills();

        enableSkills();
    }

    public void charStats(PlayerClass selectedChar)
    {
        //fill in screen data with char info
        charName.text = selectedChar.GetName();
        classValue.text = selectedChar.GetClass();
        LvlValue.text = selectedChar.GetLevel().ToString();
        HPValue.text = selectedChar.GetCurrentHP() + "/" + selectedChar.GetMaxHP();
        MPValue.text = selectedChar.GetCurrentMP() + "/" + selectedChar.GetMaxMP();
        StrValue.text = selectedChar.GetStrength().ToString();
        AgiValue.text = selectedChar.GetAgility().ToString();
        MindValue.text = selectedChar.GetMind().ToString();
        SoulValue.text = selectedChar.GetSoul().ToString();
        DefValue.text = selectedChar.GetDefense().ToString();
        ExpValue.text = selectedChar.GetCurExp() + "/" + selectedChar.GetLvlExp();

        //collect character skills from character        
        charSkills = selectedChar.GetAllSkills();
    }

    //destroys objects that holds skills, meant to remove one classes skills when player selects another character
    private void refreshSkills()
    {
        foreach (Transform child in skillScroll.transform)
        {
            Destroy(child.gameObject);
        }

    }

    //loads skills into skill panel for character currently selected
    private void enableSkills()
    {        
        foreach (KeyValuePair<string, SkillClass> skills in charSkills)
        {
            string key = skills.Key;

            GameObject skillBut = (GameObject)Instantiate(skillButton) as GameObject;
            skillBut.transform.SetParent(skillScroll.transform, false);

            PopulateSkillName skillNames = skillBut.GetComponent<PopulateSkillName>();
            skillNames.skillName(charSkills[key]);
        }
    }

    //get which character is currently casting a spell from the stat screen
    public PlayerClass GetCastingChar() { return charSelected; }
}
