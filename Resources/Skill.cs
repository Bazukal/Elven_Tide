using System.Xml.Serialization;
using System.Xml;

public class Skill {
    //name of the skill
    [XmlAttribute("Name")]
    public string name;

    //is the skill a damage, status affliction, heal, revive, or cure skill
    [XmlElement("Type")]
    public string type;

    //which player class gets this skill, or enemy
    [XmlElement("Class")]
    public string charClass;

    //does the skill target the enemy, or the player
    [XmlElement("Target")]
    public string target;

    //buffs what stat
    [XmlElement("Stat")]
    public string stat;

    //status ailment for skill
    [XmlElement("Ailment")]
    public string ailment;

    //what type of damage skill is this, physical or magical
    [XmlElement("DamageType")]
    public string damageType;

    //description of skill
    [XmlElement("Desc")]
    public string desc;
    
    //at what level does the player get the skill
    [XmlElement("Level")]
    public int level;

    //how much mana does the skill cost
    [XmlElement("Mana")]
    public int mana;

    //additional strength
    [XmlElement("Base")]
    public int skillBase;

    //turns skill is in effect
    [XmlElement("Turns")]
    public int turns;

    //determines the chance the debuff will take effect
    [XmlElement("DebuffChance")]
    public float debuffChance;

    //strength modifier of skill
    [XmlElement("Modifier")]
    public float modifier;

    //is the skill aoe or single target
    [XmlElement("AOE")]
    public bool aoe;    
}
