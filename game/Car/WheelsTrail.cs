using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsTrail : MonoBehaviour
{
    private TrailRenderer RightTrail;
    private TrailRenderer LeftTrail;
    public bool BrakeTrail;

   [SerializeField] float movingSpeed;
    void Start()
    {
        LeftTrail = transform.GetChild(0).GetComponent<TrailRenderer>();
        RightTrail = transform.GetChild(1).GetComponent<TrailRenderer>();
    }

    
    void FixedUpdate()
    {
        movingSpeed = CarMovement.CarSpeed / 90;
        if (BrakeTrail)
        {
            transform.position -= Vector3.forward * movingSpeed;
           // LeftTrail.position += Vector3.forward * movingSpeed;
          //  RightTrail.position += Vector3.forward * movingSpeed;
        }
    }

    public void ActiveTrail(bool active, Vector3 parPos, Vector3 leftPos, Vector3 rightPos)
    {
       
        LeftTrail.GetComponent<TrailRenderer>().enabled = true;
        RightTrail.GetComponent<TrailRenderer>().enabled = true;

        BrakeTrail = active;
        if(!active)
        {
            LeftTrail.enabled = false;
            RightTrail.enabled = false;
            transform.transform.TransformPoint(parPos);
            LeftTrail.transform.position = leftPos;
            RightTrail.transform.position = rightPos;
        }
        else if(!LeftTrail.enabled)
        {
            RightTrail.Clear();
            LeftTrail.Clear();
            RightTrail.enabled = true;
            LeftTrail.enabled = true;
        }
    }

    
}
