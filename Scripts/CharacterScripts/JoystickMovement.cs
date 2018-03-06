using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class JoystickMovement : MonoBehaviour {

    private float moveSpeed = 0.1f;
    private float perBattleTime = 0.25f;
    private float battleTime;

    private Animator animator;

    private string lastMov = "Front";

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();
        battleTime = perBattleTime;
    }

    // Movement Controlls
    void Update () {
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveSpeed;
        gameObject.transform.Translate(moveVec);

        float horiz = CrossPlatformInputManager.GetAxis("Horizontal");
        float vert = CrossPlatformInputManager.GetAxis("Vertical");

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //check to see if player gets into a battle
        if (horiz > 0 || vert > 0)
        {
            if (sceneName != "Town")
            {
                battleTime -= Time.deltaTime;

                if(battleTime <= 0)
                {
                    int battleCheck = Random.Range(0, 101);
                    if (battleCheck > 95)
                        ActivateBattle.active.battle();

                    battleTime = perBattleTime;
                }

                
            }
        }

        //plays animations based on direction player is moving
        if (Mathf.Abs(horiz) > Mathf.Abs(vert))
        {
            if(horiz > 0)
            {
                animator.Play("Right Walk");
                lastMov = "Right";
            }
            else
            {
                animator.Play("Left Walk");
                lastMov = "Left";
            }
        }
        else if (Mathf.Abs(horiz) < Mathf.Abs(vert))
        {
            if(vert > 0)
            {
                animator.Play("Back Walk");
                lastMov = "Back";
            }
            else
            {
                animator.Play("Front Walk");
                lastMov = "Front";
            }
        }

        if(horiz == 0 && vert == 0)
        {
            switch(lastMov)
            {
                case "Right":
                    animator.Play("Right Idle");
                    break;
                case "Left":
                    animator.Play("Left Idle");
                    break;
                case "Front":
                    animator.Play("Front Idle");
                    break;
                case "Back":
                    animator.Play("Back Idle");
                    break;

            }
        }
    }

    //checks if certain objects come within range of player
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CaveExit")
        {
            CharacterManager.charManager.setInRange("CaveExit");
            return;
        }

        if (other.tag == "InnKeeper")
        {
            CharacterManager.charManager.setInRange("InnKeeper");
            return;
        }

        if (other.tag == "Healer")
        {
            CharacterManager.charManager.setInRange("Healer");
            return;
        }

        if (other.tag == "Blacksmith")
        {
            CharacterManager.charManager.setInRange("Blacksmith");
            return;
        }

        if (other.tag == "Cave")
        {
            
            CharacterManager.charManager.setInRange("Cave");
            return;
        }

        if (other.tag == "Chest")
        {
            CharacterManager.charManager.setChest(other.gameObject);
            CharacterManager.charManager.setInRange("Chest");
            return;
        }

        if (other.tag == "Citizen1")
        {
            CharacterManager.charManager.setInRange("Citizen1");
            return;
        }

        if (other.tag == "Citizen2")
        {
            CharacterManager.charManager.setInRange("Citizen2");
            return;
        }

        if (other.tag == "Citizen3")
        {
            CharacterManager.charManager.setInRange("Citizen3");
            return;
        }

        if (other.tag == "Citizen4")
        {
            CharacterManager.charManager.setInRange("Citizen4");
            return;
        }

        if (other.tag == "Master")
        {
            CharacterManager.charManager.setInRange("Master");
            return;
        }

    }

    //checks if certain objects leaves range of player
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CaveExit")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "InnKeeper")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Healer")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Blacksmith")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Chest")
        {
            CharacterManager.charManager.removeChest();
            return;
        }

        if (other.tag == "Cave")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Citizen1")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Citizen2")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Citizen3")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Citizen4")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }

        if (other.tag == "Master")
        {
            CharacterManager.charManager.setInRange("");
            return;
        }
    }
}
