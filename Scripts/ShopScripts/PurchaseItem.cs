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

    EquipableItemClass buyEquipItem;
    UsableItemClass buyUseItem;

    private void Awake()
    {
        pItem = this;
    }

    //opens equipment shop
    public void purchaseEquipPanel(EquipableItemClass item)
    {
        quantity = 1;
        buyEquipItem = item;
        itemCost = item.GetBuyPrice();
        goldAvail = CharacterManager.charManager.getGold();

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
    public void purchaseUseItem(UsableItemClass item)
    {
        quantity = 1;
        buyUseItem = item;
        itemCost = item.GetBuyPrice();
        goldAvail = CharacterManager.charManager.getGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.GetName();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        int healAmount = item.GetHeal();
        string cure = item.GetCure();
        bool revive = item.GetRevive();

        if (healAmount > 0)
        {
            if (revive == false)
            {
                stats.text = string.Format("Heals Ally for {0} Health\n\nCost: {1:n0}", healAmount, item.GetBuyPrice());
            }
            else
            {
                stats.text = string.Format("Revives Ally, and heals for {0} Health\n\nCost: {1:n0}", healAmount, item.GetBuyPrice());
            }
        }
        else
        {
            stats.text = string.Format("Cures Ally of {0}\n\nCost: {1:n0}", cure, item.GetBuyPrice());
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
        string npc = CharacterManager.charManager.getInRange();

        if(npc.Equals("Blacksmith"))
        {
            buyEquipItem.SetQuantity(quantity);
            CharacterInventory.charInven.addEquipableToInventory(buyEquipItem);
            CharacterManager.charManager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }
        else
        {            
            buyUseItem.SetQuantity(quantity);
            CharacterInventory.charInven.addUsableToInventory(buyUseItem);
            CharacterManager.charManager.changeGold(-totalCost);
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
