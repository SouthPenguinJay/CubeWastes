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
        // Ber�kna offset baserat p� den initiala positionen
        offset = transform.position - player.position;
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
        Quaternion targetRotation = Quaternion.Euler(0, rotationY, 0);

        // Uppdatera kamerans position och rotation
        transform.position = player.position + targetRotation * offset;
        transform.LookAt(player.position);
    }
}

