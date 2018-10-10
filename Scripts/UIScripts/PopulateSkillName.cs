using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSkillName : MonoBehaviour {

    private SkillScriptObject selectedSkill;

	public void skillName(SkillScriptObject skill)
    {
        selectedSkill = skill;
        gameObject.GetComponentInChildren<Text>().text = skill.skillName;
    }

    public void skillInfo()
    {
        SkillInfo.sInfo.populateInfo(selectedSkill);
    }
}
