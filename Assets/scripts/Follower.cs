
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.AI;



public class follower : MonoBehaviour

{



    public Transform player;


    //Vector3 dest;
    public Rigidbody rb; // Koppla Rigidbody i Unity
    public float forceAmount = 10f; // Hur stark kraften ska vara
    public float maxSpeed = 5f;
    public float upForce = 2f;
    private bool shouldFollow = true;
    private float jumpCooldown = 1.5f; // Tid mellan hopp
    private float nextJumpTime = 0f; // N�r n�sta hopp f�r ske
    public float followRadius = 5f;
    private float maxDistance;
    private Vector3 randomDirection;
    private float newMaxSpeed = 7f;

    private float timeSinceLastDirectionChange = 0f;
    public float directionChangeInterval = 5f;
    public float randomMovementForce = 2f;
    private void Start()
    {
        maxDistance = followRadius;
    }


    void Update()

    {
        Statemachine hej = gameObject.GetComponent<Statemachine>();

        Vector3 direction = (player.position - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //dest = player.position;

        if (distanceToPlayer > followRadius)
        {
            direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * forceAmount, ForceMode.Acceleration);
        }
        else
        {
            timeSinceLastDirectionChange += Time.deltaTime; // L�gg till tid sedan senaste riktning

            if (timeSinceLastDirectionChange >= directionChangeInterval)
            {
                // Slumpm�ssig r�relse inom ett omr�de (radius 0.5 runt objektet)
                randomDirection = Random.insideUnitSphere * 0.5f;
                randomDirection.y = 0; // Se till att r�relsen sker p� samma h�jdplan (utan att g� upp eller ned)

                // Applicera extra kraft p� den slumpm�ssiga r�relsen

                rb.AddForce(randomDirection.normalized * randomMovementForce, ForceMode.Impulse);
                rb.AddForce(direction * forceAmount, ForceMode.Acceleration);
                timeSinceLastDirectionChange = 0f;
            }
            rb.AddForce(randomDirection.normalized * randomMovementForce, ForceMode.Impulse);
            rb.AddForce(direction * forceAmount, ForceMode.Acceleration);
            maxSpeed = newMaxSpeed;

        }

        // ai.destination = dest;
        if (player == null) return; // S�kerhetskontroll

        


        // Applicera kraft i riktning mot spelaren

        // Begr�nsa hastigheten
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (Time.time >= nextJumpTime)
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse); // Skapar ett hopp
            nextJumpTime = Time.time + jumpCooldown; // V�nta innan n�sta hopp
        }
        if (!shouldFollow || player == null) return;

    }
    private void OnMouseDown()
    {
        shouldFollow = false;
    }
    private void OnMouseUp()
    {
        shouldFollow = true;
    }

}