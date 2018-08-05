using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Usable", menuName = "Item/Usable")]
public class UsableItem : ScriptableObject {

    public new string name;
    public string type;
    public string rarity;
    public string cureAilment;
    public string description;

    public int quantity;

    public int healAmount;
    public float reviveAmount;

    public int minLevel;
    public int maxLevel;

    public int buyValue;
    public int sellValue;

    public bool dropped;
    public bool bought;
    public bool chest;

    public void Init(string Name, string Type, string Rarity, string CureAilment, string Desc,
        int Quantity, int Heal, float Revive, int MinLevel, int MaxLevel, int BuyValue, int SellValue,
        bool Dropped, bool Bought, bool Chest)
    {
        name = Name;
        type = Type;
        rarity = Rarity;
        cureAilment = CureAilment;
        description = Desc;
        quantity = Quantity;
        healAmount = Heal;
        reviveAmount = Revive;
        minLevel = MinLevel;
        maxLevel = MaxLevel;
        buyValue = BuyValue;
        sellValue = SellValue;
        dropped = Dropped;
        bought = Bought;
        chest = Chest;
    }

    public void useItem() { quantity--; }
    public void addItem(int add) { quantity += add; } 

    public void usedOnChar(ScriptablePlayerClasses player)
    {
        if (type.Equals("Heal"))
            player.changeHP(healAmount);
        else if (type.Equals("Cure"))
            player.CureStatus(cureAilment);
        else if (type.Equals("Revive"))
            player.changeHP(Mathf.RoundToInt(player.levelHp[player.level] * reviveAmount));

        useItem();
    }
}
