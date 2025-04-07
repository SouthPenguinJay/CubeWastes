using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Statemachine : MonoBehaviour
{
    private IState currentState;
    private Rigidbody rb;
    public Transform player;
    private static Statemachine instance; // Singleton instance

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // Destroy duplicate instances
            return;
        }
        instance = this; // Set the instance
        DontDestroyOnLoad(this.gameObject); // Optional: Keep the singleton across scenes
    }
    public static Statemachine Instance
    {
     
        get

        {
            if (instance == null)
            {
                Debug.LogError("Statemachine instance is not initialized!");
            }
            else
            {
              //  Debug.LogError(instance.name);
                return instance;
            }
            return null;
        }
    }
    private void Start()
    {
        SetState(new UnfoundState(player));
    }

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