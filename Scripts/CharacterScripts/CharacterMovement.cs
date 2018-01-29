using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    private bool upPressed = false;
    private bool rightPressed = false;
    private bool downPressed = false;
    private bool leftPressed = false;

    private Animator animator;

    private float moveSpeed = 0.1f;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    //moves character based on which direction button is being pressed
    private void FixedUpdate()
    {
        if(upPressed == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.back, out hit, 2f))
            {
                if (hit.collider.tag != "Terrain")
                {
                    gameObject.transform.Translate(0, moveSpeed, 0);
                    animator.Play("Back Walk");
                }
                else
                    animator.Play("Back Idle");
            }
            else
            {
                gameObject.transform.Translate(0, moveSpeed, 0);
                animator.Play("Back Walk");
            }
        }

        if(rightPressed == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.right, out hit, 2f))
            {
                if (hit.collider.tag != "Terrain")
                {
                    gameObject.transform.Translate(moveSpeed, 0, 0);
                    animator.Play("Right Walk");
                }
                else
                    animator.Play("Right Idle");
            }
            else
            {
                gameObject.transform.Translate(moveSpeed, 0, 0);
                animator.Play("Right Walk");
            }
        }

        if(downPressed == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.back, out hit, 2f))
            {
                if (hit.collider.tag != "Terrain")
                {
                    gameObject.transform.Translate(0, -moveSpeed, 0);
                    animator.Play("Front Walk");
                }
                else
                    animator.Play("Front Idle");
            }
            else
            {
                gameObject.transform.Translate(0, -moveSpeed, 0);
                animator.Play("Front Walk");
            }
        }

        if(leftPressed == true)
        {
            

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.right, out hit, 2f))
            {
                if (hit.collider.tag != "Terrain")
                {
                    gameObject.transform.Translate(-moveSpeed, 0, 0);
                    animator.Play("Left Walk");
                }
                else
                    animator.Play("Left Idle");
            }
            else
            {
                gameObject.transform.Translate(-moveSpeed, 0, 0);
                animator.Play("Left Walk");
            }
        }
    }

    //sets which direction the character is moving, and plays animation, or stops character movement
    public void moveUp()
    {
        upPressed = true;        
    }

    public void stopUp()
    {
        upPressed = false;
        animator.Play("Back Idle");
    }

    public void moveRight()
    {
        rightPressed = true;
    }

    public void stopRight()
    {
        rightPressed = false;
        animator.Play("Right Idle");
    }

    public void moveDown()
    {
        downPressed = true;
    }

    public void stopDown()
    {
        downPressed = false;
        animator.Play("Front Idle");
    }

    public void moveLeft()
    {
        leftPressed = true;
    }

    public void stopLeft()
    {
        leftPressed = false;
        animator.Play("Left Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "InnKeeper")
        {
            CharacterManager.charManager.setInRange("InnKeeper");
            return;
        }

        if(other.tag == "Healer")
        {
            CharacterManager.charManager.setInRange("Healer");
            return;
        }

        if(other.tag == "Blacksmith")
        {
            CharacterManager.charManager.setInRange("Blacksmith");
            return;
        }

        if(other.tag == "Cave")
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

        if(other.tag == "Master")
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
