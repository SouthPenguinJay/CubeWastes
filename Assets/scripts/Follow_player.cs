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
        // Ber�kna offset baserat p� den initiala positionen
        offset = transform.position - player.position;
        offset2 = transform.position + player.position;
        offset2 = offset2 / 8;
    }
    // Update is called once per frame
    void Update () {
        // F�lj spelaren
        // Kontrollera om A-knappen �r nedtryckt
        if (Input.GetKey(KeyCode.A))
        {
            // Rotera kameran �t v�nster
            rotationY += rotationSpeed * Time.deltaTime;
        }

        // Kontrollera om D-knappen �r nedtryckt
        if (Input.GetKey(KeyCode.D))
        {
            // Rotera kameran �t h�ger
            rotationY -= rotationSpeed * Time.deltaTime;
        }

        // Ber�kna den nya rotationen
       

        // Uppdatera kamerans position och rotation
        if (Input.GetKey(KeyCode.W))
        {
            // Rotera kameran �t v�nster
            rotationX += rotationSpeed * Time.deltaTime;
        }

        // Kontrollera om D-knappen �r nedtryckt
        if (Input.GetKey(KeyCode.S))
        {
            // Rotera kameran �t h�ger
            rotationX -= rotationSpeed * Time.deltaTime;
        }

        // Ber�kna den nya rotationen
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


