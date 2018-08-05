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

    EquipableItem equipSelected;
    UsableItem useSelected;

    ScriptablePlayerClasses usingChar = null;

    public GameObject weaponEquip;
    public GameObject healWindow;

    public Button character1;
    public Button character2;
    public Button character3;
    public Button character4;

    public Slider char1HP;
    public Slider char1MP;
    public Slider char2HP;
    public Slider char2MP;
    public Slider char3HP;
    public Slider char3MP;
    public Slider char4HP;
    public Slider char4MP;

    public Text char1Name;
    public Text char2Name;
    public Text char3Name;
    public Text char4Name;

    private bool isEquipable;

    private Dictionary<string, ScriptablePlayerClasses> chars = new Dictionary<string, ScriptablePlayerClasses>();

    private void Start()
    {
        equipItem = this;

        chars.Add("char1", Manager.manager.GetPlayer("Player1"));
        chars.Add("char2", Manager.manager.GetPlayer("Player2"));
        chars.Add("char3", Manager.manager.GetPlayer("Player3"));
        chars.Add("char4", Manager.manager.GetPlayer("Player4"));
    }

    //open the item stat window
    public void activateEquipItemStat(EquipableItem item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        equipSelected = item;
        isEquipable = true;
        EquipItemScreen();
    }

    //open the item stat window
    public void activateUseItemStat(UsableItem item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        useSelected = item;
        isEquipable = false;
        UseItem();
    }

    //closes screen that shows item stats
    public void closeItemStat()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //populate item data into stat window
	private void UseItem()
    {
        string name = useSelected.name;
        int healAmount = useSelected.healAmount;
        string itemType = useSelected.type;
        string cure = useSelected.cureAilment;
        int quantity = useSelected.quantity;

        if (healAmount > 0)
        {
            if (itemType.Equals("Heal"))
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
        MiniStats.stats.updateSliders();
    }

    //determines if the item selected is equipment or usable item, and opens screen based on outcome
    public void equipOrUse()
    {
        if (isEquipable)
            EquipItem();
        else
        {
            openHealPanel();
        }
           
    }

    //opens screen for player to select which character will be healed from item
    public void openHealPanel()
    {
        closeItemStat();

        healWindow.GetComponent<CanvasGroup>().alpha = 1;
        healWindow.GetComponent<CanvasGroup>().interactable = true;
        healWindow.GetComponent<CanvasGroup>().blocksRaycasts = true;

        character1.GetComponent<Image>().sprite = chars["char1"].classHead;
        character2.GetComponent<Image>().sprite = chars["char2"].classHead;
        character3.GetComponent<Image>().sprite = chars["char3"].classHead;
        character4.GetComponent<Image>().sprite = chars["char4"].classHead;

        char1Name.text = chars["char1"].name;
        char2Name.text = chars["char2"].name;
        char3Name.text = chars["char3"].name;
        char4Name.text = chars["char4"].name;

        healSlider();

        if (useSelected.type.Equals("Revive"))
            reviveButtonInteract();
        else
            healButtonInteract();
    }

    //closes screen used to heal characters with items
    public void closeHealPanel()
    {
        healWindow.GetComponent<CanvasGroup>().alpha = 0;
        healWindow.GetComponent<CanvasGroup>().interactable = false;
        healWindow.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //sets sliders that shows characters hp and mp
    public void healSlider()
    {
        char1HP.value = (float)chars["char1"].currentHp / (float)chars["char1"].levelHp[chars["char1"].level];
        char1MP.value = (float)chars["char1"].currentMp / (float)chars["char1"].levelMp[chars["char1"].level];

        char2HP.value = (float)chars["char2"].currentHp / (float)chars["char2"].levelHp[chars["char2"].level];
        char2MP.value = (float)chars["char2"].currentMp / (float)chars["char2"].levelMp[chars["char2"].level];

        char3HP.value = (float)chars["char3"].currentHp / (float)chars["char3"].levelHp[chars["char3"].level];
        char3MP.value = (float)chars["char3"].currentMp / (float)chars["char3"].levelMp[chars["char3"].level];

        char4HP.value = (float)chars["char4"].currentHp / (float)chars["char4"].levelHp[chars["char4"].level];
        char4MP.value = (float)chars["char4"].currentMp / (float)chars["char4"].levelMp[chars["char4"].level];
    }

    //activates or deactivates character buttons if the character is alive or not
    public void healButtonInteract()
    {
        if (chars["char1"].currentHp == 0 || chars["char1"].currentHp == chars["char1"].levelHp[chars["char1"].level])
            character1.interactable = false;
        else
            character1.interactable = true;

        if (chars["char2"].currentHp == 0 || chars["char2"].currentHp == chars["char2"].levelHp[chars["char2"].level])
            character2.interactable = false;
        else
            character2.interactable = true;

        if (chars["char3"].currentHp == 0 || chars["char3"].currentHp == chars["char3"].levelHp[chars["char3"].level])
            character3.interactable = false;
        else
            character3.interactable = true;

        if (chars["char4"].currentHp == 0 || chars["char4"].currentHp == chars["char4"].levelHp[chars["char4"].level])
            character4.interactable = false;
        else
            character4.interactable = true;
    }

    public void reviveButtonInteract()
    {
        if (chars["char1"].currentHp == 0)
            character1.interactable = true;
        else
            character1.interactable = false;

        if (chars["char2"].currentHp == 0)
            character2.interactable = true;
        else
            character2.interactable = false;

        if (chars["char3"].currentHp == 0)
            character3.interactable = true;
        else
            character3.interactable = false;

        if (chars["char4"].currentHp == 0)
            character4.interactable = true;
        else
            character4.interactable = false;
    }

    //opens screen to equip item
    public void EquipItemScreen()
    {
        usingChar = StatsScreen.stats.GetPlayerObject();

        string name = equipSelected.name;
        int damage = equipSelected.currentDamage;
        int armor = equipSelected.currentArmor;
        int str = equipSelected.currentStrength;
        int agi = equipSelected.currentAgility;
        int mind = equipSelected.currentMind;
        int soul = equipSelected.currentSoul;
        string type = equipSelected.type;

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

        try
        {
            stats.Remove(count - 1, 1);
        }
        catch { }

        nameText.text = name;
        statsText.text = stats.ToString();

        switch (type)
        {

            case "Weapon":
                string weaponType = equipSelected.equipType;

                switch (weaponType)
                {
                    case "Sword":
                        bool canSword = usingChar.canSword;
                        if (canSword)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Dagger":
                        bool canDagger = usingChar.canDagger;
                        if (canDagger)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Fist":
                        bool canFist = usingChar.canFists;
                        if (canFist)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Mace":
                        bool canMace = usingChar.canMace;
                        if (canMace)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Bow":
                        bool canBow = usingChar.canBow;
                        if (canBow)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Staff":
                        bool canStaff = usingChar.canStaff;
                        if (canStaff)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                    case "Rod":
                        bool canRod = usingChar.canRod;
                        if (canRod)
                            useButton.interactable = true;
                        else
                            useButton.interactable = false;
                        break;
                }

                break;
            case "Armor":
                string armorType = equipSelected.equipType;
                string maxArmor = usingChar.maxArmor;
                bool canShield = usingChar.canShield;

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
                        if (canShield)
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

    //heals characters hp and uses item
    public void healChar(string character)
    {
        chars[character].changeHP(useSelected.healAmount);
        useSelected.useItem();
        healSlider();

        int quantity = useSelected.quantity;

        if(quantity <= 0)
        {
            Manager.manager.removeUsableFromInventory(useSelected);
            closeHealPanel();
            closeItemStat();
        }
        MiniStats.stats.updateSliders();
        Manager.manager.afterUseRefresh();
    }

    //equips item selected
    public void EquipItem()
    {
        EquipableItem equippedItem = null;
        string itemType = equipSelected.type;

        switch (itemType)
        {
            case "Weapon":
                string weaponType = equipSelected.equipType;

                if (weaponType == "Sword" || weaponType == "Dagger" || weaponType == "Fist" || weaponType == "Mace" && usingChar.canDuelWield == true)
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
                if (equipSelected.equipType == "Shield")
                    equippedItem = usingChar.ChangeOffHand(equipSelected);
                else
                    equippedItem = usingChar.ChangeArmor(equipSelected);

                closeItemStat();
                break;
            case "Accessory":
                equippedItem = usingChar.ChangeAccessory(equipSelected);
                closeItemStat();
                break;
        }
        Manager.manager.addEquipableToInventory(equippedItem);
        Manager.manager.removeEquipableFromInventory(equipSelected);
        Manager.manager.afterUseRefresh();
    }

    //equip item into main slot
    public void equipMain()
    {
        EquipableItem equippedItem = usingChar.ChangeWeapon(equipSelected);

        Manager.manager.addEquipableToInventory(equippedItem);
        Manager.manager.removeEquipableFromInventory(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        Manager.manager.afterUseRefresh();

        closeItemStat();
    }

    //equip item into off hand slot
    public void equipOff()
    {
        EquipableItem equippedItem = usingChar.ChangeOffHand(equipSelected);

        Manager.manager.addEquipableToInventory(equippedItem);
        Manager.manager.removeEquipableFromInventory(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        Manager.manager.afterUseRefresh();

        closeItemStat();
    }

    //cancel equipping weapon
    public void cancelWeapEquip()
    {
        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        closeItemStat();
    }
}
