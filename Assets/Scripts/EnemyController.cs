using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 90f;
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform visionRayPoint;
    public float playerHeightOffSet = 1f;
    public bool canRotate = true;

    GameLogic gameLogic;



    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameLogic.canMove == false)
        {
            animator.SetFloat("MoveSpeed", 0);
            agent.isStopped = true;
            return;
        }

        if(agent.remainingDistance < 0.1f)
        {
            animator.SetFloat("MoveSpeed", 0);
            canRotate = true;
        }
        if(canRotate == true)
        {
            //rotate character
            //scale by speed and time
            float rotateAmount = rotationSpeed * Time.deltaTime;
            // apply rotation
            transform.Rotate(Vector3.up, rotateAmount);
        }
        
    }   
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // is there something in fornt the player?
            // get direction from enemy to player
            Vector3 playerPosition = other.transform.position;
            playerPosition.y += playerHeightOffSet;
            Vector3 rayDir = playerPosition - visionRayPoint.position;
            Ray ray = new Ray(visionRayPoint.position, rayDir);
            //generate raycast
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //check tag of hit object
                if(hit.transform.gameObject.tag == "Player")
                {
                    canRotate = false;
                    agent.SetDestination(other.transform.position);
                    animator.SetFloat("MoveSpeed", 1);
                }
            }

        }
    }
}
