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

    public void purchaseEquipPanel(EquipableItemClass item)
    {
        quantity = 1;
        buyEquipItem = item;
        itemCost = item.getBuyPrice();
        goldAvail = CharacterManager.charManager.getGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.getName();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        StringBuilder strb = new StringBuilder();
        if (item.getDamage() > 0)
            strb.Append("Attack: " + item.getDamage() + "\n");
        if (item.getArmor() > 0)
            strb.Append("Armor: " + item.getArmor() + "\n");
        if (item.getStr() > 0)
            strb.Append("Strength: " + item.getStr() + "\n");
        if (item.getAgi() > 0)
            strb.Append("Agility: " + item.getAgi() + "\n");
        if (item.getMind() > 0)
            strb.Append("Mind: " + item.getMind() + "\n");
        if (item.getSoul() > 0)
            strb.Append("Soul: " + item.getSoul() + "\n");
        strb.Append("\nCost: " + item.getBuyPrice());

        stats.text = strb.ToString();
    }

    public void purchaseUseItem(UsableItemClass item)
    {
        quantity = 1;
        buyUseItem = item;
        itemCost = item.getBuyPrice();
        goldAvail = CharacterManager.charManager.getGold();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        quantityText.text = quantity.ToString();
        nameText.text = item.getName();
        totalCost = itemCost * quantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        int healAmount = item.getHeal();
        string cure = item.getCure();
        bool revive = item.getRevive();

        if (healAmount > 0)
        {
            if (revive == false)
            {
                stats.text = string.Format("Heals Ally for {0} Health\n\nCost: {1:n0}", healAmount, item.getBuyPrice());
            }
            else
            {
                stats.text = string.Format("Revives Ally, and heals for {0} Health\n\nCost: {1:n0}", healAmount, item.getBuyPrice());
            }
        }
        else
        {
            stats.text = string.Format("Cures Ally of {0}\n\nCost: {1:n0}", cure, item.getBuyPrice());
        }
    }

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

    public void purchaseItem()
    {
        string npc = CharacterManager.charManager.getInRange();

        if(npc.Equals("Blacksmith"))
        {
            buyEquipItem.buyQuantity(quantity);
            CharacterInventory.charInven.addEquipableToInventory(buyEquipItem);
            CharacterManager.charManager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }
        else
        {            
            buyUseItem.buyQuantity(quantity);
            CharacterInventory.charInven.addUsableToInventory(buyUseItem);
            CharacterManager.charManager.changeGold(-totalCost);
            CloseBuyPanel.closeBuyPanel.updateGold();
        }        

        closePanel();
    }

    public void closePanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
