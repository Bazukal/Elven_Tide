using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castHeals : MonoBehaviour {

    public static castHeals heal;

    private Dictionary<string, PlayerClass> chars = new Dictionary<string, PlayerClass>();

    SkillClass castedSkill;
    PlayerClass castingChar;

    // Use this for initialization
    void Start () {
        heal = this;

        chars.Add("char1", Manager.manager.GetPlayer("Player1"));
        chars.Add("char2", Manager.manager.GetPlayer("Player2"));
        chars.Add("char3", Manager.manager.GetPlayer("Player3"));
        chars.Add("char4", Manager.manager.GetPlayer("Player4"));
    }

    //casting character heals selected character determined by spell used
    public void healChar(string character)
    {
        castedSkill = SkillInfo.sInfo.getCastedSkill();
        castingChar = CharacterStatScreen.stats.GetCastingChar();
        int healAmount;

        if(castedSkill.GetSkillType().Equals("Heal"))
        {
            healAmount = Mathf.RoundToInt((float)castingChar.GetSoul() * castedSkill.GetSkillModifier()) + castedSkill.GetSkillBase();
            setHealth(healAmount, character);
            SkillInfo.sInfo.healButtonInteract();
        }            
        else
        {
            healAmount = Mathf.RoundToInt(chars[character].GetMaxHP() * castedSkill.GetSkillModifier());
            setHealth(healAmount, character);
            SkillInfo.sInfo.reviveButtonInteract();
        }

        SkillInfo.sInfo.healSlider();

        CharacterStatScreen.stats.charStats(castingChar);

        if (castingChar.GetCurrentMP() < castedSkill.GetCost())
            SkillInfo.sInfo.closeSkill();
    }

    //changes health of character healed and subtracts mana from casting character
    private void setHealth(int healFor, string character)
    {
        chars[character].changeHP(healFor);
        castingChar.changeMP(-castedSkill.GetCost());
    }
}
