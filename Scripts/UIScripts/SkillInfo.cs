using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour {

    public static SkillInfo sInfo;

    public Text nameText;
    public Text descText;

    public Button castButton;

    private SkillClass skill;

    public GameObject healPanel;
    public Button character1;
    public Button character2;
    public Button character3;
    public Button character4;

    public Slider char1HP;
    public Slider char1MP;
    public Slider char2HP;
    public Slider char2MP;
    public Slider char3HP;
    public Slider char3MP;
    public Slider char4HP;
    public Slider char4MP;

    public Text char1Name;
    public Text char2Name;
    public Text char3Name;
    public Text char4Name;

    private CharacterClass char1;
    private CharacterClass char2;
    private CharacterClass char3;
    private CharacterClass char4;


    private void Start()
    {
        sInfo = this;

        char1 = CharacterManager.charManager.character1;
        char2 = CharacterManager.charManager.character2;
        char3 = CharacterManager.charManager.character3;
        char4 = CharacterManager.charManager.character4;
    }

    public void populateInfo(SkillClass Skill)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        skill = Skill;

        nameText.text = skill.GetSkillName();
        descText.text = skill.GetSkillDesc();

        string skillType = skill.GetSkillType();

        CharacterClass castingChar = CharacterStatScreen.stats.GetCastingChar();
        if(castingChar.GetCharCurrentHp() > 0)
        {
            if (castingChar.GetCharCurrentMp() >= skill.GetSkillMana())
            {
                if (skillType.Equals("Heal") || skillType.Equals("Revive"))
                    castButton.interactable = true;
                else
                    castButton.interactable = false;
            }
            else
            {
                castButton.interactable = false;
            }
        }        
    }

    public void closeSkill()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if(healPanel.GetComponent<CanvasGroup>().alpha == 1)
        {
            healPanel.GetComponent<CanvasGroup>().alpha = 0;
            healPanel.GetComponent<CanvasGroup>().interactable = false;
            healPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void castSkill()
    {
        healPanel.GetComponent<CanvasGroup>().alpha = 1;
        healPanel.GetComponent<CanvasGroup>().interactable = true;
        healPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        populateCharInfo();

        string skillType = skill.GetSkillType();

        if (skillType.Equals("Heal"))
            healButtonInteract();
        else
            reviveButtonInteract();
    }

    public SkillClass getCastedSkill() { return skill; }

    private void populateCharInfo()
    {
        character1.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(char1.GetCharClass());
        character2.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(char2.GetCharClass());
        character3.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(char3.GetCharClass());
        character4.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(char4.GetCharClass());

        char1Name.text = char1.GetCharName();
        char2Name.text = char2.GetCharName();
        char3Name.text = char3.GetCharName();
        char4Name.text = char4.GetCharName();

        healSlider();
    }

    public void healButtonInteract()
    {
        if (char1.GetCharCurrentHp() == 0 || char1.GetCharCurrentHp() == char1.GetCharMaxHp())
            character1.interactable = false;
        else
            character1.interactable = true;

        if (char2.GetCharCurrentHp() == 0 || char2.GetCharCurrentHp() == char2.GetCharMaxHp())
            character2.interactable = false;
        else
            character2.interactable = true;

        if (char3.GetCharCurrentHp() == 0 || char3.GetCharCurrentHp() == char3.GetCharMaxHp())
            character3.interactable = false;
        else
            character3.interactable = true;

        if (char4.GetCharCurrentHp() == 0 || char4.GetCharCurrentHp() == char4.GetCharMaxHp())
            character4.interactable = false;
        else
            character4.interactable = true;
    }

    public void reviveButtonInteract()
    {
        if (char1.GetCharCurrentHp() == 0)
            character1.interactable = true;
        else
            character1.interactable = false;

        if (char2.GetCharCurrentHp() == 0)
            character2.interactable = true;
        else
            character2.interactable = false;

        if (char3.GetCharCurrentHp() == 0)
            character3.interactable = true;
        else
            character3.interactable = false;

        if (char4.GetCharCurrentHp() == 0)
            character4.interactable = true;
        else
            character4.interactable = false;
    }

    public void healSlider()
    {
        char1HP.value = (float)char1.GetCharCurrentHp() / (float)char1.GetCharMaxHp();
        char1MP.value = (float)char1.GetCharCurrentMp() / (float)char1.GetCharMaxMp();

        char2HP.value = (float)char2.GetCharCurrentHp() / (float)char2.GetCharMaxHp();
        char2MP.value = (float)char2.GetCharCurrentMp() / (float)char2.GetCharMaxMp();

        char3HP.value = (float)char3.GetCharCurrentHp() / (float)char3.GetCharMaxHp();
        char3MP.value = (float)char3.GetCharCurrentMp() / (float)char3.GetCharMaxMp();

        char4HP.value = (float)char4.GetCharCurrentHp() / (float)char4.GetCharMaxHp();
        char4MP.value = (float)char4.GetCharCurrentMp() / (float)char4.GetCharMaxMp();
    }
}
