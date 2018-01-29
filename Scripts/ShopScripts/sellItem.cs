﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sellItem : MonoBehaviour {

    public static sellItem sItem;

    public Text nameText;
    public Text quantityText;
    public Text costText;
    public Text sellEach;

    private int itemQuantity;
    private int sellQuantity;
    private int itemCost;
    private int totalCost;

    public Button sellButton;
    public Button quantityDownButton;
    public Button quantityUpButton;

    EquipableItemClass sellingEquipItem = null;
    UsableItemClass sellingUseItem = null;

    private void Awake()
    {
        sItem = this;
    }

    public void sellPanel(EquipableItemClass equipTtem, UsableItemClass useItem)
    {
        if (equipTtem != null)
        {
            sellingEquipItem = equipTtem;
            itemCost = sellingEquipItem.getSellPrice();
            costText.text = string.Format("Total Cost: {0:n0}", itemCost);
            itemQuantity = sellingEquipItem.getQuantity();
            sellQuantity = 1;
            totalCost = itemCost * sellQuantity;
            quantityText.text = sellQuantity.ToString();
            nameText.text = sellingEquipItem.getName();
            sellEach.text = sellingEquipItem.getSellPrice().ToString();
        }            
        else
        {
            sellingUseItem = useItem;
            itemCost = sellingUseItem.getSellPrice();
            costText.text = string.Format("Total Cost: {0:n0}", itemCost);
            itemQuantity = sellingUseItem.getQuantity();
            sellQuantity = 1;
            totalCost = itemCost * sellQuantity;
            quantityText.text = sellQuantity.ToString();
            nameText.text = sellingUseItem.getName();
            sellEach.text = sellingUseItem.getSellPrice().ToString();
        }

        if (sellQuantity == itemQuantity)
            quantityUpButton.interactable = false;

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void quantityUp()
    {
        sellQuantity++;
        quantityText.text = sellQuantity.ToString();
        totalCost = itemCost * sellQuantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        if (sellQuantity == itemQuantity)
            quantityUpButton.interactable = false;

        if (quantityDownButton.interactable == false)
            quantityDownButton.interactable = true;        

        if (sellButton.interactable == false)
            sellButton.interactable = true;
    }

    public void quantityDown()
    {
        sellQuantity--;
        quantityText.text = sellQuantity.ToString();
        totalCost = itemCost * sellQuantity;
        costText.text = string.Format("Total Cost: {0:n0}", totalCost);

        if (sellQuantity == 0)
        {
            quantityDownButton.interactable = false;
            sellButton.interactable = false;
        }


        if (quantityUpButton.interactable == false)
            quantityUpButton.interactable = true;
    }

    public void closePanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void sellItemQuantity()
    {
        if(sellingEquipItem != null)
        {
            sellingEquipItem.changeQuantity(-sellQuantity);
            CharacterInventory.charInven.removeEquipableFromInventory(sellingEquipItem);
            CharacterManager.charManager.changeGold(totalCost);
            CloseSellPanel.closeSellPanel.updateGold();
            shopSell.shSell.shopSellPanel();
        }
        else
        {
            sellingUseItem.changeQuantity(-sellQuantity);
            CharacterInventory.charInven.removeUsableFromInventory(sellingUseItem);
            CharacterManager.charManager.changeGold(totalCost);
            CloseSellPanel.closeSellPanel.updateGold();
            shopSell.shSell.shopSellPanel();
        }

        closePanel();
    }
}
