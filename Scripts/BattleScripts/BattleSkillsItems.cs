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

    public Sprite poisonImage;
    public Sprite confuseImage;
    public Sprite paralyzeImage;
    public Sprite blindImage;
    public Sprite muteImage;

    public GameObject instantSkill;
    public GameObject instantItem;
    public GameObject instantScroll;

    private List<CharacterClass> chars = new List<CharacterClass>();
    private Dictionary<string, DebuffClass> char1Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char2Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char3Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, DebuffClass> char4Debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, Sprite> statusImages = new Dictionary<string, Sprite>();

    public List<Button> charButtons = new List<Button>();

    private void Start()
    {
        view = this;

        gameObject.SetActive(false);

        statusImages.Add("Poison", poisonImage);
        statusImages.Add("Confused", confuseImage);
        statusImages.Add("Paralyzed", paralyzeImage);
        statusImages.Add("Blind", blindImage);
        statusImages.Add("Mute", muteImage);

        charButtons[0].GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(chars[0].GetCharClass());
        charButtons[1].GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(chars[1].GetCharClass());
        charButtons[2].GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(chars[2].GetCharClass());
        charButtons[3].GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(chars[3].GetCharClass());
    }

    private void OnEnable()
    {
        chars = new List<CharacterClass>();

        chars.Add(CharacterManager.charManager.character1);
        chars.Add(CharacterManager.charManager.character2);
        chars.Add(CharacterManager.charManager.character3);
        chars.Add(CharacterManager.charManager.character4);

        char1Debuffs = chars[0].GetDebuffs();
        char2Debuffs = chars[1].GetDebuffs();
        char3Debuffs = chars[2].GetDebuffs();
        char4Debuffs = chars[3].GetDebuffs();

        sliders();
    }

    public void showSkills()
    {
        removeObjects();
        int castingChar = getCastingChar();

        List<SkillClass> skills = chars[castingChar].GetCharSkills();

        resetListing();

        foreach (SkillClass skill in skills)
        {
            GameObject skillBut = (GameObject)Instantiate(instantSkill) as GameObject;
            skillBut.transform.SetParent(instantScroll.transform, false);

            BattleSkill battleSkill = skillBut.GetComponent<BattleSkill>();
            battleSkill.skillName(skill);
        }
    }

    public void showItems()
    {
        removeObjects();

        List<UsableItemClass> items = CharacterInventory.charInven.getUsableInventory();

        resetListing();

        foreach (UsableItemClass item in items)
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

    public void charSelect(SkillClass skill, UsableItemClass item)
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
                string cureStatus = skill.GetSkillCure();
                cureButtons(cureStatus);
            }
        }
        else
        {
            if(item.GetRevive())
            {
                reviveButtons();
            }
            else if(item.GetHeal() > 0)
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
        foreach (CharacterClass characters in chars)
        {
            if (characters.GetCharCurrentHp() > 0 && characters.GetCharCurrentHp() < characters.GetCharMaxHp())
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
        foreach (CharacterClass characters in chars)
        {
            if (characters.GetCharCurrentHp() == 0)
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

        foreach (CharacterClass characters in chars)
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
        int charTurn = Battle.battle.GetCharTurn();

        return charTurn;
    }

    private void sliders()
    {
        for(int i = 0; i < 4;i++)
        {
            float hpValue = (float)chars[i].GetCharCurrentHp() / (float)chars[i].GetCharMaxHp();
            float mpValue = (float)chars[i].GetCharCurrentMp() / (float)chars[i].GetCharMaxMp();

            hpSliders[i].value = hpValue;
            mpSliders[i].value = mpValue;
        }
    }

    private void statusIcons()
    {

    }
}
