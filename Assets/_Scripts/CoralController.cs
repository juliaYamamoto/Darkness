using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralController : MonoBehaviour
{
    //Light
    private float minLightIntensity = 0.8f;
    private float maxLightIntensity = 3.0f;

    public bool isTouched = false;
    public bool isGettingLight = false;
    public float speedOnGettingLight = 2.0f;
    public Light characterLight;



    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Light
        characterLight.intensity = minLightIntensity;
    }

    private void Update()
    {
        if (isGettingLight) {
            characterLight.intensity += (speedOnGettingLight * Time.deltaTime);

            if (characterLight.intensity >= maxLightIntensity) {
                characterLight.intensity = maxLightIntensity;
                isGettingLight = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character" && !isTouched)
        {
            isTouched = true;
            isGettingLight = true;
        }
    }
}
