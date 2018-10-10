using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour {

    public static SkillInfo sInfo;

    public Text nameText;
    public Text descText;

    public Button castButton;

    private SkillScriptObject skill;

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

    private ScriptablePlayer char1;
    private ScriptablePlayer char2;
    private ScriptablePlayer char3;
    private ScriptablePlayer char4;


    private void Start()
    {
        sInfo = this;

        char1 = Manager.manager.GetPlayer("Player1");
        char2 = Manager.manager.GetPlayer("Player2");
        char3 = Manager.manager.GetPlayer("Player3");
        char4 = Manager.manager.GetPlayer("Player4");
    }

    public void populateInfo(SkillScriptObject Skill)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        skill = Skill;

        nameText.text = skill.name;
        descText.text = skill.skillDescription;

        string skillType = skill.skillType;

        ScriptablePlayer castingChar = StatsScreen.stats.GetPlayerObject();
        if (castingChar.currentHp > 0) 
        {
            if (castingChar.currentMp >= skill.manaCost)
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
        else if(castingChar.currentHp == 0)
            castButton.interactable = false;
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

        string skillType = skill.skillType;

        if (skillType.Equals("Heal"))
            healButtonInteract();
        else
            reviveButtonInteract();
    }

    public SkillScriptObject getCastedSkill() { return skill; }

    private void populateCharInfo()
    {
        character1.GetComponent<Image>().sprite = char1.classHead;
        character2.GetComponent<Image>().sprite = char2.classHead;
        character3.GetComponent<Image>().sprite = char3.classHead;
        character4.GetComponent<Image>().sprite = char4.classHead;

        char1Name.text = char1.name;
        char2Name.text = char2.name;
        char3Name.text = char3.name;
        char4Name.text = char4.name;

        healSlider();
    }

    public void healButtonInteract()
    {
        if (char1.currentHp == 0 || char1.currentHp == char1.levelHp[char1.level])
            character1.interactable = false;
        else
            character1.interactable = true;

        if (char2.currentHp == 0 || char2.currentHp == char2.levelHp[char2.level])
            character2.interactable = false;
        else
            character2.interactable = true;

        if (char3.currentHp == 0 || char3.currentHp == char3.levelHp[char3.level])
            character3.interactable = false;
        else
            character3.interactable = true;

        if (char4.currentHp == 0 || char4.currentHp == char4.levelHp[char4.level])
            character4.interactable = false;
        else
            character4.interactable = true;
    }

    public void reviveButtonInteract()
    {
        if (char1.currentHp == 0)
            character1.interactable = true;
        else
            character1.interactable = false;

        if (char2.currentHp == 0)
            character2.interactable = true;
        else
            character2.interactable = false;

        if (char3.currentHp == 0)
            character3.interactable = true;
        else
            character3.interactable = false;

        if (char4.currentHp == 0)
            character4.interactable = true;
        else
            character4.interactable = false;
    }

    public void healSlider()
    {
        char1HP.value = (float)char1.currentHp / (float)char1.levelHp[char1.level];
        char1MP.value = (float)char1.currentMp / (float)char1.levelMp[char1.level];

        char2HP.value = (float)char2.currentHp / (float)char2.levelHp[char2.level];
        char2MP.value = (float)char2.currentMp / (float)char2.levelMp[char2.level];

        char3HP.value = (float)char3.currentHp / (float)char3.levelHp[char3.level];
        char3MP.value = (float)char3.currentMp / (float)char3.levelMp[char3.level];

        char4HP.value = (float)char4.currentHp / (float)char4.levelHp[char4.level];
        char4MP.value = (float)char4.currentMp / (float)char4.levelMp[char4.level];
    }
}
