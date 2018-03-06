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
    public string getName() { return name; }
    public string getType() { return type; }
    public bool getEquipable() { return equipable; }
    public bool getUsable() { return usable; }
    public bool getRevive() { return revive; }
    public int getMinLevel() { return minLevel; }
    public int getMaxLevel() { return maxLevel; }
    public string getBoughtOrDrop() { return boughtOrDrop; }
    public int getBuyPrice() { return buyPrice; }
    public int getSellPrice() { return sellPrice; }
    public int getQuantity() { return quantity; }

    //change the quantity amount for item
    public void changeQuantity(int change) { quantity += change; }
    public void setQuantity(int amount) { quantity = amount; }
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
    public string getCure() { return cureAilment; }
    public int getHeal() { return healAmount; }
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
    public string getWeaponType() { return weaponType; }
    public string getArmorType() { return armorType; }
    public int getDamage() { return damage; }
    public int getArmor() { return armor; }
    public int getStr() { return strength; }
    public int getAgi() { return agility; }
    public int getMind() { return mind; }
    public int getSoul() { return soul; }
}
