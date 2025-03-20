using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float velocity = 0;
    private Transform selfTransform;
    private Transform player;
    private float followRadius = 7.0f;
    public void Enter()
    {
        Debug.Log("Entered AttackState");
        selfTransform = GameObject.Find("Moveable").transform;

        // Code to execute when entering the  state
    }

    public void Execute()
    {
       while (velocity >= 3)
        {
            Debug.Log("velocity is more than 3");
        }
        //if hit enemy setstate stunned
        if (selfTransform != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(selfTransform.position, player.position);
            if (distanceToPlayer > followRadius)
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