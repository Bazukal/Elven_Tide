using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UseEquipItem : MonoBehaviour {

    public static UseEquipItem equipItem;

    public Text nameText;
    public Text statsText;

    public Button useButton;

    EquipableItemClass equipSelected;
    UsableItemClass useSelected;
    private int charSelected;

    CharacterClass usingChar = null;

    public GameObject weaponEquip;

    private void Awake()
    {
        equipItem = this;
    }

    //open the item stat window
    public void activateEquipItemStat(EquipableItemClass item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        equipSelected = item;
        charSelected = CharacterInventory.charInven.getCharSelected();
        EquipItemScreen();
    }

    //open the item stat window
    public void activateUseItemStat(UsableItemClass item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        useSelected = item;
        charSelected = CharacterInventory.charInven.getCharSelected();
        UseItem();
    }

    public void closeItemStat()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //populate item data into stat window
	private void UseItem()
    {
        string name = useSelected.getName();
        int healAmount = useSelected.getHeal();
        string cure = useSelected.getCure();
        bool revive = useSelected.getRevive();
        int quantity = useSelected.getQuantity();

        switch (charSelected)
        {
            case 1:
                usingChar = CharacterManager.charManager.character1;
                break;
            case 2:
                usingChar = CharacterManager.charManager.character2;
                break;
            case 3:
                usingChar = CharacterManager.charManager.character3;
                break;
            case 4:
                usingChar = CharacterManager.charManager.character4;
                break;
        }

        int currentHP = usingChar.GetCharCurrentHp();
        int maxHP = usingChar.GetCharMaxHp();

        if (healAmount > 0)
        {
            if (revive == false)
            {
                if (quantity > 1)
                {
                    nameText.text = string.Format("{0} x {1}", name, quantity);
                    statsText.text = string.Format("Heals Ally for {0} Health", healAmount);
                }
                else
                {
                    nameText.text = string.Format("{0}", name);
                    statsText.text = string.Format("Heals Ally for {0} Health", healAmount);
                }

                if (currentHP == maxHP)
                {
                    useButton.interactable = false;
                }
                else
                {
                    useButton.interactable = true;
                }

            }
            else
            {
                if (quantity > 1)
                {
                    nameText.text = string.Format("{0} x {1}", name, quantity);
                    statsText.text = string.Format("Revives Ally, and heals for {0} Health", healAmount);
                }
                else
                {
                    nameText.text = string.Format("{0}", name);
                    statsText.text = string.Format("Revives Ally, and heals for {0} Health", healAmount);
                }

                if (currentHP == 0)
                {
                    useButton.interactable = true;
                }
                else
                {
                    useButton.interactable = false;
                }
            }
        }
        else
        {
            if (quantity > 1)
            {
                nameText.text = string.Format("{0} x {1}", name, quantity);
                statsText.text = string.Format("Cures Ally of {0}", cure);
            }
            else
            {
                nameText.text = string.Format("{0}", name);
                statsText.text = string.Format("Cures Ally of {0}", cure);
            }
        }

        useButton.GetComponentInChildren<Text>().text = "Use";
    }

    public void EquipItemScreen()
    {
        switch (charSelected)
        {
            case 1:
                usingChar = CharacterManager.charManager.character1;
                break;
            case 2:
                usingChar = CharacterManager.charManager.character2;
                break;
            case 3:
                usingChar = CharacterManager.charManager.character3;
                break;
            case 4:
                usingChar = CharacterManager.charManager.character4;
                break;
        }

        string name = equipSelected.getName();
        int damage = equipSelected.getDamage();
        int armor = equipSelected.getArmor();
        int str = equipSelected.getStr();
        int agi = equipSelected.getAgi();
        int mind = equipSelected.getMind();
        int soul = equipSelected.getSoul();
        string type = equipSelected.getType();

        StringBuilder stats = new StringBuilder();

        if (damage > 0)
            stats.Append("Attack: " + damage + ",");
        if (armor > 0 && damage > 0)
            stats.Append(" Armor: " + armor + ",");
        else if (armor > 0)
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

        nameText.text = name;
        statsText.text = stats.ToString();

        switch (type)
        {

            case "Weapon":
                string weaponType = equipSelected.getWeaponType();
                string charClass = usingChar.GetCharClass();

                switch (weaponType)
                {
                    case "Sword":
                        if (charClass == "Warrior")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Dagger":
                        if (charClass == "Thief")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Mace":
                        if (charClass == "Paladin")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Bow":
                        if (charClass == "Archer")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Staff":
                        if (charClass == "White Mage")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Rod":
                        if (charClass == "Black Mage")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                }

                break;
            case "Armor":
                string armorType = equipSelected.getArmorType();
                string maxArmor = usingChar.GetMaxArmor();
                bool canShield = usingChar.GetShield();

                switch (armorType)
                {
                    case "Cloth":
                        useButton.interactable = true;
                        break;
                    case "Leather":
                        if (maxArmor == "Leather" || maxArmor == "Plate")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Plate":
                        if (maxArmor == "Plate")
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Shield":
                        if (canShield == true)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                }

                break;
            case "Accessory":
                useButton.interactable = true;
                break;

        }
        useButton.GetComponentInChildren<Text>().text = "Equip";
    }

    public void EquipItem()
    {
        EquipableItemClass equippedItem = null;
        string itemType = equipSelected.getType();

        switch (itemType)
        {
            case "Weapon":
                string weaponType = equipSelected.getWeaponType();

                if (weaponType == "Sword" || weaponType == "Dagger")
                {
                    weaponEquip.GetComponent<CanvasGroup>().alpha = 1;
                    weaponEquip.GetComponent<CanvasGroup>().interactable = true;
                    weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
                else
                {
                    equippedItem = usingChar.ChangeWeapon(equipSelected);
                    closeItemStat();
                }

                break;
            case "Armor":
                if (equipSelected.getArmorType() == "Shield")
                    equippedItem = usingChar.ChangeOffHand(equipSelected);
                else
                    equippedItem = usingChar.ChangeArmor(equipSelected);

                closeItemStat();
                break;
            case "Accessory":
                equippedItem = usingChar.ChangeAccessory(equipSelected);
                Debug.Log(usingChar.GetAccessory().getName() + " " + usingChar.GetAccessory().getQuantity());
                closeItemStat();
                break;
        }
        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquippedFromInven(equipSelected);
        CharacterInventory.charInven.afterEquipRefresh();
    }

    public void equipMain()
    {
        EquipableItemClass equippedItem = usingChar.ChangeWeapon(equipSelected);

        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquippedFromInven(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CharacterInventory.charInven.afterEquipRefresh();

        closeItemStat();
    }

    public void equipOff()
    {
        EquipableItemClass equippedItem = usingChar.ChangeOffHand(equipSelected);

        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquippedFromInven(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CharacterInventory.charInven.afterEquipRefresh();

        closeItemStat();
    }

    public void cancelWeapEquip()
    {
        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        closeItemStat();
    }
}
