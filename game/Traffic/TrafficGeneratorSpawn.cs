using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class TrafficGeneratorSpawn : TrafficGeneratorController
{
    public GameObject CarsPrefab;
    public List<GameObject> Cars = new List<GameObject>();

    List<GameObject> EachLaneCars = new List<GameObject>();
    public Transform LastCarZ;

    protected float[] Xposition = { 8.22f, 2.65f, -2.65f, -8.25f };
    private float[] Zposition = { 12, 25, 45, 65, 85, 105, 125, 145, 165, 180 };

    public void SetTrafficCars(List<GameObject> mashin)
    {
        Cars = mashin;
    }

    
    public IEnumerator SpawnCarsInStreet(bool[,] place)
    {
        float LocalZ = 0;
        float Z = 0;

        if (LastCarZ == null)
            Z = 0;
        else
            Z = LastCarZ.transform.localPosition.z;
       /* if (LastCar == null)
            LastCarZ = 0;
        else
            LastCarZ = LastCar.transform.localPosition.z + 10;
       */
        if(Cars.Count < 20)
            yield break;

        for (byte i = 0; i < 4; i++)
        {
            for (byte j = 0; j < 10; j++)
            {
                if (place[i, j])
                {
                    if (Cars.Count == 0)
                        yield return new WaitForEndOfFrame();

                    byte k = (byte)Random.Range(1, Cars.Count - 1);


                    if (GameMode == 2 && (i == 2 || i == 3))
                    {
                        LocalZ = Z + 80;

                        Cars[k].GetComponent<TrafficCar>().SetWrongway(true);
                    }
                    else
                    {
                        LocalZ = Z;
                        Cars[k].GetComponent<TrafficCar>().SetWrongway(false);
                    }
                    //if (Zposition[j] <= 45 && Zposition[j] > 15)
                     //   LocalZ = -15;

                    Cars[k].gameObject.transform.localPosition = new Vector3(Xposition[i], 0.6f, Zposition[j] + LocalZ);
                    Cars[k].gameObject.GetComponent<TrafficCar>().enabled = true;
                    EachLaneCars.Add(Cars[k]);
                    Cars.Remove(Cars[k]);
                  
                }
                
            }
        }

        for (byte k = 0; k < EachLaneCars.Count; k++)
        {
            if (EachLaneCars[k].transform.localPosition.z > Z)
            {
                LastCarZ = EachLaneCars[k].transform;
                Z = LastCarZ.transform.localPosition.z;
            }

            if (k == EachLaneCars.Count - 1)
            {
                //Debug.Log(LastCarZ.);
                EachLaneCars.Clear();
            }
        }
        
    }


    public void RemoveCars()
    {
        foreach (Transform child in CarsPrefab.transform)
            Destroy(child);

        Cars.Clear();

    }

}
