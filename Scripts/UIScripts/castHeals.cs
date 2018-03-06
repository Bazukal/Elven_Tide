using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castHeals : MonoBehaviour {

    public static castHeals heal;

    private Dictionary<string, CharacterClass> chars = new Dictionary<string, CharacterClass>();

    SkillClass castedSkill;
    CharacterClass castingChar;

    // Use this for initialization
    void Start () {
        heal = this;

        chars.Add("char1", CharacterManager.charManager.character1);
        chars.Add("char2", CharacterManager.charManager.character2);
        chars.Add("char3", CharacterManager.charManager.character3);
        chars.Add("char4", CharacterManager.charManager.character4);
    }

    //casting character heals selected character determined by spell used
    public void healChar(string character)
    {
        castedSkill = SkillInfo.sInfo.getCastedSkill();
        castingChar = CharacterStatScreen.stats.GetCastingChar();
        int healAmount;

        if(castedSkill.GetSkillType().Equals("Heal"))
        {
            healAmount = Mathf.RoundToInt((float)castingChar.GetTotalSoul() * castedSkill.GetSkillMod()) + castedSkill.GetModPlus();
            setHealth(healAmount, character);
            SkillInfo.sInfo.healButtonInteract();
        }            
        else
        {
            healAmount = Mathf.RoundToInt(chars[character].GetCharMaxHp() * castedSkill.GetSkillMod());
            setHealth(healAmount, character);
            SkillInfo.sInfo.reviveButtonInteract();
        }

        SkillInfo.sInfo.healSlider();

        CharacterStatScreen.stats.charStats(castingChar);

        if (castingChar.GetCharCurrentMp() < castedSkill.GetSkillMana())
            SkillInfo.sInfo.closeSkill();
    }

    //changes health of character healed and subtracts mana from casting character
    private void setHealth(int healFor, string character)
    {
        chars[character].ChangeCharCurrentHp(healFor);
        castingChar.ChangeCharCurrentMp(-castedSkill.GetSkillMana());
    }
}
