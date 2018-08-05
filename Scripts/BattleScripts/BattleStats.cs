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

    private ScriptablePlayerClasses Player1;
    private ScriptablePlayerClasses Player2;
    private ScriptablePlayerClasses Player3;
    private ScriptablePlayerClasses Player4;

    // Use this for initialization
    void Awake () {
        stats = this;

        Player1 = Manager.manager.GetPlayer("Player1");
        Player2 = Manager.manager.GetPlayer("Player2");
        Player3 = Manager.manager.GetPlayer("Player3");
        Player4 = Manager.manager.GetPlayer("Player4");
    }

    private void Start()
    {
        updateStats();
    }

    public void updateStats()
    {
        play1Name.text = Player1.name;
        int p1Level = Player1.level;
        play1HP.text = string.Format("HP: {0}/{1}", Player1.currentHp, Player1.levelHp[p1Level]);
        play1MP.text = string.Format("MP: {0}/{1}", Player1.currentMp, Player1.levelMp[p1Level]);

        play2Name.text = Player2.name;
        int p2Level = Player2.level;
        play2HP.text = string.Format("HP: {0}/{1}", Player2.currentHp, Player2.levelHp[p2Level]);
        play2MP.text = string.Format("MP: {0}/{1}", Player2.currentMp, Player2.levelMp[p2Level]);

        play3Name.text = Player3.name;
        int p3Level = Player3.level;
        play3HP.text = string.Format("HP: {0}/{1}", Player3.currentHp, Player3.levelHp[p3Level]);
        play3MP.text = string.Format("MP: {0}/{1}", Player3.currentMp, Player3.levelMp[p3Level]);

        play4Name.text = Player4.name;
        int p4Level = Player4.level;
        play4HP.text = string.Format("HP: {0}/{1}", Player4.currentHp, Player4.levelHp[p4Level]);
        play4MP.text = string.Format("MP: {0}/{1}", Player4.currentMp, Player4.levelMp[p4Level]);
    }
}
