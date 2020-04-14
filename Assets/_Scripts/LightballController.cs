using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightballController : MonoBehaviour
{
    public GameObject roundball;

    private void Update()
    {
        //Movement
        transform.Rotate(new Vector3(0, 0, 30 * Time.deltaTime));
    }
}
