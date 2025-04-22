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
        // Ber�kna offset baserat p� den initiala positionen
       offset = transform.position + player.position ;
        offset = offset / 5;
    }
    // Update is called once per frame
    void Update()
    {
        // F�lj spelaren
        // Kontrollera om A-knappen �r nedtryckt
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
        Quaternion targetRotation = Quaternion.Euler(rotationX, 0, 0);

        // Uppdatera kamerans position och rotation
        transform.position = player.position + targetRotation * offset;
        transform.LookAt(player.position);
    }
}

