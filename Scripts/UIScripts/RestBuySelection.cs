using UnityEngine;
using UnityEngine.UI;

public class RestBuySelection : MonoBehaviour {    

    public void restOrBuy()
    {
        string whosInRange = Manager.manager.getInRange();

        int aveLvl = Manager.manager.AveLevel();

        switch (whosInRange)
        {
            case "InnKeeper":
                Manager.manager.GetPlayer("Player1").healToFull();
                Manager.manager.GetPlayer("Player2").healToFull();
                Manager.manager.GetPlayer("Player3").healToFull();
                Manager.manager.GetPlayer("Player4").healToFull();
                
                int restCost = aveLvl * 5;
                Manager.manager.changeGold(-restCost);

                Fade.fade.fadeNow();

                int goldAvail = Manager.manager.GetGold();
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
