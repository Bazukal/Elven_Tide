using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkill : MonoBehaviour {

    private SkillClass selectedSkill;

    public Text skillsName;
    public Text mpCost;

    public void skillName(SkillClass skill)
    {
        selectedSkill = skill;
        skillsName.text = skill.GetSkillName();
        mpCost.text = "MP: " + skill.GetSkillMana().ToString();
    }

    public void castSkill()
    {
        if (selectedSkill.GetSkillTarget().Equals("Enemy"))
        {
            Battle.battle.skillAction(selectedSkill);
        }            
        else
        {
            BattleSkillsItems.view.charSelect(selectedSkill, null);
            Battle.battle.healingSkill(selectedSkill);
        }
            
    }
}
