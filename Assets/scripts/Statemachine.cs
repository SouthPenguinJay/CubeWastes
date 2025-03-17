using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine : MonoBehaviour
{
    private IState currentState;

    public void SetState(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}