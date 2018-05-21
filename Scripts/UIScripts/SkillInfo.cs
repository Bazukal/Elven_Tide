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

    private PlayerClass char1;
    private PlayerClass char2;
    private PlayerClass char3;
    private PlayerClass char4;


    private void Start()
    {
        sInfo = this;

        char1 = Manager.manager.GetPlayer("Player1");
        char2 = Manager.manager.GetPlayer("Player2");
        char3 = Manager.manager.GetPlayer("Player3");
        char4 = Manager.manager.GetPlayer("Player4");
    }

    public void populateInfo(SkillClass Skill)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        skill = Skill;

        nameText.text = skill.GetName();
        descText.text = skill.GetDescription();

        string skillType = skill.GetSkillType();

        PlayerClass castingChar = CharacterStatScreen.stats.GetCastingChar();
        if(castingChar.GetCurrentHP() > 0)
        {
            if (castingChar.GetCurrentMP() >= skill.GetCost())
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
        character1.GetComponent<Image>().sprite = Manager.manager.getCharSprite(char1.GetClass());
        character2.GetComponent<Image>().sprite = Manager.manager.getCharSprite(char2.GetClass());
        character3.GetComponent<Image>().sprite = Manager.manager.getCharSprite(char3.GetClass());
        character4.GetComponent<Image>().sprite = Manager.manager.getCharSprite(char4.GetClass());

        char1Name.text = char1.GetName();
        char2Name.text = char2.GetName();
        char3Name.text = char3.GetName();
        char4Name.text = char4.GetName();

        healSlider();
    }

    public void healButtonInteract()
    {
        if (char1.GetCurrentHP() == 0 || char1.GetCurrentHP() == char1.GetMaxHP())
            character1.interactable = false;
        else
            character1.interactable = true;

        if (char2.GetCurrentHP() == 0 || char2.GetCurrentHP() == char2.GetMaxHP())
            character2.interactable = false;
        else
            character2.interactable = true;

        if (char3.GetCurrentHP() == 0 || char3.GetCurrentHP() == char3.GetMaxHP())
            character3.interactable = false;
        else
            character3.interactable = true;

        if (char4.GetCurrentHP() == 0 || char4.GetCurrentHP() == char4.GetMaxHP())
            character4.interactable = false;
        else
            character4.interactable = true;
    }

    public void reviveButtonInteract()
    {
        if (char1.GetCurrentHP() == 0)
            character1.interactable = true;
        else
            character1.interactable = false;

        if (char2.GetCurrentHP() == 0)
            character2.interactable = true;
        else
            character2.interactable = false;

        if (char3.GetCurrentHP() == 0)
            character3.interactable = true;
        else
            character3.interactable = false;

        if (char4.GetCurrentHP() == 0)
            character4.interactable = true;
        else
            character4.interactable = false;
    }

    public void healSlider()
    {
        char1HP.value = (float)char1.GetCurrentHP() / (float)char1.GetMaxHP();
        char1MP.value = (float)char1.GetCurrentMP() / (float)char1.GetMaxMP();

        char2HP.value = (float)char2.GetCurrentHP() / (float)char2.GetMaxHP();
        char2MP.value = (float)char2.GetCurrentMP() / (float)char2.GetMaxMP();

        char3HP.value = (float)char3.GetCurrentHP() / (float)char3.GetMaxHP();
        char3MP.value = (float)char3.GetCurrentMP() / (float)char3.GetMaxMP();

        char4HP.value = (float)char4.GetCurrentHP() / (float)char4.GetMaxHP();
        char4MP.value = (float)char4.GetCurrentMP() / (float)char4.GetMaxMP();
    }
}
