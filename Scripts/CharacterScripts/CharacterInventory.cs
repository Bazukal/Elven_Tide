using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory charInven;

    public Text charName;
    public Text goldText;

    private Dictionary<int, PlayerClass> characters = new Dictionary<int, PlayerClass>();

    public Button char1;
    public Button char2;
    public Button char3;
    public Button char4;

    private Dictionary<int, Button> charButtons = new Dictionary<int, Button>();

    EquipmentClass charWeap;
    EquipmentClass charOffHand;
    EquipmentClass charArmor;
    EquipmentClass charAccess;

    public Text weaponName;
    public Text weaponStats;

    public Text offHandName;
    public Text offHandStats;

    public Text armorName;
    public Text armorStats;

    public Text accName;
    public Text accStats;

    public GameObject inventoryPanel;
    public GameObject scrollContent;

    private List<ItemClass> heldUsableInventory = new List<ItemClass>();
    private List<EquipmentClass> heldEquipableInventory = new List<EquipmentClass>();

    private int charSelected;

    private void Awake()
    {
        charInven = this;

        heldUsableInventory = Manager.manager.getUsableInventory();
        heldEquipableInventory = Manager.manager.getEquipableInventory();

        characters.Add(1, Manager.manager.GetPlayer("Player1"));
        characters.Add(2, Manager.manager.GetPlayer("Player2"));
        characters.Add(3, Manager.manager.GetPlayer("Player3"));
        characters.Add(4, Manager.manager.GetPlayer("Player4"));

        charButtons.Add(1, char1);
        charButtons.Add(2, char2);
        charButtons.Add(3, char3);
        charButtons.Add(4, char4);

        char1.GetComponent<Image>().sprite = Manager.manager.getCharSprite(characters[1].GetClass());
        char2.GetComponent<Image>().sprite = Manager.manager.getCharSprite(characters[2].GetClass());
        char3.GetComponent<Image>().sprite = Manager.manager.getCharSprite(characters[3].GetClass());
        char4.GetComponent<Image>().sprite = Manager.manager.getCharSprite(characters[4].GetClass());
    }

    //opens the inventory window
    public void displayInvScreen()
    {
        goldText.text = "Gold: " + Manager.manager.GetGold().ToString();
        clearInventory();
        populateInvList();

        StoreFinds.stored.BattleActivate();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        changeChar(1);
    }

    //clears out the inventory list
    private void clearInventory()
    {
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //closes the inventory window
    public void closeInventory()
    {
        StoreFinds.stored.BattleDeactivate();

        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //changes which character is selected from button press
    public void changeChar(int charNum)
    {        
        //fill in screen data with char info
        charName.text = characters[charNum].GetName();

        charWeap = characters[charNum].GetEquipment("Weapon");
        charOffHand = characters[charNum].GetEquipment("Offhand");
        charArmor = characters[charNum].GetEquipment("Armor");
        charAccess = characters[charNum].GetEquipment("Accessory");

        //make char button selected
        charSelected = charNum;
        charButtons[charNum].Select();        

        displayWeap();
        displayOff();
        displayArmor();
        displayAccess();
    }

    //populates character weapon data
    public void displayWeap()
    {
        try
        {
            weaponName.text = charWeap.GetName();

            StringBuilder stats = new StringBuilder();

            int damage = charWeap.GetDamage();
            int str = charWeap.GetStr();
            int agi = charWeap.GetAgi();
            int mind = charWeap.GetMind();
            int soul = charWeap.GetSoul();

            stats.Append("Attack: " + damage + ",");

            if (str > 0)
                stats.Append(" Strength: " + str + ",");
            if (agi > 0)
                stats.Append(" Agility: " + agi + ",");
            if (mind > 0)
                stats.Append(" Mind: " + mind + ",");
            if (soul > 0)
                stats.Append(" Soul: " + soul + ",");

            int count = stats.Length;

            stats.Remove(count - 1, 1);

            weaponStats.text = stats.ToString();
        }
        catch
        {
            weaponName.text = "Empty";
            weaponStats.text = "";
        }        
    }

    //populates character offhand data
    public void displayOff()
    {
        try
        {
            offHandName.text = charOffHand.GetName();

            StringBuilder stats = new StringBuilder();

            int damage = charOffHand.GetDamage();
            int armor = charOffHand.GetArmor();
            int str = charOffHand.GetStr();
            int agi = charOffHand.GetAgi();
            int mind = charOffHand.GetMind();
            int soul = charOffHand.GetSoul();

            if (damage > 0)
                stats.Append("Attack: " + damage + ",");
            else
                stats.Append("Armor: " + armor + ",");

            if (str > 0)
                stats.Append(" Strength: " + str + ",");
            if (agi > 0)
                stats.Append(" Agility: " + agi + ",");
            if (mind > 0)
                stats.Append(" Mind: " + mind + ",");
            if (soul > 0)
                stats.Append(" Soul: " + soul + ",");

            int count = stats.Length;

            stats.Remove(count - 1, 1);

            offHandStats.text = stats.ToString();
        }
        catch
        {
            offHandName.text = "Empty";
            offHandStats.text = "";
        }        
    }

    //populates character armor data
    public void displayArmor()
    {
        armorName.text = charArmor.GetName();

        StringBuilder stats = new StringBuilder();
        
        int armor = charArmor.GetArmor();
        int str = charArmor.GetStr();
        int agi = charArmor.GetAgi();
        int mind = charArmor.GetMind();
        int soul = charArmor.GetSoul();
        
        stats.Append("Armor: " + armor + ",");

        if (str > 0)
            stats.Append(" Strength: " + str + ",");
        if (agi > 0)
            stats.Append(" Agility: " + agi + ",");
        if (mind > 0)
            stats.Append(" Mind: " + mind + ",");
        if (soul > 0)
            stats.Append(" Soul: " + soul + ",");

        int count = stats.Length;

        stats.Remove(count - 1, 1);

        armorStats.text = stats.ToString();
    }

    //populates character accessory data
    public void displayAccess()
    {     
        accName.text = charAccess.GetName();

        StringBuilder stats = new StringBuilder();

        int damage = charAccess.GetDamage();
        int armor = charAccess.GetArmor();
        int str = charAccess.GetStr();
        int agi = charAccess.GetAgi();
        int mind = charAccess.GetMind();
        int soul = charAccess.GetSoul();

        if (damage > 0)
            stats.Append("Attack: " + damage + ",");
        if(armor > 0 && damage > 0)
            stats.Append(" Armor: " + armor + ",");
        else if(armor > 0)
            stats.Append("Armor: " + armor + ",");

        if (str > 0)
            stats.Append(" Strength: " + str + ",");
        if (agi > 0)
            stats.Append(" Agility: " + agi + ",");
        if (mind > 0)
            stats.Append(" Mind: " + mind + ",");
        if (soul > 0)
            stats.Append(" Soul: " + soul + ",");

        int count = stats.Length;

        stats.Remove(count - 1, 1);

        accStats.text = stats.ToString();
    }

    //adds items in inventory list, and populates scroll view with these items
    private void populateInvList()
    {
        foreach (ItemClass item in heldUsableInventory)
        {
            GameObject invenItem = (GameObject)Instantiate(inventoryPanel) as GameObject;
            invenItem.transform.SetParent(scrollContent.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateUsableData(item);
        }

        foreach (EquipmentClass item in heldEquipableInventory)
        {
            GameObject invenItem = (GameObject)Instantiate(inventoryPanel) as GameObject;
            invenItem.transform.SetParent(scrollContent.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateEquipableData(item);
        }
    }

    //add usable item to the inventory
    public void addUsableToInventory(ItemClass itemAdded)
    {
        string addName = itemAdded.GetName();
        int addQuant = itemAdded.GetQuantity();

        foreach(ItemClass item in heldUsableInventory)
        {
            string currentName = item.GetName();

            if(currentName.Equals(addName))
            {
                item.ChangeQuantity(addQuant);
                return;
            }
        }

        heldUsableInventory.Add(itemAdded);        
    }

    //add equipable item to the inventory
    public void addEquipableToInventory(EquipmentClass itemAdded)
    {        
        heldEquipableInventory.Add(itemAdded);
    }


    //removes usable item from inventory
    public void removeUsableFromInventory(ItemClass item)
    {
        int index = heldUsableInventory.IndexOf(item);

        int quantityAmount = heldUsableInventory[index].GetQuantity();

        if(quantityAmount <= 0)
        {
            heldUsableInventory.RemoveAt(index);

            clearInventory();
            populateInvList();
        }
    }

    //removes equipable item from inventory
    public void removeEquipableFromInventory(EquipmentClass item)
    {
        int index = heldEquipableInventory.IndexOf(item);
        heldEquipableInventory.RemoveAt(index);

        clearInventory();
        populateInvList();
    }

    //refreshes inventory after item is equipped
    public void afterUseRefresh()
    {
        clearInventory();

        populateInvList();

        changeChar(charSelected);
    }

    //gets which character was selected
    public int getCharSelected() { return charSelected; }
    public List<ItemClass> getUsableInventory() { return heldUsableInventory; }
    public List<EquipmentClass> getEquipableInventory() { return heldEquipableInventory; }
}