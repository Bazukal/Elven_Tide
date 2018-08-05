using UnityEngine;

public class EnemyInteractions : MonoBehaviour {

    private bool targeted = false;
	
	public void enemyTargeted(int slot)
    {
        if (targeted == true)
        {
            removeTarget();
            BattleScript.battleOn.removeEnemySelected();
        }
        else
        {
            targeted = true;
            BattleScript.battleOn.selectedEnemy(slot);
        }
    }

    public void removeTarget()
    {
        targeted = false;
    }
}
