using System.Xml.Serialization;
using System.Xml;

public class Character {

    //name of character class
    [XmlAttribute("Class")]
    public string charClass;

    //highest armor the class can wear
    [XmlElement("MaxArmor")]
    public string maxArmor;

    //starting hp
    [XmlElement("MaxHP")]
    public int maxHP;

    //starting mp
    [XmlElement("MaxMP")]
    public int maxMP;

    //starting strength
    [XmlElement("Strength")]
    public int strength;

    //starting Agility
    [XmlElement("Agility")]
    public int agility;

    //starting Mind
    [XmlElement("Mind")]
    public int mind;

    //starting soul
    [XmlElement("Soul")]
    public int soul;

    //starting defense
    [XmlElement("Defense")]
    public int defense;

    //can the class wear a shield
    [XmlElement("CanShield")]
    public bool canShield;

    //starting weapon
    [XmlElement("Weapon")]
    public string weapon;

    //starting offhand
    [XmlElement("OffHand")]
    public string offHand;

    //starting armor
    [XmlElement("Armor")]
    public string armor;

    //starting accessory
    [XmlElement("Accessory")]
    public string accessory;
}
