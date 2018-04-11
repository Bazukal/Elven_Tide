using System.Xml.Serialization;
using System.Xml;

public class Skill {
    //name of the skill
    [XmlAttribute("Name")]
    public string name;

    //which player class gets this skill, or enemy
    [XmlElement("Class")]
    public string charClass;

    //at what level does the player get the skill
    [XmlElement("Level")]
    public int level;

    //is the skill a damage, status affliction, heal, revive, or cure skill
    [XmlElement("Type")]
    public string type;

    //what type of damage skill is this, physical or magical
    [XmlElement("DamageType")]
    public string damageType;

    //determines if the skill has a debuff, and what type of debuff
    [XmlElement("DebuffType")]
    public string debuffType;

    //determines the chance the debuff will take effect
    [XmlElement("DebuffChance")]
    public float debuffChance;

    //how much mana does the skill cost
    [XmlElement("Mana")]
    public int mana;

    //is the skill aoe or single target
    [XmlElement("AOE")]
    public bool aoe;

    //does the skill target the enemy, or the player
    [XmlElement("Target")]
    public string target;

    //strength modifier of skill
    [XmlElement("Strength")]
    public float strength;

    //additional strength
    [XmlElement("Plus")]
    public int plus;

    //cures affliction
    [XmlElement("Cure")]
    public string cure;

    //buffs what stat
    [XmlElement("Stat")]
    public string stat;

    //turns skill is in effect
    [XmlElement("Turns")]
    public int turns;

    //description of skill
    [XmlElement("Desc")]
    public string desc;
}
