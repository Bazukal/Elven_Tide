﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory charInven;

    public Text charName;
    public Text goldText;

    CharacterClass character1;
    CharacterClass character2;
    CharacterClass character3;
    CharacterClass character4;

    public Button char1;
    public Button char2;
    public Button char3;
    public Button char4;

    EquipableItemClass charWeap;
    EquipableItemClass charOffHand;
    EquipableItemClass charArmor;
    EquipableItemClass charAccess;

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

    private List<UsableItemClass> heldUsableInventory = new List<UsableItemClass>();
    private List<EquipableItemClass> heldEquipableInventory = new List<EquipableItemClass>();

    private int charSelected;

    private void Awake()
    {
        charInven = this;

        heldUsableInventory = CharacterManager.charManager.getUsableInventory();
        heldEquipableInventory = CharacterManager.charManager.getEquipableInventory();
    }

    //opens or closes the inventory window
    public void displayInvScreen()
    {
        StoreFinds.stored.activate();

        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            foreach (Transform child in scrollContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

            changeChar(1);
            populateInvList();
            goldText.text = "Gold: " + FindObjectOfType<CharacterManager>().getGold().ToString();
        }
    }

    //changes which character is selected from button press
    public void changeChar(int charNum)
    {
        character1 = CharacterManager.charManager.character1;
        character2 = CharacterManager.charManager.character2;
        character3 = CharacterManager.charManager.character3;
        character4 = CharacterManager.charManager.character4;

        char1.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character1.GetCharClass());
        char2.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character2.GetCharClass());
        char3.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character3.GetCharClass());
        char4.GetComponent<Image>().sprite = CharacterManager.charManager.getCharSprite(character4.GetCharClass());

        switch (charNum)
        {
            case 1:
                //fill in screen data with char info
                charName.text = character1.GetCharName();

                charWeap = character1.GetWeapon();
                charOffHand = character1.GetOffHand();
                charArmor = character1.GetArmor();
                charAccess = character1.GetAccessory();

                //make char1 button selected
                charSelected = 1;
                char1.Select();

                break;
            case 2:
                charName.text = character2.GetCharName();

                charWeap = character2.GetWeapon();
                charOffHand = character2.GetOffHand();
                charArmor = character2.GetArmor();
                charAccess = character2.GetAccessory();

                charSelected = 2;
                char2.Select();

                break;
            case 3:
                charName.text = character3.GetCharName();

                charWeap = character3.GetWeapon();
                charOffHand = character3.GetOffHand();
                charArmor = character3.GetArmor();
                charAccess = character3.GetAccessory();

                charSelected = 3;
                char3.Select();

                break;
            case 4:
                charName.text = character4.GetCharName();

                charWeap = character4.GetWeapon();
                charOffHand = character4.GetOffHand();
                charArmor = character4.GetArmor();
                charAccess = character4.GetAccessory();

                charSelected = 4;
                char4.Select();

                break;
        }

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
        foreach(UsableItemClass item in heldUsableInventory)
        {
            GameObject invenItem = (GameObject)Instantiate(inventoryPanel) as GameObject;
            invenItem.transform.SetParent(scrollContent.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateUsableData(item);
        }

        foreach (EquipableItemClass item in heldEquipableInventory)
        {
            GameObject invenItem = (GameObject)Instantiate(inventoryPanel) as GameObject;
            invenItem.transform.SetParent(scrollContent.transform, false);

            PopulateItemPanel popItem = invenItem.GetComponent<PopulateItemPanel>();

            popItem.populateEquipableData(item);
        }
    }

    //add usable item to the inventory
    public void addUsableToInventory(UsableItemClass itemAdded)
    {
        try
        {            
            int index = heldUsableInventory.IndexOf(itemAdded);
            int quantity = itemAdded.GetQuantity();

            heldUsableInventory[index].ChangeQuantity(quantity);
        }
        catch
        {
            heldUsableInventory.Add(itemAdded);
        }
    }

    //add equipable item to the inventory
    public void addEquipableToInventory(EquipableItemClass itemAdded)
    {
        try
        {
            int index = heldEquipableInventory.IndexOf(itemAdded);
            int quantity = itemAdded.GetQuantity();

            heldEquipableInventory[index].ChangeQuantity(quantity);
        }
        catch
        {
            heldEquipableInventory.Add(itemAdded);
        }
    }


    //removes usable item from inventory
    public void removeUsableFromInventory(UsableItemClass item)
    {
        int index = heldUsableInventory.IndexOf(item);

        int quantityAmount = heldUsableInventory[index].GetQuantity();

        if(quantityAmount <= 0)
        {
            heldUsableInventory.RemoveAt(index);
            
            foreach (Transform child in scrollContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            populateInvList();
        }
    }

    //removes equipable item from inventory
    public void removeEquipableFromInventory(EquipableItemClass item)
    {
        int index = heldEquipableInventory.IndexOf(item);
        int quantityAmount = heldEquipableInventory[index].GetQuantity();

        if (quantityAmount <= 0)
        {
            heldEquipableInventory.RemoveAt(index);

            foreach (Transform child in scrollContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            populateInvList();
        }
    }


    //removed the item from the inventory when it is being equipped
    public void removeEquippedFromInven(EquipableItemClass item)
    {
        int index = heldEquipableInventory.IndexOf(item);

        heldEquipableInventory.RemoveAt(index);

        foreach (Transform child in scrollContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        populateInvList();
    }

    //refreshes inventory after item is equipped
    public void afterUseRefresh()
    {
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }

        populateInvList();

        changeChar(charSelected);
    }

    //gets which character was selected
    public int getCharSelected() { return charSelected; }
    public List<UsableItemClass> getUsableInventory() { return heldUsableInventory; }
    public List<EquipableItemClass> getEquipableInventory() { return heldEquipableInventory; }
}