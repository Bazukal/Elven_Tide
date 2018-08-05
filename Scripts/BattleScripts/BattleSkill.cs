using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkill : MonoBehaviour {

    private SkillScriptObject selectedSkill;

    public Text skillsName;
    public Text mpCost;

    public void skillName(SkillScriptObject skill)
    {
        selectedSkill = skill;
        skillsName.text = skill.skillName;
        mpCost.text = "MP: " + skill.manaCost.ToString();
    }

    public void castSkill()
    {
        if (selectedSkill.targetEnemy == true)
        {
            BattleScript.battleOn.attackSkill(selectedSkill);
        }            
        else
        {
            BattleSkillsItems.view.charSelect(selectedSkill, null);
            BattleScript.battleOn.healingSkill(selectedSkill);
        }
            
    }
}
