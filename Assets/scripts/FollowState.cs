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
        Debug.Log("Player position: " + player.position);
        Debug.Log("Moveable position: " + selfTransform.position);

        Vector3 direction = (player.position - selfTransform.position).normalized;

        float distanceToPlayer = Vector3.Distance(GameObject.Find("Moveable").transform.position, player.position);
        //dest = player.position;

        if (distanceToPlayer > followRadius)
        {
            direction = (player.position - selfTransform.position).normalized;
            rb.AddForce(direction * forceAmount, ForceMode.Acceleration);
        }
        if (Input.GetMouseButton(2)) // 0 is the left mouse button
        {
            Statemachine.Instance.SetState(new AttackState());
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
