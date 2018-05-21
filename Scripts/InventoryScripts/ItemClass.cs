using System;

[Serializable]
public class ItemClass {

    private string name;
    private string type;
    private string findRarity;
    private string cureCondition;
    private string description;

    private int healAmount;

    private int minLevel;
    private int maxLevel;
    private int buyPrice;
    private int sellPrice;
    private int quantity;

    private float revivePercent;

    private bool bought;
    private bool drop;
    private bool chest;  
    
    public ItemClass() { }

    public ItemClass(string Name, string Type, string Rarity, string Cure, string Description, 
        int Heal, int MinLevel, int MaxLevel, int Buy, int Sell, float Revive, bool Bought, bool Drop, 
        bool Chest)
    {
        name = Name;
        type = Type;
        findRarity = Rarity;
        cureCondition = Cure;
        description = Description;
        healAmount = Heal;
        minLevel = MinLevel;
        maxLevel = MaxLevel;
        buyPrice = Buy;
        sellPrice = Sell;
        revivePercent = Revive;
        bought = Bought;
        drop = Drop;
        chest = Chest;
    }

    //getters and setters
    public string GetName() { return name; }
    public string GetItemType() { return type; }
    public string GetRarity() { return findRarity; }
    public string GetCure() { return cureCondition; }
    public string GetDesc() { return description; }

    public int GetHeal() { return healAmount; }
    public int GetMinLevel() { return minLevel; }
    public int GetMaxLevel() { return maxLevel; }
    public int GetBuy() { return buyPrice; }
    public int GetSell() { return sellPrice; }
    public int GetQuantity() { return quantity; }

    public void SetQuantity(int quantity) { quantity = this.quantity; }
    public void ChangeQuantity(int change) { quantity += change; }

    public float GetRevive() { return revivePercent; }

    public bool IsBought() { return bought; }
    public bool IsDropped() { return drop; }
    public bool IsChest() { return chest; }
}
