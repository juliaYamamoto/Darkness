using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Movement
    private float horizontalInput;
    private float verticalInput;
    public float speed;

    //Light
    private float minLightIntensity = 5.0f;
    private float maxLightIntensity = 20.0f;

    public float currentLightIntensity = 0.0f;
    public Light characterLight;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Light
        currentLightIntensity = minLightIntensity;
        characterLight.intensity = currentLightIntensity;
    }


    private void Update()
    {
        //Movement
        transform.Translate(new Vector3(horizontalInput, verticalInput));
    }

    private void FixedUpdate()
    {
        //Movement
        horizontalInput = (Input.GetAxis("Horizontal") * speed) * Time.fixedDeltaTime;
        verticalInput = (Input.GetAxis("Vertical") * speed) * Time.fixedDeltaTime;
    }

   

}
