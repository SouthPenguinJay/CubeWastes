using UnityEngine;

public class Follow_player : MonoBehaviour {

    public float rotationSpeed = 120f;
    public Transform player;
    private Quaternion initialRotation;
    public Vector3 offset;
    public Vector3 offset2;
    public float rotationX = 0f;
    public float rotationY = 0f; // Aktuell rotati

    private void Start()
    {
        initialRotation = transform.rotation;
        // Beräkna offset baserat på den initiala positionen
        offset = transform.position - player.position;
        offset2 = transform.position + player.position;
        offset2 = offset2 / 8;
    }
    // Update is called once per frame
    void Update () {
        // Följ spelaren
        // Kontrollera om A-knappen är nedtryckt
        if (Input.GetKey(KeyCode.A))
        {
            // Rotera kameran åt vänster
            rotationY += rotationSpeed * Time.deltaTime;
        }

        // Kontrollera om D-knappen är nedtryckt
        if (Input.GetKey(KeyCode.D))
        {
            // Rotera kameran åt höger
            rotationY -= rotationSpeed * Time.deltaTime;
        }

        // Beräkna den nya rotationen
       

        // Uppdatera kamerans position och rotation
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
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Uppdatera kamerans position och rotation
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)))
        {
            transform.position = player.position + targetRotation * offset2;
        }
        else 
        {
            transform.position = player.position + targetRotation * offset;
        }
        transform.LookAt(player.position);
    }
}


