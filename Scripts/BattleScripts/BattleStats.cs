using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStats : MonoBehaviour {

    public static BattleStats stats;

    public Text play1Name;
    public Text play1HP;
    public Text play1MP;

    public Text play2Name;
    public Text play2HP;
    public Text play2MP;

    public Text play3Name;
    public Text play3HP;
    public Text play3MP;

    public Text play4Name;
    public Text play4HP;
    public Text play4MP;

    private PlayerClass Player1;
    private PlayerClass Player2;
    private PlayerClass Player3;
    private PlayerClass Player4;

    // Use this for initialization
    void Start () {
        stats = this;

        Player1 = Manager.manager.GetPlayer("Player1");
        Player2 = Manager.manager.GetPlayer("Player2");
        Player3 = Manager.manager.GetPlayer("Player3");
        Player4 = Manager.manager.GetPlayer("Player4");

        updateStats();
    }

    public void updateStats()
    {
        play1Name.text = Player1.GetName();
        play1HP.text = string.Format("HP: {0}/{1}", Player1.GetCurrentHP(), Player1.GetMaxHP());
        play1MP.text = string.Format("MP: {0}/{1}", Player1.GetCurrentMP(), Player1.GetMaxMP());

        play2Name.text = Player2.GetName();
        play2HP.text = string.Format("HP: {0}/{1}", Player2.GetCurrentHP(), Player2.GetMaxHP());
        play2MP.text = string.Format("MP: {0}/{1}", Player2.GetCurrentMP(), Player2.GetMaxMP());

        play3Name.text = Player3.GetName();
        play3HP.text = string.Format("HP: {0}/{1}", Player3.GetCurrentHP(), Player3.GetMaxHP());
        play3MP.text = string.Format("MP: {0}/{1}", Player3.GetCurrentMP(), Player3.GetMaxMP());

        play4Name.text = Player4.GetName();
        play4HP.text = string.Format("HP: {0}/{1}", Player4.GetCurrentHP(), Player4.GetMaxHP());
        play4MP.text = string.Format("MP: {0}/{1}", Player4.GetCurrentMP(), Player4.GetMaxMP());
    }
}
