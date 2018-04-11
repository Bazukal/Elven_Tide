using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBuy : MonoBehaviour {

    public static shopBuy sBuy;

    public GameObject equipPanel;
    public GameObject usePanel;
    public GameObject scrollContent;

    public Dropdown typeDropdown;

    private void Awake()
    {
        sBuy = this;
    }

    public void shopBuyPanel()
    {
        int aveLvl = CharacterManager.charManager.aveLevel();
        string whosInRange = CharacterManager.charManager.getInRange();
        typeDropdown.GetComponent<Dropdown>();

        //displays items for sell based on if the player is near the blacksmith or the healer
        switch(whosInRange)
        {
            case "Healer":
                //clears any item buttons in the scroll view
                foreach (Transform child in scrollContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                List<UsableItemClass> usableItems = GameItems.gItems.getUsableInRange(aveLvl);
                List<string> healerDropdown = new List<string>() { "Healer" };
                //sets dropdown list for shop
                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(healerDropdown);

                //populates items available for sell into the scroll view
                foreach (UsableItemClass item in usableItems)
                {
                    string bought = item.GetBoughtOrDrop();

                    if(bought == "Bought" || bought == "Both")
                    {
                        GameObject invenItem = (GameObject)Instantiate(usePanel) as GameObject;
                        invenItem.transform.SetParent(scrollContent.transform, false);

                        PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                        popItem.populateUseButtonData(item);
                    }                    
                }
                break;
            case "Blacksmith":
                List<EquipableItemClass> equipableItems = GameItems.gItems.getEquipableInRange(aveLvl);
                List<string> blacksmithDropdown = new List<string>() { "Weapon", "Armor", "Accessory" };
                List<EquipableItemClass> weapons = new List<EquipableItemClass>();
                List<EquipableItemClass> armors = new List<EquipableItemClass>();
                List<EquipableItemClass> accessories = new List<EquipableItemClass>();
                string typeSelected;
                List<Dropdown.OptionData> menuOptions;

                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(blacksmithDropdown);

                int index = typeDropdown.GetComponent<Dropdown>().value;
                menuOptions = typeDropdown.GetComponent<Dropdown>().options;
                typeSelected = menuOptions[index].text;
                                
                foreach (EquipableItemClass item in equipableItems)
                {
                    string itemType = item.GetItemType();

                    string bought = item.GetBoughtOrDrop();

                    if (bought == "Bought" || bought == "Both")
                    {
                        switch (itemType)
                        {
                            case "Weapon":
                                weapons.Add(item);
                                break;
                            case "Armor":
                                armors.Add(item);
                                break;
                            case "Accessory":
                                accessories.Add(item);
                                break;
                        }
                    }                        
                }

                foreach (Transform child in scrollContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                switch (typeSelected)
                {
                    case "Weapon":
                        foreach (EquipableItemClass weaponItem in weapons)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(weaponItem);
                        }
                        break;
                    case "Armor":
                        foreach (EquipableItemClass armorItem in armors)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(armorItem);
                        }
                        break;
                    case "Accessory":
                        foreach (EquipableItemClass accessoryItem in accessories)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(accessoryItem);
                        }
                        break;
                }                
                break;
        }
    }      
}
