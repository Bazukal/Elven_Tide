using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.Collections;

public class JoystickMovement : MonoBehaviour {

    public static JoystickMovement joystick;

    private float moveSpeed = 0.15f;
    private float perBattleTime = 0.25f;
    private float battleTime;

    private Animator animator;

    private string lastMov = "Front";
    private bool canMove = true;

    private CharacterController cc;

    string scene;

    // Use this for initialization
    void Start () {
        joystick = this;

        scene = Manager.manager.GetScene();

        animator = gameObject.GetComponentInChildren<Animator>();
        battleTime = perBattleTime;

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Transform cameraTrans = mainCamera.transform;
        cameraTrans.transform.SetParent(gameObject.transform);
        mainCamera.GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, 0);
        mainCamera.GetComponent<Transform>().localPosition = new Vector3(0, 0, -3.5f);

        cc = gameObject.GetComponent<CharacterController>();
    }

    public void SetScene(string whatScene) { scene = whatScene; }

    // Movement Controlls
    void FixedUpdate () {

        float horiz = 0;
        float vert = 0;

        if (canMove)
        {
            Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
            transform.TransformDirection(moveVec);
            moveVec *= moveSpeed;

            cc.Move(moveVec);

            horiz = CrossPlatformInputManager.GetAxis("Horizontal");
            vert = CrossPlatformInputManager.GetAxis("Vertical");
        }

        //check to see if player gets into a battle
        if (horiz > 0 || vert > 0)
        {
            if (scene.Equals("Dungeon"))
            {
                battleTime -= Time.deltaTime;

                if(battleTime <= 0)
                {
                    int battleCheck = Random.Range(0, 101);
                    if (battleCheck > 95)
                    {
                        StoreFinds.stored.BattleActivate();
                        ActivateBattle.active.battle(false);
                    }
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
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            switch (lastMov)
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
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void setMove(bool move)
    {
        canMove = move;
    }

    //moves character away from boss if retreated
    public void retreat()
    {
        Vector3 currentPos = transform.localPosition;

        Vector3 retreatMove = new Vector3(currentPos.x + 9, currentPos.y);
        transform.Translate(retreatMove);
    }

    //checks if certain objects come within range of player
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Terrain")
        {
            Manager.manager.setObject(other.gameObject);
        }
    }

    //checks if certain objects leaves range of player
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Terrain")
            Manager.manager.setObject(null);
    }
}
