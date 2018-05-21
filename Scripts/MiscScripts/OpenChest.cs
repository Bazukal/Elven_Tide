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
    private List<EquipmentClass> equipables;
    private List<ItemClass> items;

    private void Start()
    {
        rarities.Add("Common");
        rarities.Add("UnCommon");
        rarities.Add("Rare");
        rarities.Add("Mythical");

        equipables = GameItems.gItems.getEquipableInRange(itemLevel);
        items = GameItems.gItems.getUsableInRange(itemLevel);
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
            List<EquipmentClass> rarityEquip = new List<EquipmentClass>();
            int rarityCount = rarityEquip.Count;

            do
            {
                foreach (EquipmentClass equip in equipables)
                {
                    if (equip.GetRarity() == rarities[rarity])
                        rarityEquip.Add(equip);
                }
                rarityCount = rarityEquip.Count;

                if (rarityCount == 0)
                    rarity--;
            }
            while (rarityCount == 0);
            

            int itemEarned = Random.Range(0, rarityCount);
            EquipmentClass itemGained = rarityEquip[itemEarned];
            CharacterInventory.charInven.addEquipableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}.", itemGained.GetName());
        }
        else if(chestHolds > 70)
        {
            List<ItemClass> rarityItem = new List<ItemClass>();
            int rarityCount = rarityItem.Count;

            do
            {
                foreach (ItemClass item in items)
                {
                    if (item.GetRarity() == rarities[rarity])
                        rarityItem.Add(item);
                }
                rarityCount = rarityItem.Count;

                if (rarityCount == 0)
                    rarity--;
            }
            while (rarityCount == 0);

            int itemEarned = Random.Range(0, rarityCount);
            ItemClass itemGained = rarityItem[itemEarned];            
            CharacterInventory.charInven.addUsableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}.", itemGained.GetName());
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
