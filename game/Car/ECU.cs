using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ECU : Car
{

    public Text Vaziat;

    public static ushort _rpm = 2000;
    public static ushort RPM
    {
        get { return _rpm; }
    }

    private ushort TargetRPM;

    [Header("Gas Values")]
    public bool isPushGas;
    public bool isPushBrake;


    [SerializeField]
    private bool isGearDown, isGearUp;
    public byte CurrentGear = 0;
    public float RpmSpeed = -1;


    [Header("Speeding Info")]
    public float speed;
    public float SpeedTarget;
    public float EnginePower;

    
    public ushort rpm;
    
    //--- Inputs
    private bool inputGas, inputBrake, inputLeft, inputRight, inputHBrake;

    private void Start()
    {
    }
    public void GetInputs(bool[] _inputs)
    {
        inputGas = _inputs[0];
        inputBrake = _inputs[1];
        inputLeft = _inputs[2];
        inputRight = _inputs[3];


        if (inputBrake)
            PushBrake();
        else
            PushGas();

        if (inputLeft)
            transform.GetComponent<CarMovement>().SteerLeft();
        else if(inputRight)
            transform.GetComponent<CarMovement>().SteerRight();

       // Vaziat.text = "Gas:" + _inputs[0] + " Brake:" + _inputs[1];
    }

    private void PushGas()
    {
        //if torque > High torque :  takeoff
        //else add torque and move
        isPushGas = true;
        isPushBrake = false;
        SpeedTarget = SpeedsOfGears[CurrentGear];
        

        if (isGearUp) GearUpLessPower(SpeedRatio[CurrentGear]);
        else
            EnginePower = SpeedRatio[CurrentGear];

        Engine();
        GearBox();
        BrakeTrail(false);
    }

    private void PushBrake()
    {
        isPushBrake = true;
        isPushGas = false;
        if (CurrentGear == 0 && speed <= 5)
            CurrentGear = 0;
       
        SpeedTarget = SpeedsOfGears[CurrentGear];
        EnginePower = SpeedRatio[CurrentGear];
        Engine();
        GearBox();
        if (inputRight || inputLeft)
            BrakeTrail(false);
        else
            BrakeTrail(true);
    }
   

    private void Engine()
    {
        

        if(isPushGas)
        {
            if (speed > SpeedsOfGears[MaxGear]) return;
            speed += Time.deltaTime * EnginePower;
            _rpm += (ushort)(300 * Time.deltaTime * EnginePower);
        }
        else
        {
            if (speed < 10) return;
            speed -= Time.deltaTime * BrakePower;
            _rpm -= (ushort)(300 * Time.deltaTime * BrakePower);
        }

        
        CarMovement.CarSpeed = speed;
        rpm = RPM;
    }
    public void LoseGame()
    {
        speed = 0;
        CarMovement.CarSpeed = speed;
    }

    private void GearBox()
    {
        if (speed >= SpeedsOfGears[CurrentGear] - 1 && isPushGas && CurrentGear < MaxGear)
            GearUp();


        if (speed <= SpeedsOfGears[CurrentGear] + 1 && inputBrake && CurrentGear > 0)
            GearDown();

    }

    void GearUp()
    {
        CurrentGear++;
        _rpm = 2000;
        isGearUp = true;
        StartCoroutine(GearUpCoolDown());
    }
    void GearDown()
    {
        CurrentGear--;
        _rpm = 2000;
    }



    void GearUpLessPower(float standarsEngingPow)
    {
        float LessEngin = SpeedRatio[CurrentGear] / 3;
        EnginePower = Mathf.Lerp(LessEngin, standarsEngingPow, Time.deltaTime * 0.8f);
        speed -= Time.deltaTime * (EnginePower * 1.7f);
    }


    IEnumerator GearUpCoolDown()
    {
        
        yield return new WaitForSeconds(GearupCooldown);
        EnginePower = SpeedRatio[CurrentGear];
        isGearUp = false;
    }
    /// <summary>
    /// Calculate Car Speed SPECIAL FOR HIGHWAY DRIVER game
    /// </summary>
 /*   private void CarCalculateSpeed(bool gearU)
    {
        float InTimeSpeed = ShowenRpm * CurrentGear * ConstCarSpeed;
       // Debug.Log("showen: " + ShowenRpm + " Current Gear: " + CurrentGear);
        if (gearU)
        {
            speed = Mathf.Lerp(speed, SpeedHelper - 2f, Time.deltaTime * 2);
        }
        else
        
            SpeedHelper = speed = Mathf.Lerp(speed, InTimeSpeed, Time.deltaTime * 2);
       
        

        gameObject.GetComponent<CarMovement>().CarSpeed = speed;
        
    }*/

    void BrakeTrail(bool braking)
    {
        Transform tParent = transform.Find("Trails");
        GameObject.Find("VehicleWheelsTrail").GetComponent<WheelsTrail>().ActiveTrail(braking, tParent.position, tParent.GetChild(0).position, tParent.GetChild(1).position);
    }

}
