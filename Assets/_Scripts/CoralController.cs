using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralController : MonoBehaviour
{
    //Light
    private float minLightIntensity = 1.0f;
    private float maxLightIntensity = 4.0f;

    public bool isTouched = false;
    public bool isGettingLight = false;
    public float speedOnGettingLight = 1.2f;
    public Light coralLight;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Light
        coralLight.intensity = minLightIntensity;
    }

    private void Update()
    {
        if (isGettingLight) {
            coralLight.intensity += (speedOnGettingLight * Time.deltaTime);

            if (coralLight.intensity >= maxLightIntensity) {
                coralLight.intensity = maxLightIntensity;
                isGettingLight = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character" && !isTouched)
        {
            //isTouched = true;
            isGettingLight = true;
        }
    }
}
