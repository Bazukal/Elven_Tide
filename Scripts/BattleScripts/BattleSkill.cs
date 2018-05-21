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
        skillsName.text = skill.GetName();
        mpCost.text = "MP: " + skill.GetCost().ToString();
    }

    public void castSkill()
    {
        if (selectedSkill.GetTarget().Equals("Enemy"))
        {
            StateMachine.state.skillAction(selectedSkill);
        }            
        else
        {
            BattleSkillsItems.view.charSelect(selectedSkill, null);
            StateMachine.state.healingSkill(selectedSkill);
        }
            
    }
}
