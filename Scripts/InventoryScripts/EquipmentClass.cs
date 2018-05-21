using System;

[Serializable]
public class EquipmentClass {

    private string name;
    private string type;
    private string equipType;
    private string findRarity;

    private int damage;
    private int armor;
    private int strength;
    private int agility;
    private int mind;
    private int soul;

    private int minLevel;
    private int maxLevel;
    private int buyPrice;
    private int sellPrice;

    private bool bought;
    private bool drop;
    private bool chest;

    public EquipmentClass() { }

    public EquipmentClass(string Name, string Type, string EquipType, string Rarity, int Damage, int Armor, 
        int Strength, int Agility, int Mind, int Soul, int MinLevel, int MaxLevel, int Buy, int Sell, 
        bool Bought, bool Drop, bool Chest)
    {
        name = Name;
        type = Type;
        equipType = EquipType;
        findRarity = Rarity;
        damage = Damage;
        armor = Armor;
        strength = Strength;
        agility = Agility;
        mind = Mind;
        soul = Soul;
        minLevel = MinLevel;
        maxLevel = MaxLevel;
        buyPrice = Buy;
        sellPrice = Sell;
        bought = Bought;
        drop = Drop;
        chest = Chest;
    }

    //getters
    public string GetName() { return name; }
    public string GetItemType() { return type; }
    public string GetEquipType() { return equipType; }
    public string GetRarity() { return findRarity; }

    public int GetDamage() { return damage; }
    public int GetArmor() { return armor; }
    public int GetStr() { return strength; }
    public int GetAgi() { return agility; }
    public int GetMind() { return mind; }
    public int GetSoul() { return soul; }

    public int GetMinLevel() { return minLevel; }
    public int GetMaxLevel() { return maxLevel; }
    public int GetBuyPrice() { return buyPrice; }
    public int GetSellPrice() { return sellPrice; }

    public bool IsBought() { return bought; }
    public bool IsDropped() { return drop; }
    public bool IsChest() { return chest; }
}
