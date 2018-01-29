using UnityEngine;
using UnityEngine.UI;

public class RestBuySelection : MonoBehaviour {    

    public void restOrBuy()
    {
        string whosInRange = CharacterManager.charManager.getInRange();

        int aveLvl = CharacterManager.charManager.aveLevel();

        switch (whosInRange)
        {
            case "InnKeeper":
                CharacterManager.charManager.character1.healToFull();
                CharacterManager.charManager.character2.healToFull();
                CharacterManager.charManager.character3.healToFull();
                CharacterManager.charManager.character4.healToFull();
                
                int restCost = aveLvl * 5;
                CharacterManager.charManager.changeGold(-restCost);

                Fade.fade.fadeNow();

                int goldAvail = CharacterManager.charManager.getGold();
                Button restButton = SelectionPanelData.panelData.restBuyButton;
                if (goldAvail < restCost)
                    restButton.interactable = false;
                else
                    restButton.interactable = true;
                break;
            case "Healer":
                CloseNpcPanel.closeNpcPanel.activatePanel();
                CloseBuyPanel.closeBuyPanel.activatePanel();

                shopBuy.sBuy.shopBuyPanel();
                break;
            case "Blacksmith":
                CloseNpcPanel.closeNpcPanel.activatePanel();
                CloseBuyPanel.closeBuyPanel.activatePanel();

                shopBuy.sBuy.shopBuyPanel();
                break;
        }
    }
}
