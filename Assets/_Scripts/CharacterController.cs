using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //GameController
    public GameController gameController;
                
    //Movement
    private float horizontalInput;
    private float verticalInput;
    public float speed;

    //Light
    public Light characterLight;
    private float maxLightIntensity = 20.0f;

    public bool isGettingLight = false;
    public bool isGivingLight = false;
    public float speedOnTransferingLight = 1.2f;
    public float targetLightAmount = 0;

    //Light indicator
    public Color indicatorColorOn;
    public Color indicatorColorOff;
    public SpriteRenderer lightIndicator5;
    public SpriteRenderer lightIndicator10;
    public SpriteRenderer lightIndicator15;
    public SpriteRenderer lightIndicator20;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        //Game Controllerx
        this.gameController = GetComponent<GameController>();

        //Light
        characterLight.intensity = 5.0f;
        UpdateLightIndicator(amount: characterLight.intensity);
    }


    private void Update()
    {
        if(gameController.currentGameState == GameState.Keys || gameController.currentGameState == GameState.Game)
        {
            //Movement
            transform.Translate(new Vector3(horizontalInput, verticalInput));
        }

        //Light: Give
        if (isGivingLight)
        {
            characterLight.intensity -= (speedOnTransferingLight * Time.deltaTime);

            if (characterLight.intensity <= targetLightAmount)
            {
                characterLight.intensity = targetLightAmount;
                isGivingLight = false;

                if (targetLightAmount == 0)
                {
                    gameController.BadEnd();
                }

                targetLightAmount = 0;
            }
        }

        //Light: Get
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
        if (gameController.currentGameState == GameState.Keys || gameController.currentGameState == GameState.Game)
        {
            //Movement
            horizontalInput = (Input.GetAxis("Horizontal") * speed) * Time.fixedDeltaTime;
            verticalInput = (Input.GetAxis("Vertical") * speed) * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameController.currentGameState == GameState.Keys || gameController.currentGameState == GameState.Game)
        {

            if (collision.gameObject.tag == "Coral")
            {
                CoralController coral = collision.gameObject.GetComponent<CoralController>();

                if (!coral.isTouched)
                {
                    if (collision.gameObject.name == "FirstCoral")
                    {
                        gameController.CollectFirstCoral();
                    }

                    coral.isTouched = true;
                    GiveLight();
                }
            }

            else if (collision.gameObject.tag == "Lightball")
            {
                if (collision.gameObject.name == "FirstLightball")
                {
                    gameController.CollectFirstLightball();
                }
                else {
                    Destroy(collision.gameObject);
                }
                GetLight();
            }
        }
    }

    private void GiveLight()
    {
        if (!isGettingLight && !isGivingLight) {
            targetLightAmount = characterLight.intensity - 5.0f;
            isGivingLight = true;
            gameController.AddToTotalCorals();

            UpdateLightIndicator(amount: targetLightAmount);
        }

    }

    private void GetLight()
    {
        if (!isGettingLight && !isGivingLight)
        {
            targetLightAmount = targetLightAmount > maxLightIntensity ?
                characterLight.intensity + 10 : maxLightIntensity;
            
            isGettingLight = true;
            UpdateLightIndicator(amount: targetLightAmount);
        }
    }

    private void UpdateLightIndicator(float amount)
    {
        lightIndicator5.color = amount >= 5 ?  indicatorColorOn : indicatorColorOff;
        lightIndicator10.color = amount >= 10 ? indicatorColorOn : indicatorColorOff;
        lightIndicator15.color = amount >= 15 ? indicatorColorOn : indicatorColorOff;
        lightIndicator20.color = amount >= 20 ? indicatorColorOn : indicatorColorOff;
    }
}
