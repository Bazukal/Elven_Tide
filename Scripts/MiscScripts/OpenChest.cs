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

	public void open()
    {
        gainedDisplayPanel.SetActive(true);

        //random roll to see what kind of item or gold comes out of the chest
        int chestHolds = Random.Range(0, 101);

        if (chestHolds > 90)
        {
            int itemEarned = Random.Range(0, equipNames.Count);
            EquipableItemClass itemGained = GameItems.gItems.findEquipItem(equipNames[itemEarned]);
            itemGained.changeQuantity(equipQuantity[itemEarned]);
            CharacterInventory.charInven.addEquipableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}x {1}.", itemGained.getQuantity(), itemGained.getName());
        }
        else if(chestHolds > 70)
        {
            int itemEarned = Random.Range(0, useNames.Count);
            UsableItemClass itemGained = GameItems.gItems.findUseItem(useNames[itemEarned]);
            itemGained.changeQuantity(useQuantity[itemEarned]);
            CharacterInventory.charInven.addUsableToInventory(itemGained);

            gainedDisplayText.text = string.Format("Found {0}x {1}.", itemGained.getQuantity(), itemGained.getName());
        }
        else
        {
            int goldGained = Random.Range(minGold, maxGold);
            CharacterManager.charManager.changeGold(goldGained);
            
            gainedDisplayText.text = string.Format("Found {0} Gold.", goldGained);
        }
    }
}
