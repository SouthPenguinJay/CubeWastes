using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfoundState : IState
{
    // Start is called befo
    // re the first frame update
    private Transform player;
    public UnfoundState(Transform player)
    {
        this.player = player; // Assign the player's transform via constructor

    }
    public void Enter()
    {
       
    }

    // Update is called once per frame
    public  void Execute()
    {
        if (Input.GetMouseButton(1)) // 0 is the left mouse button
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object has the specific tag or layer
                if (hit.collider.CompareTag("Follower")) // Replace with your tag
                {
                    Statemachine.Instance.SetState(new FollowState(player));
                }
            }
        }
        //add sphere collider
    }
    public void Exit()
    {

    }
}
