using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private static Engine _instance;
    public static Engine Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Engine Instance is NULL.");

            return _instance;
        }
    }

   
    private float _currentSpeed;
    public  float CurrentSpeed
    {
        get { return _currentSpeed; }
        private set { _currentSpeed = value; }
    }

    [Range(0, 10)]
    public float _rpm;
    public byte _gear;
    public bool isPushGas;
    public bool isPushBrake;

    void Awake()
    {
        _instance = this;
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Gaz(byte currentgear, bool ig, bool ib)
    {
       
        _gear = currentgear;
      //  _currentSpeed = (rpm*1000 * 57 * 0.00188f) / (gr * dr);//V = (engine rpm* wheel tyre perimeter)/ (gear ratio* axle ratio)
                                                               // _rpm = rpm;
        isPushGas = ig;
        isPushBrake = ib;
    }


    public Vector2 Steering(float _steerInput)
    {
        Vector2 _SteeringWheels = Vector2.zero;
        if (_steerInput > 0)
        {
           // _SteeringWheels.x = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_turnRadius + (_rearTrack / 2))) * _steerInput;
           // _SteeringWheels.y = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_turnRadius - (_rearTrack / 2))) * _steerInput;
        }
        else if (_steerInput < 0)
        {
           // _SteeringWheels.x = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_turnRadius - (_rearTrack / 2))) * _steerInput;
          //  _SteeringWheels.y = Mathf.Rad2Deg * Mathf.Atan(_wheelBase / (_turnRadius + (_rearTrack / 2))) * _steerInput;
        }
        else
            _SteeringWheels = Vector2.zero;

        //ster = _SteeringWheels;
        return _SteeringWheels;
    }
}
