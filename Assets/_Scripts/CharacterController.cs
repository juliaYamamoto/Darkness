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
    public Light characterLight;
    private float minLightIntensity = 5.0f;
    private float maxLightIntensity = 20.0f;

    public bool isGettingLight = false;
    public bool isGivingLight = false;
    public float speedOnTransferingLight = 1.2f;
    public float targetLightAmount = 0;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Light
        //characterLight.intensity = minLightIntensity;
        characterLight.intensity = 15.0f;
    }


    private void Update()
    {
        //Movement
        transform.Translate(new Vector3(horizontalInput, verticalInput));

        //Light = Give
        if (isGivingLight)
        {
            characterLight.intensity -= (speedOnTransferingLight * Time.deltaTime);

            if (characterLight.intensity <= targetLightAmount)
            {
                characterLight.intensity = targetLightAmount;
                isGivingLight = false;

                if (targetLightAmount == 0)
                {
                    print("Morri");
                }
            }
        }

        //Light = Get
        if (isGettingLight)
        {
            characterLight.intensity += (speedOnTransferingLight * Time.deltaTime);

            if (characterLight.intensity >= targetLightAmount)
            {
                characterLight.intensity = targetLightAmount;
                isGettingLight = false;
                targetLightAmount = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        horizontalInput = (Input.GetAxis("Horizontal") * speed) * Time.fixedDeltaTime;
        verticalInput = (Input.GetAxis("Vertical") * speed) * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Coral")
        {
            CoralController coral = collision.gameObject.GetComponent<CoralController>();

            if (!coral.isTouched){
                coral.isTouched = true;
                GiveLight();
            }
        }
    }

    private void GiveLight()
    {
        if (!isGettingLight && !isGivingLight) {
            targetLightAmount = characterLight.intensity - 5.0f;
            isGivingLight = true;
        }

    }

    private void GetLight()
    {
        if (!isGettingLight && !isGivingLight)
        {
            targetLightAmount = characterLight.intensity + 10;
            if (targetLightAmount > maxLightIntensity)
            {
                targetLightAmount = maxLightIntensity;
                print("Maximo de luz");
            }
            
            isGettingLight = true;
        }
    }
}
