using UnityEngine;

public class EnemyInteractions : MonoBehaviour {

    private bool targeted = false;
	
	public void enemyTargeted()
    {
        if (targeted == true)
        {
            removeTarget();
            StateMachine.state.removeEnemySelected();
        }
        else
        {
            StateMachine.state.removeEnemyTargets();
            targeted = true;
            gameObject.transform.Find("Target").gameObject.SetActive(true);
            StateMachine.state.SetEnemySelected(gameObject);
        }
    }

    public void removeTarget()
    {
        targeted = false;
        gameObject.transform.Find("Target").gameObject.SetActive(false);
    }
}
