using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatScreen : MonoBehaviour {

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

    public List<Text> SkillNames;
    public List<Text> SkillMPs;
    public List<Button> SkillCasts;

    public Button char1;
    public Button char2;
    public Button char3;
    public Button char4;    

    //opens or closes character stat screen
    public void displayCharScreen()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
            changeChar(1);
        }
            
    }

    //sets which character has been selected to view
    public void changeChar(int charNum)
    {
        List<string> charSkills = null;
        int listSize = 0;

        CharacterClass character1 = CharacterManager.charManager.character1;
        CharacterClass character2 = CharacterManager.charManager.character2;
        CharacterClass character3 = CharacterManager.charManager.character3;
        CharacterClass character4 = CharacterManager.charManager.character4;

        char1.GetComponent<Image>().sprite = CharacterManager.charManager.getChar1Sprite();
        char2.GetComponent<Image>().sprite = CharacterManager.charManager.getChar2Sprite();
        char3.GetComponent<Image>().sprite = CharacterManager.charManager.getChar3Sprite();
        char4.GetComponent<Image>().sprite = CharacterManager.charManager.getChar4Sprite();

        switch (charNum)
        {
            case 1:
                //fill in screen data with char info
                charName.text = character1.GetCharName();
                classValue.text = character1.GetCharClass();
                LvlValue.text = character1.GetCharLevel().ToString();
                HPValue.text = character1.GetCharCurrentHp() + "/" + character1.GetCharMaxHp();
                MPValue.text = character1.GetCharCurrentMp() + "/" + character1.GetCharMaxMp();
                StrValue.text = character1.GetCharStr().ToString();
                AgiValue.text = character1.GetCharAgi().ToString();
                MindValue.text = character1.GetCharMind().ToString();
                SoulValue.text = character1.GetCharSoul().ToString();
                DefValue.text = character1.GetCharDef().ToString();
                ExpValue.text = character1.GetCurExp() + "/" + character1.GetLvlExp();

                //make char1 button selected
                char1.Select();

                //collect character skills from character
                try
                {
                    charSkills = character1.GetCharSkills();
                    listSize = charSkills.Count;
                }
                catch
                {
                    listSize = 0;
                }
                          
                break;
            case 2:
                charName.text = character2.GetCharName();
                classValue.text = character2.GetCharClass();
                LvlValue.text = character2.GetCharLevel().ToString();
                HPValue.text = character2.GetCharCurrentHp() + "/" + character2.GetCharMaxHp();
                MPValue.text = character2.GetCharCurrentMp() + "/" + character2.GetCharMaxMp();
                StrValue.text = character2.GetCharStr().ToString();
                AgiValue.text = character2.GetCharAgi().ToString();
                MindValue.text = character2.GetCharMind().ToString();
                SoulValue.text = character2.GetCharSoul().ToString();
                DefValue.text = character2.GetCharDef().ToString();
                ExpValue.text = character2.GetCurExp() + "/" + character2.GetLvlExp();

                char2.Select();

                try
                {
                    charSkills = character2.GetCharSkills();
                    listSize = charSkills.Count;
                }
                catch
                {
                    listSize = 0;
                }
                break;

            case 3:
                charName.text = character3.GetCharName();
                classValue.text = character3.GetCharClass();
                LvlValue.text = character3.GetCharLevel().ToString();
                HPValue.text = character3.GetCharCurrentHp() + "/" + character3.GetCharMaxHp();
                MPValue.text = character3.GetCharCurrentMp() + "/" + character3.GetCharMaxMp();
                StrValue.text = character3.GetCharStr().ToString();
                AgiValue.text = character3.GetCharAgi().ToString();
                MindValue.text = character3.GetCharMind().ToString();
                SoulValue.text = character3.GetCharSoul().ToString();
                DefValue.text = character3.GetCharDef().ToString();
                ExpValue.text = character3.GetCurExp() + "/" + character3.GetLvlExp();

                char3.Select();

                try
                {
                    charSkills = character3.GetCharSkills();
                    listSize = charSkills.Count;
                }
                catch
                {
                    listSize = 0;
                }
                break;
            case 4:
                charName.text = character4.GetCharName();
                classValue.text = character4.GetCharClass();
                LvlValue.text = character4.GetCharLevel().ToString();
                HPValue.text = character4.GetCharCurrentHp() + "/" + character4.GetCharMaxHp();
                MPValue.text = character4.GetCharCurrentMp() + "/" + character4.GetCharMaxMp();
                StrValue.text = character4.GetCharStr().ToString();
                AgiValue.text = character4.GetCharAgi().ToString();
                MindValue.text = character4.GetCharMind().ToString();
                SoulValue.text = character4.GetCharSoul().ToString();
                DefValue.text = character4.GetCharDef().ToString();
                ExpValue.text = character4.GetCurExp() + "/" + character4.GetLvlExp();

                char4.Select();

                try
                {
                    charSkills = character4.GetCharSkills();
                    listSize = charSkills.Count;
                }
                catch
                {
                    listSize = 0;
                }
                break;
        }          
            
        //fill in skill information
        for (int i = 0; i < listSize; i++)
        {
            SkillNames[i].text = charSkills[i];
            SkillCasts[i].interactable = true;
            SkillCasts[i].GetComponentInChildren<Text>().text = "Cast";
            //Continue places values into the MP for the skills and determine if the skill is castable outside of battle
        }
        
            //disable skill info for any spots that does not have a skill
            for (int i = listSize; i < 8; i++)
            {
                SkillCasts[i].interactable = false;
                SkillCasts[i].GetComponentInChildren<Text>().text = "";
                SkillNames[i].text = "";
                SkillMPs[i].text = "";
            }
    }
}
