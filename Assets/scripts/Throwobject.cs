using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwobject : MonoBehaviour
{
public float MaxObjectSpeed = 40;

public float FlickSpeed = 0.4f;

public float howClose = 9.5f;

float startTime, endTime, swipeDistance, swipeTime;
Vector2 startPos;
Vector2 endPos;
float tempTime;

float flicklength;
float Objectvelocity = 0;
float ObjectSpeed = 0;
Vector3 angle;
bool thrown, holding;
Vector3 newPosition, velocity;
    // Start is called before the first frame update
    void Start()
    {
      this.GetComponent<Rigidbody> ().useGravity = false;
    }

    // Update is called once per frame
    void OnTouch()
    {
        Vector3 mousePos = Input.GetTouch (0).position;
    }
}
