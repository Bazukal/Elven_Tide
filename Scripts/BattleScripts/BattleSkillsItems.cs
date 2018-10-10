using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BattleSkillsItems : MonoBehaviour {

    public static BattleSkillsItems view;

    public GameObject charPanel;

    public List<Text> charNames;

    public List<Slider> hpSliders;
    public List<Slider> mpSliders;
    public List<GameObject> charStatus;

    public GameObject instantSkill;
    public GameObject instantItem;
    public GameObject instantScroll;

    private List<ScriptablePlayer> chars;
    private Dictionary<string, DebuffClass> char1Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char2Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char3Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char4Debuffs = new Dictionary<string, DebuffClass>();
    private List<Dictionary<string, DebuffClass>> debuffs = new List<Dictionary<string, DebuffClass>>();

    public List<Button> charButtons = new List<Button>();

    private void Awake()
    {
        view = this;
        chars = new List<ScriptablePlayer>();

        chars.Add(Manager.manager.GetPlayer("Player1"));
        chars.Add(Manager.manager.GetPlayer("Player2"));
        chars.Add(Manager.manager.GetPlayer("Player3"));
        chars.Add(Manager.manager.GetPlayer("Player4"));

        for(int i = 0; i < chars.Count; i++)
        {
            charButtons[i].GetComponent<Image>().sprite = chars[i].classHead;
        }

        displayNames();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        sliders();
        statusIcons();
    }

    public void showSkills()
    {
        removeObjects();
        ScriptablePlayer castingChar = getCastingChar();

        Dictionary<string, SkillScriptObject> skills = castingChar.GetAllActiveSkills();

        resetListing();

        foreach (KeyValuePair<string, SkillScriptObject> skill in skills)
        {
            string key = skill.Key;            

            if(!key.Equals("Attack"))
            {
                int charMana = castingChar.currentMp;
                int manaCost = skills[key].manaCost;

                GameObject skillBut = (GameObject)Instantiate(instantSkill) as GameObject;
                skillBut.transform.SetParent(instantScroll.transform, false);

                BattleSkill battleSkill = skillBut.GetComponent<BattleSkill>();
                battleSkill.skillName(skills[key]);

                if (manaCost > charMana)
                    skillBut.GetComponent<Button>().interactable = false;
                else
                    skillBut.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void showItems()
    {
        removeObjects();

        Dictionary<string, UsableItem> items = Manager.manager.getUsableInventory();

        resetListing();

        foreach (KeyValuePair<string, UsableItem> item in items)
        {
            string key = item.Key;

            GameObject itemBut = (GameObject)Instantiate(instantItem) as GameObject;
            itemBut.transform.SetParent(instantScroll.transform, false);

            BattleItem battleItem = itemBut.GetComponent<BattleItem>();
            battleItem.itemName(items[key]);
        }
    }

    private void resetListing()
    {
        foreach (Transform oldSkill in instantScroll.transform)
        {
            Destroy(oldSkill.gameObject);
        }
    }

    public void charSelect(SkillScriptObject skill, UsableItem item)
    {
        charPanel.SetActive(true);

        if (skill != null)
        {
            string skillType = skill.skillType;

            if(skillType.Equals("Buff"))
            {
                foreach(Button character in charButtons)
                {
                    character.interactable = true;
                }
            }
            else if(skillType.Equals("Heal"))
            {
                healButtons();
            }
            else if(skillType.Equals("Revive"))
            {
                reviveButtons();
            }
            else if(skillType.Equals("Cure"))
            {
                string cureStatus = skill.ailment;
                cureButtons(cureStatus);
            }
        }
        else
        {
            string itemType = item.type;

            if(itemType.Equals("Revive"))
            {
                reviveButtons();
            }
            else if(itemType.Equals("Heal"))
            {
                healButtons();
            }
            else
            {
                string cureStatus = item.cureAilment;
                cureButtons(cureStatus);
            }
        }
    }

    private void healButtons()
    {
        int index = 0;
        foreach (ScriptablePlayer characters in chars)
        {
            if (characters.currentHp > 0 && characters.currentHp < characters.levelHp[characters.level])
            {
                charButtons[index].interactable = true;
            }
            else
            {
                charButtons[index].interactable = false;
            }

            index++;
        }
    }

    private void reviveButtons()
    {
        int index = 0;
        foreach (ScriptablePlayer characters in chars)
        {
            if (characters.currentHp == 0)
            {
                charButtons[index].interactable = true;
            }
            else
            {
                charButtons[index].interactable = false;
            }

            index++;
        }
    }

    private void cureButtons(string ailment)
    {
        int index = 0;

        foreach (ScriptablePlayer characters in chars)
        {
            bool afflicted = characters.CheckAffliction(ailment);
            if (afflicted)
            {
                charButtons[index].interactable = true;
            }
            else
            {
                charButtons[index].interactable = false;
            }

            index++;
        }
    }

    public void cancelCharSelect()
    {
        charPanel.SetActive(false);
    }

    public void cancelUse()
    {
        gameObject.SetActive(false);
    }

    private void removeObjects()
    {
        foreach(Transform objects in instantScroll.transform)
        {
            Destroy(objects.gameObject);
        }
    }

    private ScriptablePlayer getCastingChar()
    {
        ScriptablePlayer charTurn = BattleScript.battleOn.getCharactersTurn();

        return charTurn;
    }

    private void sliders()
    {
        for(int i = 0; i < 4;i++)
        {
            int charLevel = chars[i].level;

            float hpValue = (float)chars[i].currentHp / (float)chars[i].levelHp[charLevel];
            float mpValue = (float)chars[i].currentMp / (float)chars[i].levelMp[charLevel];

            hpSliders[i].value = hpValue;
            mpSliders[i].value = mpValue;
        }
    }

    private void displayNames()
    {
        for(int i = 0;i < charNames.Count;i++)
        {
            string charName = chars[i].name;
            charNames[i].text = charName;
        }
    }

    private void removeIcons()
    {
        debuffs.Clear();
        List<GameObject> icons = new List<GameObject>();
        
        for(int i = 0;i < 4;i++)
        {
            GameObject status = charStatus[i];

            foreach(Transform child in status.transform)
            {
                icons.Add(child.gameObject);
            }
        }

        foreach(GameObject icon in icons)
        {
            DestroyImmediate(icon.gameObject);
        }
    }

    private void statusIcons()
    {
        removeIcons();

        char1Debuffs = chars[0].GetDebuffs();
        debuffs.Add(char1Debuffs);
        char2Debuffs = chars[1].GetDebuffs();
        debuffs.Add(char2Debuffs);
        char3Debuffs = chars[2].GetDebuffs();
        debuffs.Add(char3Debuffs);
        char4Debuffs = chars[3].GetDebuffs();
        debuffs.Add(char4Debuffs);

        for(int i = 0;i < 4;i++)
        {
            Dictionary<string, DebuffClass> tempDic = debuffs[i];

            foreach(KeyValuePair<string, DebuffClass> debuff in tempDic)
            {
                string key = debuff.Key;
                bool effected = tempDic[key].GetEffected();

                if(effected)
                {
                    GameObject icon = tempDic[key].GetIcon();
                    GameObject debuffIcon = Instantiate(icon) as GameObject;
                    debuffIcon.transform.SetParent(charStatus[i].transform, false);
                }
            }
        }
    }
}
