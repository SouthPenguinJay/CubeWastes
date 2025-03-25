using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState
{
    float jumpForce = 5.0f;
    private Rigidbody rb;
    private Transform selfTransform;
    private float jumpInterval = 1.0f; // Time interval between jumps
    private float timeSinceLastJump = 0.0f;
    private Transform player;
    private float followRadius = 7.0f;
    private bool isMouseHeld = false;
    public IdleState(Transform player)
    {
        this.player = player; // Pass the player's Transform during initialization

    }
    public void Enter()
    {
        rb = GameObject.Find("Moveable").GetComponent<Rigidbody>();
        selfTransform = GameObject.Find("Moveable").transform;

        Debug.Log("Entered IdleState");
        timeSinceLastJump = jumpInterval;
    }

    public void Execute()
    {


        if (rb != null)
        {
            // Perform idle behavior (jumping)
            timeSinceLastJump += Time.deltaTime;
            if (timeSinceLastJump >= jumpInterval)
            {
                Debug.Log("Executing IdleState");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                timeSinceLastJump = 0.0f; // Reset the timer
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
            // Check distance to the player
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
                Debug.LogError("Rigidbody is not assigned in IdleState");
            }           
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the Idle state
        Debug.Log("Exited IdleState");
    }
}
