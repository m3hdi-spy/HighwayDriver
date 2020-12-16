using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    
    private static float movingSpeed = 4;
    public static float GetMovingSpeed
    {
        get { return movingSpeed; }
    }
    private CarMovement theCar;
    void Start()
    {
        theCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
    }

    void FixedUpdate()
    {
        movingSpeed = CarMovement.CarSpeed / 100;
        transform.position -= Vector3.forward * movingSpeed;
    }


  
}

