using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool[] inputs = new bool[4];
    ECU car;
    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<ECU>();
    }
    void FixedUpdate()
    {
#if UNITY_EDITOR
        inputs[0] = true;
        inputs[1] = Input.GetKey(KeyCode.S);
        inputs[2] = Input.GetKeyDown(KeyCode.A);
        inputs[3] = Input.GetKeyDown(KeyCode.D);

       if(car.enabled) car.GetInputs(inputs);
#endif
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            var _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Stationary)
            {
                inputs[0] = false;
                inputs[1] = true;
            }
            else
            {
                inputs[0] = true;
                inputs[1] = false;
            }
            if(_touch.phase == TouchPhase.Moved)
            {
               if(_touch.deltaPosition.x < -10)
                {
                    inputs[2] = true;
                    inputs[3] = false;
                }
               else if (_touch.deltaPosition.x > 10)
                {
                    inputs[2] = false;
                    inputs[3] = true;
                }
            }

           
        }
        if (car.enabled) car.GetInputs(inputs);
        inputs[1] = false;
        inputs[2] = false;
        inputs[3] = false;
#endif
    }
}
