using UnityEngine;

public class rotatetest : MonoBehaviour
{

    public float rotationSpeed = 120f;
    public Transform player;
    private Quaternion initialRotation;
    public Vector3 offset;
    public float rotationX = 0f; // Aktuell rotati

    private void Start()
    {
        initialRotation = transform.rotation;
        // Beräkna offset baserat på den initiala positionen
       offset = transform.position + player.position ;
        offset = offset / 5;
    }
    // Update is called once per frame
    void Update()
    {
        // Följ spelaren
        // Kontrollera om A-knappen är nedtryckt
        if (Input.GetKey(KeyCode.W))
        {
            // Rotera kameran åt vänster
            rotationX += rotationSpeed * Time.deltaTime;
        }

        // Kontrollera om D-knappen är nedtryckt
        if (Input.GetKey(KeyCode.S))
        {
            // Rotera kameran åt höger
            rotationX -= rotationSpeed * Time.deltaTime;
        }

        // Beräkna den nya rotationen
        Quaternion targetRotation = Quaternion.Euler(rotationX, 0, 0);

        // Uppdatera kamerans position och rotation
        transform.position = player.position + targetRotation * offset;
        transform.LookAt(player.position);
    }
}

