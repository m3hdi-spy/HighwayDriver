using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public byte ID;

    public byte SpeedRatio = 2;
    public byte SlowSpeedRatio = 4;
    private float WrongWaySpeedRatio = 1.5f;
    private float WrongWaySlowSpeedRatio = 1;
    private float Speed;
    public bool isWrongWay;

    [SerializeField]
    private bool isChangingSpeed;
    private bool isChangingLane;

    Vector3[] RaycastsPos = new Vector3[3];

    RaycastHit hit;
    RaycastHit hit2;

    RaycastHit hit3;
    RaycastHit hit4;

    private Street streets;
    void Start()
    {
        streets = transform.parent.parent.GetChild(2).gameObject.GetComponent<Street>();
        RaycastsPos = new Vector3[3] { (transform.right / 2) + transform.forward, transform.right, (transform.right * 2) - transform.forward };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isWrongWay)
            transform.position += transform.forward * Speed;
        else
        transform.position -= transform.forward * Speed;
    }


    private void LateUpdate()
    {
        if(ShouldSlowDown())
        {
            if(isWrongWay)
                Speed = Street.GetMovingSpeed * WrongWaySlowSpeedRatio;
            else
            Speed = Street.GetMovingSpeed / SlowSpeedRatio;
           
            isChangingSpeed = true;
        }
        else
        {
            isChangingSpeed = false;

            if (isWrongWay)
                Speed = Street.GetMovingSpeed * WrongWaySpeedRatio;
            else
                Speed = Street.GetMovingSpeed / SpeedRatio;
        }
    }

    private bool ShouldSlowDown()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit4, 3))
        {
            if (hit4.transform.tag == gameObject.tag)
                return true;
            else
                return false;
        }

        else
            return false;
        /*
        // if ((Physics.Raycast(transform.position, transform.forward + (transform.right / 3f), out hit3, 6) && Physics.Raycast(transform.position, -(transform.forward + (transform.right / 3f)), out hit4, 6)) || (Physics.Raycast(transform.position, transform.right, out hit2, 6) && Physics.Raycast(transform.position, -transform.right, out hit, 6)))
        if (Physics.Raycast(transform.position, transform.right, out hit2, 6) && Physics.Raycast(transform.position, -transform.right, out hit, 6))
       {
            if (hit.transform.CompareTag(hit2.transform.tag))
                return true;
            else
                return false;

       }
        else
            return false;
        */
       
    }

    private void ChangeLane()
    {
        if (gameObject.GetComponentInParent<TrafficGeneratorController>().MaxCarsInColumn > 3)
            return;

        if (Physics.Raycast(transform.position, RaycastsPos[0], 4) || Physics.Raycast(transform.position, RaycastsPos[1], 4) || Physics.Raycast(transform.position, RaycastsPos[2], 4))
            return;



    }// Not a Good IDEA now

    public void SetWrongway(bool isWrong)
    {
        if(isWrong)
        {
            isWrongWay = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            isWrongWay = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private void OnDrawGizmos()
    {


        /*
        if (Physics.Raycast(transform.position, transform.forward + (transform.right / 3f), 6))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, (transform.forward + (transform.right / 3f)) * 6);

        //--------------------------------------------
        if (Physics.Raycast(transform.position, -(transform.forward + (transform.right / 3f)), 6))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, -(transform.forward + (transform.right / 3f)) * 6);
        */
        //--------------------------------------------
        RaycastsPos = new Vector3[3] { (transform.right / 2) + transform.forward, transform.right, (transform.right * 2) - transform.forward };


        if (Physics.Raycast(transform.position, transform.right, 6))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.right * 6);

        //--------------------------------------------
        if (Physics.Raycast(transform.position, -transform.right, 6))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, -transform.right * 6);
        //==========================================================
        if (Physics.Raycast(transform.position, RaycastsPos[0], 4))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, RaycastsPos[0] * 4);

        if (Physics.Raycast(transform.position, RaycastsPos[1], 4))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, RaycastsPos[1] * 4);

        if (Physics.Raycast(transform.position, RaycastsPos[2], 4))
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, RaycastsPos[2] * 4);
    }
}
