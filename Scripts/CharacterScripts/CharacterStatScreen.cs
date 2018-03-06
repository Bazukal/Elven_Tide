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

    CharacterClass character1;
    CharacterClass character2;
    CharacterClass character3;
    CharacterClass character4;

    private List<SkillClass> charSkills = null;

    private CharacterClass charSelected;

    private void Start()
    {
        character1 = CharacterManager.charManager.character1;
        character2 = CharacterManager.charManager.character2;
        character3 = CharacterManager.charManager.character3;
        character4 = CharacterManager.charManager.character4;

        char1.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character1.GetCharClass());
        char2.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character2.GetCharClass());
        char3.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character3.GetCharClass());
        char4.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character4.GetCharClass());

        stats = this;
    }

    //opens or closes character stat screen
    public void displayCharScreen()
    {
        StoreFinds.stored.activate();

        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            changeChar(1);
        }
            
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

    public void charStats(CharacterClass selectedChar)
    {
        //fill in screen data with char info
        charName.text = selectedChar.GetCharName();
        classValue.text = selectedChar.GetCharClass();
        LvlValue.text = selectedChar.GetCharLevel().ToString();
        HPValue.text = selectedChar.GetCharCurrentHp() + "/" + selectedChar.GetCharMaxHp();
        MPValue.text = selectedChar.GetCharCurrentMp() + "/" + selectedChar.GetCharMaxMp();
        StrValue.text = selectedChar.GetTotalStr().ToString();
        AgiValue.text = selectedChar.GetTotalAgi().ToString();
        MindValue.text = selectedChar.GetTotalMind().ToString();
        SoulValue.text = selectedChar.GetTotalSoul().ToString();
        DefValue.text = selectedChar.GetTotalDef().ToString();
        ExpValue.text = selectedChar.GetCurExp() + "/" + selectedChar.GetLvlExp();

        //collect character skills from character        
        charSkills = selectedChar.GetCharSkills();
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
        foreach (SkillClass skills in charSkills)
        {
            GameObject skillBut = (GameObject)Instantiate(skillButton) as GameObject;
            skillBut.transform.SetParent(skillScroll.transform, false);

            PopulateSkillName skillNames = skillBut.GetComponent<PopulateSkillName>();
            skillNames.skillName(skills);
        }
    }

    //get which character is currently casting a spell from the stat screen
    public CharacterClass GetCastingChar() { return charSelected; }
}
