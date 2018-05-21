using System.Xml.Serialization;
using System.Xml;

public class Item {
    //name of the item
    [XmlAttribute("Name")]
    public string name;

    //type of the item, heal, revive, cure
    [XmlElement("Type")]
    public string type;

    //how rare is the item to find- common, uncommon, rare, mythical
    [XmlElement("Rarity")]
    public string rarity;

    //what status effects does this item cure, null if it does not cure
    [XmlElement("CureAilment")]
    public string cureAilment;

    //description of the item
    [XmlElement("Description")]
    public string description;

    //how much damage does this item heal for, 0 if it does not heal
    [XmlElement("HealAmount")]
    public int healAmount;

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

    //percentage of max life healed when revived
    [XmlElement("Revive")]
    public float revive;

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
