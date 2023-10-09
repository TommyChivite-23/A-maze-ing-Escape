using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float moveSpeed = 5f;

    public CharacterController controller;
    public Animator animator;

    GameLogic gameLogic;


    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameLogic.canMove == false)
        {
            animator.SetFloat("MoveSpeed", 0);
            return;
        }
        //rotate character
        // get horizontal
        float rotateAmount = Input.GetAxis("Horizontal");
        //scale by speed and time
        rotateAmount *= rotateSpeed * Time.deltaTime;
        // apply rotation
        transform.Rotate(Vector3.up, rotateAmount);
        //move character
        // get vertical 
        float moveAmount = Input.GetAxis("Vertical");
        //update animaton
        animator.SetFloat("MoveSpeed", moveAmount);
        //get forward
        Vector3 moveVector = transform.forward * moveAmount;
        // scale by speed and time
        moveVector *= moveSpeed * Time.deltaTime;
        // ipdate animation 
        controller.Move(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal")
        {
            gameLogic.WinGame();
        }
    }
}
