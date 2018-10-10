using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStats : MonoBehaviour {

    public static MiniStats stats;

    public Image p1Sprite;
    public Image p2Sprite;
    public Image p3Sprite;
    public Image p4Sprite;

    public Slider p1HP;
    public Slider p1MP;
    public Slider p1Exp;

    public Slider p2HP;
    public Slider p2MP;
    public Slider p2Exp;

    public Slider p3HP;
    public Slider p3MP;
    public Slider p3Exp;

    public Slider p4HP;
    public Slider p4MP;
    public Slider p4Exp;

    private ScriptablePlayer p1;
    private ScriptablePlayer p2;
    private ScriptablePlayer p3;
    private ScriptablePlayer p4;

    private List<ScriptablePlayer> players = new List<ScriptablePlayer>();
    private List<Slider> hpSliders = new List<Slider>();
    private List<Slider> mpSliders = new List<Slider>();
    private List<Slider> expSliders = new List<Slider>();

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);

        if (stats == null)
            stats = this;
        else if (stats != this)
            Destroy(gameObject);

        p1 = Manager.manager.GetPlayer("Player1");
        players.Add(p1);
        p2 = Manager.manager.GetPlayer("Player2");
        players.Add(p2);
        p3 = Manager.manager.GetPlayer("Player3");
        players.Add(p3);
        p4 = Manager.manager.GetPlayer("Player4");
        players.Add(p4);

        p1Sprite.sprite = p1.classHead;
        p2Sprite.sprite = p2.classHead;
        p3Sprite.sprite = p3.classHead;
        p4Sprite.sprite = p4.classHead;

        hpSliders.Add(p1HP);
        hpSliders.Add(p2HP);
        hpSliders.Add(p3HP);
        hpSliders.Add(p4HP);

        mpSliders.Add(p1MP);
        mpSliders.Add(p2MP);
        mpSliders.Add(p3MP);
        mpSliders.Add(p4MP);

        expSliders.Add(p1Exp);
        expSliders.Add(p2Exp);
        expSliders.Add(p3Exp);
        expSliders.Add(p4Exp);

        updateSliders();
    }

    //call to update slider values
    public void updateSliders()
    {
        for(int i = 0;i < players.Count;i++)
        {
            int playerLevel = players[i].level;

            float hpValue = (float)players[i].currentHp / (float)players[i].levelHp[playerLevel];
            hpSliders[i].value = hpValue;

            float mpValue = (float)players[i].currentMp / (float)players[i].levelMp[playerLevel];
            mpSliders[i].value = mpValue;

            float expValue = (float)players[i].currentExp / (float)players[i].expChart.experience[playerLevel];
            expSliders[i].value = expValue;
        }
    }

    //Destroys when going back to title screen from Innkeeper
    public void destroyThis()
    {
        Destroy(gameObject);
    }
}
