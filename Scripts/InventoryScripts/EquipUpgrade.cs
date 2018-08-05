using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class EquipUpgrade : MonoBehaviour {

    public static EquipUpgrade upgrade;

    public GameObject equippedScroll;
    public GameObject inventoryScroll;

    public GameObject equippedButton;
    public GameObject inventoryButton;

    public GameObject statsPanel;

    public Text itemName;

    public Text currentDamage;
    public Text currentArmor;
    public Text currentStr;
    public Text currentAgi;
    public Text currentMind;
    public Text currentSoul;

    public Text upgradeDamage;
    public Text upgradeArmor;
    public Text upgradeStr;
    public Text upgradeAgi;
    public Text upgradeMind;
    public Text upgradeSoul;

    public Text oreType;
    public Text oreQuantity;
    public Text goldQuantity;

    public Button upgradeButton;

    private string oreNeeded;
    private List<int> oreRequired = new List<int>(){3, 7, 12, 23, 40};
    private int goldRequired;

    private EquipableItem itemToUpgrade;
    int upgradesDone = 0;

    private void Start()
    {
        upgrade = this;
    }

    public void openUpgrade()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        populateItems();
    }

    public void populateItems()
    {
        foreach (Transform child in equippedScroll.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in inventoryScroll.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        SortedDictionary<int, EquipableItem> inventoryItems = new SortedDictionary<int, EquipableItem>();

        inventoryItems = Manager.manager.getEquipableInventory();

        foreach(KeyValuePair<int, EquipableItem> item in inventoryItems)
        {
            int key = item.Key;
            EquipableItem currentItem = inventoryItems[key];

            int itemUpgraded = currentItem.upgradesDone;
            int maxUpgrades = currentItem.maxUpgrades;

            if(itemUpgraded < maxUpgrades)
            {
                GameObject invenItem = (GameObject)Instantiate(inventoryButton) as GameObject;
                invenItem.transform.SetParent(inventoryScroll.transform, false);

                UpgradeInvButton buttonScript = invenItem.GetComponent<UpgradeInvButton>();
                buttonScript.AddItemToButton(currentItem);
            }
        }

        List<string> playerTags = new List<string>() { "Player1", "Player2", "Player3", "Player4" };

        foreach(string tag in playerTags)
        {
            ScriptablePlayerClasses player = Manager.manager.GetPlayer(tag);

            List<EquipableItem> equipment = new List<EquipableItem>() { player.weapon, player.offHand,
            player.armor, player.accessory};

            foreach(EquipableItem equip in equipment)
            {
                if(equip != null)
                {
                    int itemUpgraded = equip.upgradesDone;
                    int maxUpgrades = equip.maxUpgrades;

                    if (itemUpgraded < maxUpgrades)
                    {
                        GameObject equippedItem = (GameObject)Instantiate(equippedButton) as GameObject;
                        equippedItem.transform.SetParent(equippedScroll.transform, false);

                        UpgradeEquipButton buttonScript = equippedItem.GetComponent<UpgradeEquipButton>();
                        buttonScript.AddItemToButton(equip, player.name);
                    }
                }
            }
        }
    }

    public void openStatsPanel(EquipableItem item)
    {
        itemToUpgrade = item;

        statsPanel.GetComponent<CanvasGroup>().alpha = 1;
        statsPanel.GetComponent<CanvasGroup>().interactable = true;
        statsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        upgradesDone = item.upgradesDone;

        currentDamage.text = item.currentDamage.ToString();
        currentArmor.text = item.currentArmor.ToString();
        currentStr.text = item.currentStrength.ToString();
        currentAgi.text = item.currentAgility.ToString();
        currentMind.text = item.currentMind.ToString();
        currentSoul.text = item.currentSoul.ToString();

        upgradeDamage.text = item.damage[upgradesDone + 1].ToString();
        upgradeArmor.text = item.armor[upgradesDone + 1].ToString();
        upgradeStr.text = item.strength[upgradesDone + 1].ToString();
        upgradeAgi.text = item.agility[upgradesDone + 1].ToString();
        upgradeMind.text = item.mind[upgradesDone + 1].ToString();
        upgradeSoul.text = item.soul[upgradesDone + 1].ToString();

        StringBuilder sb = new StringBuilder();

        string oreSize = item.maxLevel <= 15 ? "Small " : item.maxLevel <= 30 ? "Medium " :
            item.maxLevel <= 45 ? "" : item.maxLevel <= 60 ? "Large " : "Gigantic ";

        string oreTypeNeeded = item.type.Equals("Weapon") ? "Klogyte" : item.type.Equals("Armor") ?
            "Formatyte" : "Ragalyte";

        sb.Append(oreSize);
        sb.Append(oreTypeNeeded);

        oreNeeded = sb.ToString();
        oreType.text = oreNeeded;
        oreQuantity.text = oreRequired[upgradesDone].ToString();

        List<int> upgradeCosts = item.upgradeCosts;
        goldRequired = upgradeCosts[upgradesDone];
        goldQuantity.text = goldRequired.ToString();

        int oreAvailable = Manager.manager.GetOre(oreNeeded);
        int goldAvailable = Manager.manager.GetGold();

        if (oreRequired[upgradesDone] > oreAvailable || goldRequired > goldAvailable)
            upgradeButton.interactable = false;
        else
            upgradeButton.interactable = true;
    }

    public void upgradeItem()
    {
        itemToUpgrade.UpgradeDone();
        Manager.manager.ChangeOre(oreNeeded, -oreRequired[upgradesDone]);
        Manager.manager.changeGold(-goldRequired);

        closeUpgradeStats();

        populateItems();
    }

    public void closeUpgradeStats()
    {
        statsPanel.GetComponent<CanvasGroup>().alpha = 0;
        statsPanel.GetComponent<CanvasGroup>().interactable = false;
        statsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void closeUpgradeScreen()
    {
        StoreFinds.stored.BattleDeactivate();
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
