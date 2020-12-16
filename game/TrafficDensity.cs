using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficDensity : MonoBehaviour
{
    public BoxCollider area;

    private sbyte[] zPos = { -4, -2, 0, 2, 4};
    private byte[] aDensity = new byte[4];
    private byte DensityCounter = 0;

    private byte cc = 0;
    void Start()
    {
       // area = gameObject.GetComponent<BoxCollider>();
        AreaDensity();
    }


    public void AreaDensity()
    {
        if (DensityCounter == 4)
        {
            Debuger();
            return;
        }
           
        area.center = new Vector3(0, 0, zPos[DensityCounter]);
        StartCoroutine(CalculateDensity());

    }
    IEnumerator CalculateDensity()
    {
        yield return new WaitForEndOfFrame();
        
        aDensity[DensityCounter] = cc;
        cc = 0;
        DensityCounter++;
        AreaDensity();

    }
    public void Debuger()
    {
        cc = 0;
        DensityCounter = 0;

    }

    public byte GetLessDensity()
    {
        byte a = 0;
        for (byte i = 0; i < 4; i++)
        {
            if (aDensity[i] < a)
                a = aDensity[i];
        }
        return a;
    }
    public byte GetHighDensity()
    {
        byte a = 0;
        for (byte i = 0; i < 4; i++)
        {
            if (aDensity[i] > a)
                a = aDensity[i];
        }
        return a;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TrafficCar")
        {
            cc++;
        }
    }

}
