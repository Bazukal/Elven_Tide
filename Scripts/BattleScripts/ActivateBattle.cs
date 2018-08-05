using UnityEngine;

public class ActivateBattle : MonoBehaviour {

    public static ActivateBattle active;

    public GameObject battleScreen;

    private void Start()
    {
        active = this;
        //battleScreen.SetActive(false);
    }
    
    //activates battle and spawns enemies
    public void battle(bool boss)
    {
        battleScreen.SetActive(true);
        BattleScript.battleOn.StartBattle(boss);
        //StateMachine.state.StartBattle(boss);
    }
}
