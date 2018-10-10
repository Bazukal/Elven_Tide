using UnityEngine;

public class EnemyInteractions : MonoBehaviour {

    private bool targeted = false;
    public GameObject target;
	
	public void enemyTargeted(int slot)
    {
        if (targeted == true)
        {
            targeted = false;
            target.SetActive(false);
            BattleScript.battleOn.removeEnemySelected();
        }
        else
        {
            targeted = true;
            try
            {
                HoleBattle.battle.selectedEnemy(slot);
            }
            catch
            {
                DungeonBattle.battle.selectedEnemy(slot);
            }

            target.SetActive(true);
        }
    }
}
