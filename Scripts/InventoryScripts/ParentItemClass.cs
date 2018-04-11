using System;

[Serializable]
public class ParentItemClass {

    protected string name;
    protected string type;
    protected bool equipable;
    protected bool usable;
    protected bool revive;
    protected int minLevel;
    protected int maxLevel;
    protected string boughtOrDrop;
    protected int buyPrice;
    protected int sellPrice;
    protected int quantity;

    public ParentItemClass() { }

    public ParentItemClass(string Name, string Type, bool Equipable, bool Usable, bool Revive, int MinLevel, int MaxLevel,
        string BoughtOrDrop, int BuyPrice, int SellPrice, int Quantity)
    {
        name = Name;
        type = Type;
        equipable = Equipable;
        usable = Usable;
        revive = Revive;
        minLevel = MinLevel;
        maxLevel = MaxLevel;
        boughtOrDrop = BoughtOrDrop;
        buyPrice = BuyPrice;
        sellPrice = SellPrice;
        quantity = Quantity;
    }

    //getters and setters for Item Class
    public string GetName() { return name; }
    public string GetItemType() { return type; }
    public bool GetEquipable() { return equipable; }
    public bool GetUsable() { return usable; }
    public bool GetRevive() { return revive; }
    public int GetMinLevel() { return minLevel; }
    public int GetMaxLevel() { return maxLevel; }
    public string GetBoughtOrDrop() { return boughtOrDrop; }
    public int GetBuyPrice() { return buyPrice; }
    public int GetSellPrice() { return sellPrice; }
    public int GetQuantity() { return quantity; }

    //change the quantity amount for item
    public void ChangeQuantity(int change) { quantity += change; }
    public void SetQuantity(int amount) { quantity = amount; }
}

[Serializable]
public class UsableItemClass : ParentItemClass
{
    private string cureAilment;
    private int healAmount;

    public UsableItemClass() { }

    public UsableItemClass(string Name, string Type, bool Equipable, bool Usable, bool Revive, int MinLevel, int MaxLevel,
        string BoughtOrDrop, int BuyPrice, int SellPrice, int Quantity, string cure, int heal) : base(Name, Type, Equipable,
            Usable, Revive, MinLevel, MaxLevel, BoughtOrDrop, BuyPrice, SellPrice, Quantity)
    {
        cureAilment = cure;
        healAmount = heal;
    }

    //getters
    public string GetCure() { return cureAilment; }
    public int GetHeal() { return healAmount; }
}

[Serializable]
public class EquipableItemClass : ParentItemClass
{
    private string weaponType;
    private string armorType;
    private int damage;
    private int armor;
    private int strength;
    private int agility;
    private int mind;
    private int soul;

    public EquipableItemClass() { }

    public EquipableItemClass(string Name, string Type, bool Equipable, bool Usable, bool Revive, int MinLevel, int MaxLevel,
        string BoughtOrDrop, int BuyPrice, int SellPrice, int Quantity, string WeaponType, string ArmorType, int Damage,
        int Armor, int Strength, int Agility, int Mind, int Soul) : base(Name, Type, Equipable, Usable, Revive, MinLevel, 
        MaxLevel, BoughtOrDrop, BuyPrice, SellPrice, Quantity)
    {
        weaponType = WeaponType;
        armorType = ArmorType;
        damage = Damage;
        armor = Armor;
        strength = Strength;
        agility = Agility;
        mind = Mind;
        soul = Soul;
    }

    //getters
    public string GetWeaponType() { return weaponType; }
    public string GetArmorType() { return armorType; }
    public int GetDamage() { return damage; }
    public int GetArmor() { return armor; }
    public int GetStr() { return strength; }
    public int GetAgi() { return agility; }
    public int GetMind() { return mind; }
    public int GetSoul() { return soul; }
}
