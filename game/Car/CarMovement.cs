using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform BodyObj;
    public static float CarSpeed = 0.0f;


    private delegate void SuspentionAnimation();

    private float[] SteeringPlace = new float[5] {0, -8.23f, -2.65f, 2.65f, 8.23f };
    [SerializeField]
    private byte currentLane = 3;
    
    public bool bSteering;
    private bool bCanSteer = true;
    private float steerPower;
    private enum SteeringMode { None, Left, Right};
    [SerializeField]
    private SteeringMode currentSteering;

    //---- Car Rotation in Steering/Acceleration
    Vector3 LastEuler = Vector3.zero;
    float XeulerReset = -90;
    public float Xeuler = -90;

    //----- Car Steer Anim
    float SteerRation = 0;
    public bool TimeToChange;
    
    private Car myCar;
    Animator CarAnim;
   void Awake()
    {
        myCar = gameObject.GetComponent<Car>();
        CarAnim = myCar.GetComponent<Animator>();
        steerPower = myCar.SteerPower;
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (bSteering)
            Steering();
    }

    
  /*  private void Brake()
    {
        if(CarSpeed <= myCar.MinSpeed)
            return;
        else
        {
            
            CarSpeed -= myCar.BrakePower * (Time.deltaTime * 0.02f);
        }
    }*/

    public void SteerLeft()
    {
        if (!IsSteerAvalable(true) || !bCanSteer)
            return;

        currentSteering = SteeringMode.Left;
        bCanSteer = false;
        bSteering = true;
        currentLane--;

    }

    public void SteerRight()
    {
        if (!IsSteerAvalable(false) || !bCanSteer)
            return;

        currentSteering = SteeringMode.Right;
        bCanSteer = false;
        bSteering = true;
        currentLane++;
    }

    private void Steering()
    {
        float currentSteerPower = steerPower + (CarSpeed / 150);
        Vector3 targetPos = new Vector3(SteeringPlace[currentLane], transform.position.y, transform.position.z);
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * currentSteerPower);
        
        
        transform.position = newPos;
        if(currentSteering == SteeringMode.Left)
        {
            if (transform.position.x < targetPos.x + 0.65f)
            {
                bCanSteer = true;
            }
                if (transform.position.x < targetPos.x + 0.15f)
            {
                bSteering = false;
                currentSteering = SteeringMode.None;
            }
        }
        else if(currentSteering == SteeringMode.Right)
        {
            if (transform.position.x > targetPos.x - 0.65f)
            {
                bCanSteer = true;
            }
            if (transform.position.x > targetPos.x - 0.15f)
            {
                bSteering = false;
                currentSteering = SteeringMode.None;
            }
        }
        SmoothlyAnimation(currentSteerPower);
    }
    private void SmoothlyAnimation(float speed)
    {
        Vector3 RightSide = new Vector3(-95, 270, 90);
        Quaternion NormalEuler = Quaternion.Euler(0, 0, 0);
        Vector3 LeftSide = new Vector3(-95, 90, 270);
        
        
        if (currentSteering == SteeringMode.Left)
        {
            if (Xeuler <= LeftSide.x + 0.5f)
            {
                TurnBackToNormal(speed);
                return;
            }
                
            Xeuler = Mathf.Lerp(Xeuler, LeftSide.x, Time.deltaTime * (speed*2));
            BodyObj.eulerAngles = new Vector3(Xeuler, LeftSide.y, LeftSide.z);
            LastEuler = BodyObj.eulerAngles;

        }

        else if(currentSteering == SteeringMode.Right)
        {
            if (Xeuler <= RightSide.x + 0.5f)
            {
                TurnBackToNormal(speed);
                return;
            }
            Xeuler = Mathf.Lerp(Xeuler, RightSide.x, Time.deltaTime * (speed*2));
            BodyObj.eulerAngles = new Vector3(Xeuler, RightSide.y, RightSide.z);
            LastEuler = BodyObj.eulerAngles;
            
        }
        else if(currentSteering == SteeringMode.None)
        {
            // BodyObj.rotation = Quaternion.Lerp(BodyObj.rotation, NormalEuler, Time.deltaTime * 5);
            //CarAnim.SetBool("TurnRight", false);
            //CarAnim.SetBool("TurnLeft", false);
           // Xeuler = 0;
        }
    }
    private void TurnBackToNormal(float speed)
    {
        Debug.Log("Ome");
        Xeuler = Mathf.Lerp(Xeuler, XeulerReset, Time.deltaTime * (speed * 5f));
        BodyObj.eulerAngles = new Vector3(Xeuler, LastEuler.y, LastEuler.z);
    }
    
    private bool IsSteerAvalable(bool strLeft)
    {

        if (strLeft && currentLane == 1)
            return false;
        else if (!strLeft && currentLane == 4)
            return false;
        else
            return true;
    }
    //--------------------- [Collisions]
    private void OnTriggerEnter(Collider otherColl)
    {
        switch(otherColl.gameObject.tag)
        {
            case "StartStreet":
                {
                   // MapGenerator.Instance.BuildMap();
                    break;
                }
            case "StreetCar":
                {
                    break;
                }
            case "CopCar":
                {
                    break;
                }
            case "MapSides":
                {
                    break;
                }
        }
    }

 
}
