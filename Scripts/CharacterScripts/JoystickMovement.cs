using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class JoystickMovement : MonoBehaviour {

    private float moveSpeed = 0.1f;

    private Animator animator;

    private string lastMov = "Front";

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveSpeed;
        gameObject.transform.Translate(moveVec);

        float horiz = CrossPlatformInputManager.GetAxis("Horizontal");
        float vert = CrossPlatformInputManager.GetAxis("Vertical");        

        if(Mathf.Abs(horiz) > Mathf.Abs(vert))
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


    private void OnTriggerEnter(Collider other)
    {
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

    private void OnTriggerExit(Collider other)
    {
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
            CharacterManager.charManager.setInRange("");
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
