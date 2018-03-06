using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour {

    public List<string> equipNames;
    public List<int> equipQuantity;
    public List<string> useNames;
    public List<int> useQuantity;

    public int minGold;
    public int maxGold;

    public GameObject gainedDisplayPanel;
    public Text gainedDisplayText;

    //opens chest that player is next to
	public void open()
    {
        gainedDisplayPanel.SetActive(true);
        StoreFinds.stored.activate();

        //random roll to see what kind of item or gold comes out of the chest
        int chestHolds = Random.Range(0, 101);

        if (chestHolds > 90)
        {
            int itemEarned = Random.Range(0, equipNames.Count);
            EquipableItemClass itemGained = GameItems.gItems.findEquipItem(equipNames[itemEarned]);
            int itemAmount = equipQuantity[itemEarned];
            itemGained.setQuantity(itemAmount);
            CharacterInventory.charInven.addEquipableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}x {1}.", itemAmount, itemGained.getName());
        }
        else if(chestHolds > 70)
        {
            int itemEarned = Random.Range(0, useNames.Count);
            UsableItemClass itemGained = GameItems.gItems.findUseItem(useNames[itemEarned]);
            int itemAmount = useQuantity[itemEarned];
            itemGained.setQuantity(itemAmount);
            CharacterInventory.charInven.addUsableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}x {1}.", itemAmount, itemGained.getName());
        }
        else
        {
            int goldGained = Random.Range(minGold, maxGold);
            CharacterManager.charManager.changeGold(goldGained);
            
            gainedDisplayText.text = string.Format("Found {0} Gold.", goldGained);
        }
    }

    //closes chest and destroys chest
    public void closeChest()
    {
        StoreFinds.stored.activate();
        Destroy(gameObject);
    }
}
