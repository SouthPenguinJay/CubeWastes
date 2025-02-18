using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
 
public class ThrowBall : MonoBehaviour
{
 
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;
 
    public float MinSwipDist = 0;
    private float BallVelocity = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 350;
    public Vector3 angle;
    public float SpeedMultiplier = 1.5f;


    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;
   
    public LineRenderer directionGuide;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
     
        if (directionGuide == null)
        {
            directionGuide = new GameObject("DirectionGuide").AddComponent<LineRenderer>();
            directionGuide.startWidth = 0.02f;
            directionGuide.endWidth = 0.02f;
            directionGuide.positionCount = 8 * 2; // 8 riktningar
            directionGuide.material = new Material(Shader.Find("Sprites/Default"));
            directionGuide.startColor = Color.blue;
            directionGuide.endColor = Color.blue;

            UpdateDirectionGuide();
        }
    }
    private void Update()
    {
        UpdateDirectionGuide();
    }

    private void UpdateDirectionGuide()
    {
        if (directionGuide == null) return;
        float radius = 2f;
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            directionGuide.SetPosition(i * 2, transform.position);
            directionGuide.SetPosition(i * 2 + 1, transform.position + direction);
        }
    }

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        holding = true;
    }
 
    private void OnMouseDrag()
    {
        PickupBall();
    }
 
    private void OnMouseUp()
    {
        endTime = Time.time;
        endPos = Input.mousePosition;
        swipeDistance = (endPos - startPos).magnitude;
        swipeTime = endTime - startTime;
      

        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            //throw ball
            CalSpeed();
            CalAngle();
            rb.velocity *= 0.5f;
            rb.angularVelocity *= 0.5f;
            rb.AddForce(new Vector3((angle.x * BallSpeed), (angle.y * BallSpeed / 3), (angle.z * BallSpeed) * 2));
           // rb.AddForce(new Vector3((0), (BallSpeed / 3), (-BallSpeed)));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            //Invoke("ResetBall", 4f);
        }
    }
 
    void PickupBall()
    {
     //   Vector3 mousePos = Input.mousePosition;
     //   mousePos.z = Camera.main.nearClipPlane * 5f;
     // transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 8f * Time.deltaTime);
    }
 

 
    private void CalAngle()
    {
        Vector2 swipeDirection = (endPos - startPos).normalized;
        float angleInDegrees = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;

        // Snappa till närmaste 8 riktningar
        float snappedAngle = Mathf.Round(angleInDegrees / 45f) * 45f;

        // Konvertera till enhetsvektor
        float radianAngle = snappedAngle * Mathf.Deg2Rad;
        Vector3 localDirection = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

        // Korrigera kamerans inverkan
        Vector3 worldDirection = Camera.main.transform.TransformDirection(localDirection);
        worldDirection.y = 0; // Förhindra att kamerans lutning påverkar rörelsen
        angle = worldDirection.normalized;
    }



    void CalSpeed()
    {
        if (swipeTime > 0)
        {
            float normalizedSwipe = Mathf.Clamp01(swipeDistance / 300f); // Normalisera till 0-1
            BallVelocity = normalizedSwipe * SpeedMultiplier * MaxBallSpeed;

            // Om swipen är nära maxlängd, ge en extra boost
            if (swipeDistance > 250f)
            {
                BallVelocity *= 1.5f; // 50% extra fart vid maxdrag
            }
        }

        BallSpeed = Mathf.Clamp(BallVelocity, 10f, MaxBallSpeed * 1.2f); // Tillåter viss överskjutning av max
        swipeTime = 0;
    }
}
