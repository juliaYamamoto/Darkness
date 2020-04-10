using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    public float speed;


    void FixedUpdate()
    {
        horizontalInput = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
        verticalInput = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;

        transform.Translate(new Vector3(horizontalInput, verticalInput));
    }
}
