using UnityEngine;

public class Follow_player : MonoBehaviour {

    public float rotationSpeed = 120f;
    public Transform player;
    private Quaternion initialRotation;
    public Vector3 offset;
    public float rotationY = 0f; // Aktuell rotati

    private void Start()
    {
        initialRotation = transform.rotation;
        // Beräkna offset baserat på den initiala positionen
        offset = transform.position - player.position;
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
        Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);

        // Uppdatera kamerans position och rotation
        transform.position = player.position + targetRotation * offset;
        transform.LookAt(player.position);
    }
}

