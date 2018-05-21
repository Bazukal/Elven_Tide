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

    EquipmentClass buyEquipItem;
    ItemClass buyUseItem;

    private void Awake()
    {
        pItem = this;
    }

    //opens equipment shop
    public void purchaseEquipPanel(EquipmentClass item)
    {
        quantity = 1;
        buyEquipItem = item;
        itemCost = item.GetBuyPrice();
        goldAvail = Manager.manager.GetGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.GetName();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        StringBuilder strb = new StringBuilder();
        if (item.GetDamage() > 0)
            strb.Append("Attack: " + item.GetDamage() + "\n");
        if (item.GetArmor() > 0)
            strb.Append("Armor: " + item.GetArmor() + "\n");
        if (item.GetStr() > 0)
            strb.Append("Strength: " + item.GetStr() + "\n");
        if (item.GetAgi() > 0)
            strb.Append("Agility: " + item.GetAgi() + "\n");
        if (item.GetMind() > 0)
            strb.Append("Mind: " + item.GetMind() + "\n");
        if (item.GetSoul() > 0)
            strb.Append("Soul: " + item.GetSoul() + "\n");
        strb.Append("\nCost: " + item.GetBuyPrice());

        stats.text = strb.ToString();
    }

    //opens usable shop
    public void purchaseUseItem(ItemClass item)
    {
        quantity = 1;
        buyUseItem = item;
        itemCost = item.GetBuy();
        goldAvail = Manager.manager.GetGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.GetName();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        string itemType = item.GetItemType();
        int buyPrice = item.GetBuy();

        if (itemType.Equals("Heal"))
        {
            int healAmount = item.GetHeal();
            stats.text = string.Format("Heals Ally for {0} Health\n\nCost: {1:n0}", healAmount, buyPrice);            
        }
        else if (itemType.Equals("Revive"))
        {
            float revivePerc = item.GetRevive();
            stats.text = string.Format("Revives Ally, and heals for {0}% of Max Health\n\nCost: {1:n0}", revivePerc, buyPrice);
        }
        else
        {
            string cure = item.GetCure();
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
        string npc = Manager.manager.getInRange();

        if(npc.Equals("Blacksmith"))
        {
            CharacterInventory.charInven.addEquipableToInventory(buyEquipItem);
            Manager.manager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }
        else
        {            
            buyUseItem.ChangeQuantity(quantity);
            CharacterInventory.charInven.addUsableToInventory(buyUseItem);
            Manager.manager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }        

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
