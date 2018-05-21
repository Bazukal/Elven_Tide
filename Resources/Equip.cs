using System.Xml.Serialization;
using System.Xml;

public class Equip {

    //name of the item
    [XmlAttribute("Name")]
    public string name;

    //type of the item, weapon, armor, usable
    [XmlElement("Type")]
    public string type;

    //type of the weapons- sword, dagger, rod, armor- plate, leather, cloth
    [XmlElement("EquipType")]
    public string equipType;

    //how rare is the item to find- common, uncommon, rare, mythical
    [XmlElement("Rarity")]
    public string rarity;

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

    //how much does this item sell for from the vendor
    [XmlElement("BuyPrice")]
    public int buyPrice;

    //how much does the player get if sold to the vendor
    [XmlElement("SellPrice")]
    public int sellPrice;

    //can the item be bought
    [XmlElement("Bought")]
    public bool bought;

    //can the item be dropped
    [XmlElement("Dropped")]
    public bool dropped;

    //can the item be found in a chest
    [XmlElement("Chest")]
    public bool chest;
}
