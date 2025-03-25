using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float velocity = 0;
    private Transform selfTransform;
    private Transform player;
    private float followRadius = 7.0f;
    private Rigidbody rb;
    public AttackState(Transform player)
    {
        this.player = player; // Assign the player's transform via constructor

    }
    public void Enter()
    {
        rb = GameObject.Find("Moveable").GetComponent<Rigidbody>();
        Debug.Log("Entered AttackState");
        selfTransform = GameObject.Find("Moveable").transform;
         
        // Code to execute when entering the  state
    }

    public void Execute()
    {
        if (rb != null)
        {
            velocity = rb.velocity.magnitude;
            Debug.Log("Velocity: " + velocity);
        }

        
        //if hit enemy setstate stunned
        if (selfTransform != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(selfTransform.position, player.position);
            if (distanceToPlayer > followRadius && velocity < 1)
            {
                Debug.Log("Player is far away, transitioning to FollowState");
                Statemachine.Instance.SetState(new FollowState(player)); // Transition to FollowState
            }
        }
        else
        {
           // Debug.LogError("Rigidbody is not assigned in IdleState");
        }

        // Code to execute while in the  state

    }

    public void Exit()
    {
        Debug.Log("Exited AttackState");
        // Code to execute when exiting the  state
    }
}