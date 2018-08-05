using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour {

    public int itemLevel;

    public int minGold;
    public int maxGold;

    public GameObject gainedDisplayPanel;
    public Text gainedDisplayText;

    private List<string> rarities = new List<string>();
    private List<EquipableItem> equipables;
    private List<UsableItem> items;

    private void Start()
    {
        rarities.Add("Common");
        rarities.Add("UnCommon");
        rarities.Add("Rare");
        rarities.Add("Mythical");

        equipables = Manager.manager.getEquipableInRange(itemLevel);
        items = Manager.manager.getUsableInRange(itemLevel);
    }

    //opens chest that player is next to
    public void open()
    {
        gainedDisplayPanel.SetActive(true);
        StoreFinds.stored.BattleActivate();

        int rarity = RarityCheck();


        //random roll to see what kind of item or gold comes out of the chest
        int chestHolds = Random.Range(0, 101);

        if (chestHolds > 90)
        {
            List<EquipableItem> rarityEquip = new List<EquipableItem>();
            int rarityCount = rarityEquip.Count;

            do
            {
                foreach (EquipableItem equip in equipables)
                {
                    if (equip.rarity == rarities[rarity])
                        rarityEquip.Add(equip);
                }
                rarityCount = rarityEquip.Count;

                if (rarityCount == 0)
                    rarity--;
            }
            while (rarityCount == 0);
            

            int itemEarned = Random.Range(0, rarityCount);
            EquipableItem itemGained = rarityEquip[itemEarned];
            Manager.manager.addEquipableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}.", itemGained.name);
        }
        else if(chestHolds > 70)
        {
            List<UsableItem> rarityItem = new List<UsableItem>();
            int rarityCount = rarityItem.Count;

            do
            {
                foreach (UsableItem item in items)
                {
                    if (item.rarity == rarities[rarity])
                        rarityItem.Add(item);
                }
                rarityCount = rarityItem.Count;

                if (rarityCount == 0)
                    rarity--;
            }
            while (rarityCount == 0);

            int itemEarned = Random.Range(0, rarityCount);
            UsableItem itemGained = rarityItem[itemEarned];
            Manager.manager.addUsableToInventory(itemGained.name, 1);

            gainedDisplayText.text = string.Format("Found {0}.", itemGained.name);
        }
        else
        {
            int goldGained = Random.Range(minGold, maxGold);
            Manager.manager.changeGold(goldGained);
            
            gainedDisplayText.text = string.Format("Found {0} Gold.", goldGained);
        }
    }

    //closes chest and destroys chest
    public void closeChest()
    {
        StoreFinds.stored.BattleDeactivate();
        Destroy(gameObject);
    }

    private int RarityCheck()
    {
        int rarityCheck = Random.Range(0, 1001);
        int returnIndex = 0;
        string rarity;

        if (rarityCheck >= 990)
            rarity = "Mythical";
        else if (rarityCheck >= 925)
            rarity = "Rare";
        else if (rarityCheck >= 750)
            rarity = "UnCommon";
        else
            rarity = "Common";

        foreach(string rare in rarities)
        {
            if (rarity.Equals(rare))
                returnIndex = rarities.IndexOf(rare);
        }

        return returnIndex;
    }
}
