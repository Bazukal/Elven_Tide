using UnityEngine;

public class SaveSellSelection : MonoBehaviour {

    public void saveOrSell()
    {
        string whosInRange = Manager.manager.getInRange();

        switch (whosInRange)
        {
            case "InnKeeper":
                InnKeeperSave.save.openSave();
                break;
            case "Healer":
                CloseNpcPanel.closeNpcPanel.activatePanel();
                CloseSellPanel.closeSellPanel.activatePanel();

                shopSell.shSell.shopSellPanel();
                break;
            case "Blacksmith":
                CloseNpcPanel.closeNpcPanel.activatePanel();
                CloseSellPanel.closeSellPanel.activatePanel();

                shopSell.shSell.shopSellPanel();
                break;
        }
    }
}
