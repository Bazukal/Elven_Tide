using System.Xml.Serialization;
using System.Xml;

public class Item {
    //name of the item
    [XmlAttribute("Name")]
    public string name;

    //type of the item, weapon, armor, usable
    [XmlElement("Type")]
    public string type;

    //is the item equipable
    [XmlElement("Equipable")]
    public bool equipable;

    //is the item usable
    [XmlElement("Usable")]
    public bool usable;

    //does this item revive dead party members
    [XmlElement("Revive")]
    public bool revive;

    //what type of weapon is the item, null if not a weapon
    [XmlElement("WeaponType")]
    public string weaponType;

    //what type of armor is the item, null if not an armor
    [XmlElement("ArmorType")]
    public string armorType;

    //what status effects does this item cure, null if it does not cure
    [XmlElement("CureAilment")]
    public string cureAilment;

    //how much damage does this item heal for, 0 if it does not heal
    [XmlElement("HealAmount")]
    public int healAmount;

    //if a weapon, how much damage does the weapon have
    [XmlElement("Damage")]
    public int damage;

    //if an armor, how much armor does it give
    [XmlElement("Armor")]
    public int armor;

    //how much strength does the item give
    [XmlElement("Strength")]
    public int strength;

    //how much agility does the item give
    [XmlElement("Agility")]
    public int agility;

    //hoe much mind does the item give
    [XmlElement("Mind")]
    public int mind;

    //how much soul does the item give
    [XmlElement("Soul")]
    public int soul;

    //minimum average level of party for the item to start showing in game
    [XmlElement("MinLevel")]
    public int minLevel;

    //maximum average level of party before the item no longer shows in game
    [XmlElement("MaxLevel")]
    public int maxLevel;

    //can this item be bought, or is it dropped/from chest
    [XmlElement("BoughtOrDrop")]
    public string boughtOrDrop;

    //how much does this item sell for from the vendor
    [XmlElement("BuyPrice")]
    public int buyPrice;

    //how much does the player get if sold to the vendor
    [XmlElement("SellPrice")]
    public int sellPrice;
}
