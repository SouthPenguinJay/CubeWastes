using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState
{
    public Transform player;
    Rigidbody rb;
    public float followRadius = 5f;
    private float forceAmount = 9f;
    private Transform selfTransform;
    private float maxSpeed = 5.0f;

    public FollowState(Transform player)
    {
        this.player = player; // Assign the player's transform via constructor

    }

    public void Enter()
    {
        rb = GameObject.Find("Moveable").GetComponent<Rigidbody>();
        // Code to execute when entering the Patrolling state
        selfTransform = GameObject.Find("Moveable").transform;
    }

    public void Execute()
    {
       // Debug.Log("Player position: " + player.position);
     //   Debug.Log("Moveable position: " + selfTransform.position);

        Vector3 direction = (player.position - selfTransform.position).normalized;

        float distanceToPlayer = Vector3.Distance(GameObject.Find("Moveable").transform.position, player.position);
        //dest = player.position;

        if (distanceToPlayer > followRadius)
        {
            direction = (player.position - selfTransform.position).normalized;
            rb.AddForce(direction * forceAmount, ForceMode.Acceleration);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        if (Input.GetMouseButton(0)) // 0 is the left mouse button
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object has the specific tag or layer
                if (hit.collider.CompareTag("Follower")) // Replace with your tag
                {
                    Statemachine.Instance.SetState(new AttackState(player));
                }
            }
        }
        // Code to execute while in the Patrolling state
        if (selfTransform != null && player != null)
        {
            
            if (distanceToPlayer < followRadius)
            {
                Debug.Log("Player is far away, transitioning to FollowState");
                Statemachine.Instance.SetState(new IdleState(player)); // Transition to FollowState
            }
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the Patrolling state
    }
}
