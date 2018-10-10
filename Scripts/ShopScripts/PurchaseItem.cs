using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PurchaseItem : MonoBehaviour {

    public static PurchaseItem pItem;

    public Text nameText;
    public Text stats;
    public Text quantityText;
    public Text costText;

    private int quantity = 0;
    private int itemCost;
    private int totalCost;
    private int goldAvail;

    public Button buyButton;
    public Button quantityDownButton;
    public Button quantityUpButton;

    EquipableItem buyEquipItem;
    UsableItem buyUseItem;

    private void Awake()
    {
        pItem = this;
    }

    //opens equipment shop
    public void purchaseEquipPanel(EquipableItem item)
    {
        quantityDownButton.interactable = false;
        quantityUpButton.interactable = false;

        quantity = 1;
        buyEquipItem = item;
        itemCost = item.buyValue;
        goldAvail = Manager.manager.GetGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.name;
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        StringBuilder strb = new StringBuilder();
        if (item.damage[0] > 0)
            strb.Append("Attack: " + item.damage[0] + "\n");
        if (item.armor[0] > 0)
            strb.Append("Armor: " + item.armor[0] + "\n");
        if (item.strength[0] > 0)
            strb.Append("Strength: " + item.strength[0] + "\n");
        if (item.agility[0] > 0)
            strb.Append("Agility: " + item.agility[0] + "\n");
        if (item.mind[0] > 0)
            strb.Append("Mind: " + item.mind[0] + "\n");
        if (item.soul[0] > 0)
            strb.Append("Soul: " + item.soul[0] + "\n");
        strb.Append("\nCost: " + item.buyValue);

        stats.text = strb.ToString();
    }

    //opens usable shop
    public void purchaseUseItem(UsableItem item)
    {
        quantityDownButton.interactable = true;

        quantity = 1;
        buyUseItem = item;
        itemCost = item.buyValue;
        goldAvail = Manager.manager.GetGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.name;
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        if(totalCost > goldAvail)
        {
            quantityUpButton.interactable = false;
            buyButton.interactable = false;
        }
        else if (goldAvail < totalCost + itemCost)
        {
            quantityUpButton.interactable = false;
            buyButton.interactable = true;
        }            
        else
        {
            quantityUpButton.interactable = true;
            buyButton.interactable = true;
        }

        string itemType = item.type;
        int buyPrice = item.buyValue;

        if (itemType.Equals("Heal"))
        {
            int healAmount = item.healAmount;
            stats.text = string.Format("Heals Ally for {0} Health\n\nCost: {1:n0}", healAmount, buyPrice);            
        }
        else if (itemType.Equals("Revive"))
        {
            float revivePerc = item.reviveAmount;
            stats.text = string.Format("Revives Ally, and heals for {0}% of Max Health\n\nCost: {1:n0}", revivePerc, buyPrice);
        }
        else
        {
            string cure = item.cureAilment;
            stats.text = string.Format("Cures Ally of {0}\n\nCost: {1:n0}", cure, buyPrice);
        }
    }

    //increases the amount of items player is going to buy
    public void quantityUp()
    {
        quantity++;
        quantityText.text = quantity.ToString();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        if (quantityDownButton.interactable == false)
            quantityDownButton.interactable = true;

        if (goldAvail < totalCost + itemCost)
            quantityUpButton.interactable = false;

        if (buyButton.interactable == false)
            buyButton.interactable = true;
    }

    //reduces amount of items player is going to buy
    public void quantityDown()
    {
        quantity--;
        quantityText.text = quantity.ToString();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        if(quantity == 0)
        {
            quantityDownButton.interactable = false;
            buyButton.interactable = false;
        }
            

        if (quantityUpButton.interactable == false)
            quantityUpButton.interactable = true;
    }

    //purchases selected item with the quantity set from arrows
    public void purchaseItem()
    {
        GameObject npc = Manager.manager.getObject();
        string who = npc.tag;

        if(who.Equals("Blacksmith"))
        {
            Manager.manager.addEquipableToInventory(buyEquipItem);
            Manager.manager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }
        else
        {
            Debug.Log("Buying Quantity: " + quantity);
            Manager.manager.addUsableToInventory(buyUseItem.name, quantity);
            Manager.manager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }

        CloseBuyPanel.closeBuyPanel.updateGold();
        closePanel();
    }

    //closes shop
    public void closePanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
