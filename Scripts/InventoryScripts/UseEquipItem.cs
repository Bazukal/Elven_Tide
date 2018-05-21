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

    EquipmentClass equipSelected;
    ItemClass useSelected;
    private int charSelected;

    PlayerClass usingChar = null;

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

    private Dictionary<string, PlayerClass> chars = new Dictionary<string, PlayerClass>();

    private void Start()
    {
        equipItem = this;

        chars.Add("char1", Manager.manager.GetPlayer("Player1"));
        chars.Add("char2", Manager.manager.GetPlayer("Player2"));
        chars.Add("char3", Manager.manager.GetPlayer("Player3"));
        chars.Add("char4", Manager.manager.GetPlayer("Player4"));
    }

    //open the item stat window
    public void activateEquipItemStat(EquipmentClass item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        equipSelected = item;
        charSelected = CharacterInventory.charInven.getCharSelected();
        isEquipable = true;
        EquipItemScreen();
    }

    //open the item stat window
    public void activateUseItemStat(ItemClass item)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        useSelected = item;
        charSelected = CharacterInventory.charInven.getCharSelected();
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
        string name = useSelected.GetName();
        int healAmount = useSelected.GetHeal();
        string itemType = useSelected.GetItemType();
        string cure = useSelected.GetCure();
        int quantity = useSelected.GetQuantity();

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
        healWindow.GetComponent<CanvasGroup>().alpha = 1;
        healWindow.GetComponent<CanvasGroup>().interactable = true;
        healWindow.GetComponent<CanvasGroup>().blocksRaycasts = true;

        character1.GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars["char1"].GetClass());
        character2.GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars["char2"].GetClass());
        character3.GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars["char3"].GetClass());
        character4.GetComponent<Image>().sprite = Manager.manager.getCharSprite(chars["char4"].GetClass());

        char1Name.text = chars["char1"].GetName();
        char2Name.text = chars["char2"].GetName();
        char3Name.text = chars["char3"].GetName();
        char4Name.text = chars["char4"].GetName();

        healSlider();

        if (useSelected.GetItemType().Equals("Revive"))
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
        char1HP.value = (float)chars["char1"].GetCurrentHP() / (float)chars["char1"].GetMaxHP();
        char1MP.value = (float)chars["char1"].GetCurrentMP() / (float)chars["char1"].GetMaxMP();

        char2HP.value = (float)chars["char2"].GetCurrentHP() / (float)chars["char2"].GetMaxHP();
        char2MP.value = (float)chars["char2"].GetCurrentMP() / (float)chars["char2"].GetMaxMP();

        char3HP.value = (float)chars["char3"].GetCurrentHP() / (float)chars["char3"].GetMaxHP();
        char3MP.value = (float)chars["char3"].GetCurrentMP() / (float)chars["char3"].GetMaxMP();

        char4HP.value = (float)chars["char4"].GetCurrentHP() / (float)chars["char4"].GetMaxHP();
        char4MP.value = (float)chars["char4"].GetCurrentMP() / (float)chars["char4"].GetMaxMP();
    }

    //activates or deactivates character buttons if the character is alive or not
    public void healButtonInteract()
    {
        if (chars["char1"].GetCurrentHP() == 0 || chars["char1"].GetCurrentHP() == chars["char1"].GetMaxHP())
            character1.interactable = false;
        else
            character1.interactable = true;

        if (chars["char2"].GetCurrentHP() == 0 || chars["char2"].GetCurrentHP() == chars["char2"].GetMaxHP())
            character2.interactable = false;
        else
            character2.interactable = true;

        if (chars["char3"].GetCurrentHP() == 0 || chars["char3"].GetCurrentHP() == chars["char3"].GetMaxHP())
            character3.interactable = false;
        else
            character3.interactable = true;

        if (chars["char4"].GetCurrentHP() == 0 || chars["char4"].GetCurrentHP() == chars["char4"].GetMaxHP())
            character4.interactable = false;
        else
            character4.interactable = true;
    }

    public void reviveButtonInteract()
    {
        if (chars["char1"].GetCurrentHP() == 0)
            character1.interactable = true;
        else
            character1.interactable = false;

        if (chars["char2"].GetCurrentHP() == 0)
            character2.interactable = true;
        else
            character2.interactable = false;

        if (chars["char3"].GetCurrentHP() == 0)
            character3.interactable = true;
        else
            character3.interactable = false;

        if (chars["char4"].GetCurrentHP() == 0)
            character4.interactable = true;
        else
            character4.interactable = false;
    }

    //opens screen to equip item
    public void EquipItemScreen()
    {
        switch (charSelected)
        {
            case 1:
                usingChar = Manager.manager.GetPlayer("Player1");
                break;
            case 2:
                usingChar = Manager.manager.GetPlayer("Player2");
                break;
            case 3:
                usingChar = Manager.manager.GetPlayer("Player3");
                break;
            case 4:
                usingChar = Manager.manager.GetPlayer("Player4");
                break;
        }

        string name = equipSelected.GetName();
        int damage = equipSelected.GetDamage();
        int armor = equipSelected.GetArmor();
        int str = equipSelected.GetStr();
        int agi = equipSelected.GetAgi();
        int mind = equipSelected.GetMind();
        int soul = equipSelected.GetSoul();
        string type = equipSelected.GetItemType();

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
                string weaponType = equipSelected.GetEquipType();
                string charClass = usingChar.GetClass();

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
                string armorType = equipSelected.GetEquipType();
                string maxArmor = usingChar.GetMaxArmor();
                bool canShield = usingChar.CanShield();

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

    //heals characters hp and uses item
    public void healChar(string character)
    {
        chars[character].changeHP(useSelected.GetHeal());
        useSelected.ChangeQuantity(-1);
        healSlider();

        int quantity = useSelected.GetQuantity();

        if(quantity <= 0)
        {
            CharacterInventory.charInven.removeUsableFromInventory(useSelected);
            closeHealPanel();
            closeItemStat();
        }

        CharacterInventory.charInven.afterUseRefresh();
    }

    //equips item selected
    public void EquipItem()
    {
        EquipmentClass equippedItem = null;
        string itemType = equipSelected.GetItemType();

        switch (itemType)
        {
            case "Weapon":
                string weaponType = equipSelected.GetEquipType();

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
                if (equipSelected.GetEquipType() == "Shield")
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
        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquipableFromInventory(equipSelected);
        CharacterInventory.charInven.afterUseRefresh();
    }

    //equip item into main slot
    public void equipMain()
    {
        EquipmentClass equippedItem = usingChar.ChangeWeapon(equipSelected);

        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquipableFromInventory(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CharacterInventory.charInven.afterUseRefresh();

        closeItemStat();
    }

    //equip item into off hand slot
    public void equipOff()
    {
        EquipmentClass equippedItem = usingChar.ChangeOffHand(equipSelected);

        CharacterInventory.charInven.addEquipableToInventory(equippedItem);
        CharacterInventory.charInven.removeEquipableFromInventory(equipSelected);

        weaponEquip.GetComponent<CanvasGroup>().alpha = 0;
        weaponEquip.GetComponent<CanvasGroup>().interactable = false;
        weaponEquip.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CharacterInventory.charInven.afterUseRefresh();

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
