using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castHeals : MonoBehaviour {

    public static castHeals heal;

    private List<ScriptablePlayerClasses> chars = new List<ScriptablePlayerClasses>();

    SkillScriptObject castedSkill;
    ScriptablePlayerClasses castingChar;

    // Use this for initialization
    void Start () {
        heal = this;

        chars.Add(Manager.manager.GetPlayer("Player1"));
        chars.Add(Manager.manager.GetPlayer("Player2"));
        chars.Add(Manager.manager.GetPlayer("Player3"));
        chars.Add(Manager.manager.GetPlayer("Player4"));
    }

    //casting character heals selected character determined by spell used
    public void healChar(int character)
    {
        castedSkill = SkillInfo.sInfo.getCastedSkill();
        castingChar = StatsScreen.stats.GetPlayerObject();
        int healAmount;

        if(castedSkill.skillType.Equals("Heal"))
        {
            healAmount = Mathf.RoundToInt((float)castingChar.GetSoul() * castedSkill.skillModifier) + castedSkill.skillBase;
            setHealth(healAmount, character);
            SkillInfo.sInfo.healButtonInteract();
        }            
        else
        {
            healAmount = Mathf.RoundToInt(chars[character].levelHp[chars[character].level] * castedSkill.skillModifier);
            setHealth(healAmount, character);
            SkillInfo.sInfo.reviveButtonInteract();
        }

        SkillInfo.sInfo.healSlider();
        MiniStats.stats.updateSliders();
        StatsScreen.stats.SetStats();

        if (castingChar.currentMp < castedSkill.manaCost)
            SkillInfo.sInfo.closeSkill();
    }

    //changes health of character healed and subtracts mana from casting character
    private void setHealth(int healFor, int character)
    {
        chars[character].changeHP(healFor);
        castingChar.changeMP(-castedSkill.manaCost);
    }
}
