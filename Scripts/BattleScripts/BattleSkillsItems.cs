using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BattleSkillsItems : MonoBehaviour {

    public static BattleSkillsItems view;

    public GameObject charPanel;

    public Text char1Name;
    public Text char2Name;
    public Text char3Name;
    public Text char4Name;

    public List<Slider> hpSliders;
    public List<Slider> mpSliders;
    public List<GameObject> charStatus;

    public GameObject instantSkill;
    public GameObject instantItem;
    public GameObject instantScroll;

    private List<PlayerClass> chars = new List<PlayerClass>();
    private Dictionary<string, DebuffClass> char1Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char2Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char3Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char4Debuffs = new Dictionary<string, DebuffClass>();
    private List<Dictionary<string, DebuffClass>> debuffs = new List<Dictionary<string, DebuffClass>>();

    public List<Button> charButtons = new List<Button>();

    private void Awake()
    {
        view = this;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        chars = new List<PlayerClass>();

        chars.Add(Manager.manager.GetPlayer("Player1"));
        chars.Add(Manager.manager.GetPlayer("Player2"));
        chars.Add(Manager.manager.GetPlayer("Player3"));
        chars.Add(Manager.manager.GetPlayer("Player4"));

        char1Debuffs = chars[0].GetDebuffs();
        debuffs.Add(char1Debuffs);
        char2Debuffs = chars[1].GetDebuffs();
        debuffs.Add(char2Debuffs);
        char3Debuffs = chars[2].GetDebuffs();
        debuffs.Add(char3Debuffs);
        char4Debuffs = chars[3].GetDebuffs();
        debuffs.Add(char4Debuffs);

        charButtons[0].GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars[0].GetClass());
        charButtons[1].GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars[1].GetClass());
        charButtons[2].GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars[2].GetClass());
        charButtons[3].GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars[3].GetClass());

        sliders();
        statusIcons();
    }

    public void showSkills()
    {
        removeObjects();
        int castingChar = getCastingChar();

        Dictionary<string, SkillClass> skills = chars[castingChar].GetAllSkills();

        resetListing();

        foreach (KeyValuePair<string, SkillClass> skill in skills)
        {
            string key = skill.Key;
            int charMana = chars[castingChar].GetCurrentMP();
            int manaCost = skills[key].GetCost();

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

    public void showItems()
    {
        removeObjects();

        List<ItemClass> items = CharacterInventory.charInven.getUsableInventory();

        resetListing();

        foreach (ItemClass item in items)
        {
            GameObject itemBut = (GameObject)Instantiate(instantItem) as GameObject;
            itemBut.transform.SetParent(instantScroll.transform, false);

            BattleItem battleItem = itemBut.GetComponent<BattleItem>();
            battleItem.itemName(item);
        }
    }

    private void resetListing()
    {
        foreach (Transform oldSkill in instantScroll.transform)
        {
            Destroy(oldSkill.gameObject);
        }
    }

    public void charSelect(SkillClass skill, ItemClass item)
    {
        charPanel.SetActive(true);

        if (skill != null)
        {
            string skillType = skill.GetSkillType();

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
                string cureStatus = skill.GetCondition();
                cureButtons(cureStatus);
            }
        }
        else
        {
            string itemType = item.GetItemType();

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
                string cureStatus = item.GetCure();
                cureButtons(cureStatus);
            }
        }
    }

    private void healButtons()
    {
        int index = 0;
        foreach (PlayerClass characters in chars)
        {
            if (characters.GetCurrentHP() > 0 && characters.GetCurrentHP() < characters.GetMaxHP())
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
        foreach (PlayerClass characters in chars)
        {
            if (characters.GetCurrentHP() == 0)
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

        foreach (PlayerClass characters in chars)
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

    private int getCastingChar()
    {
        int charTurn = StateMachine.state.GetCharTurn();

        return charTurn;
    }

    private void sliders()
    {
        for(int i = 0; i < 4;i++)
        {
            float hpValue = (float)chars[i].GetCurrentHP() / (float)chars[i].GetMaxHP();
            float mpValue = (float)chars[i].GetCurrentMP() / (float)chars[i].GetMaxMP();

            hpSliders[i].value = hpValue;
            mpSliders[i].value = mpValue;
        }
    }

    private void statusIcons()
    {
        int index = 0;

        foreach (Dictionary<string, DebuffClass> debuffList in debuffs)
        {
            foreach(KeyValuePair<string, DebuffClass> debuff in debuffList)
            {
                string key = debuff.Key;
                bool effected = debuffList[key].GetEffected();

                if(effected)
                {
                    GameObject icon = BuffIcons.buffIcons.getBuffIcon(key);
                    GameObject debuffIcon = Instantiate(icon) as GameObject;
                    debuffIcon.transform.SetParent(charStatus[index].transform, false);
                }
            }
        }
    }
}
