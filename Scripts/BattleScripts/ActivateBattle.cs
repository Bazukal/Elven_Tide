using UnityEngine;

public class ActivateBattle : MonoBehaviour {

    public static ActivateBattle active;

    public GameObject battleScreen;

    private void Start()
    {
        active = this;
    }
    
    //activates battle and spawns enemies
    public void battle(bool boss)
    {
        battleScreen.SetActive(true);
        try
        {
            HoleBattle.battle.StartBattle(boss);
        }
        catch
        {
            DungeonBattle.battle.StartBattle(boss);
        }
    }
}
