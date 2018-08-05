using System;

[Serializable]
public class EquipmentClass {

    private string name;
    private int itemID;
    private int upgradesDone;

    public EquipmentClass() { }

    public EquipmentClass(string Name, int ItemID, int Upgrades)
    {
        name = Name;
        itemID = ItemID;
        upgradesDone = Upgrades;
    }

    //getters
    public string GetName() { return name; }
    public int GetID() { return itemID; }
    public int GetUpgrades() { return upgradesDone; }
}
