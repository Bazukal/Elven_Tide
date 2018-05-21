using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSkillName : MonoBehaviour {

    private SkillClass selectedSkill;

	public void skillName(SkillClass skill)
    {
        selectedSkill = skill;
        gameObject.GetComponentInChildren<Text>().text = skill.GetName();
    }

    public void skillInfo()
    {
        SkillInfo.sInfo.populateInfo(selectedSkill);
    }
}
