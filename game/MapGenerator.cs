using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public GameObject[] StreetsPrefab;
    private byte mapID;

    public GameObject currentStreet;
    public GameObject LastStreet;

    private Transform CarsParent;
    public GameObject[] TrafficCarsPrefab;
    private byte MaxCars = 40;

    private static MapGenerator instance;
    public static MapGenerator Instance
    {
        get { return instance; }
    }
    void Awake()
    {
       instance = this;
        CarsParent = transform.GetChild(0);
    }

    public void StartGenerate(byte envID)
    {
        mapID = envID;
        currentStreet = Instantiate(StreetsPrefab[mapID], transform);

        GameObject SecStreet = Instantiate(StreetsPrefab[mapID], transform);
        SecStreet.transform.position = currentStreet.transform.GetChild(0).GetChild(currentStreet.transform.GetChild(0).childCount - 1).position;

        LastStreet = Instantiate(StreetsPrefab[mapID], transform);
        LastStreet.transform.position = SecStreet.transform.GetChild(0).GetChild(SecStreet.transform.GetChild(0).childCount - 1).position;
        
        currentStreet = LastStreet;

       // for(byte i = 0; i < 2; i++)
        AddCars(0);
    }

    public void BuildMap(GameObject street)
    {
        LastStreet = street;
        LastStreet.transform.position = currentStreet.transform.GetChild(0).GetChild(currentStreet.transform.GetChild(0).childCount - 1).position;
        Debug.Log(currentStreet.transform.GetChild(0).GetChild(currentStreet.transform.GetChild(0).childCount - 1).name);

        currentStreet = LastStreet;
    }

    private void AddCars(byte L)
    {

        List<GameObject> carsList = new List<GameObject>();
        Vector3 ParkingPos = new Vector3(6, 0, 0);

        byte n = 0;

        for (; n < 9; n++)
        {
            GameObject car1 = Instantiate(TrafficCarsPrefab[0], CarsParent);
            car1.transform.localPosition = ParkingPos * n;
            car1.transform.localPosition += new Vector3(0, 0, 200);
            car1.GetComponent<TrafficCar>().ID = L;
            carsList.Add(car1);
        }
        for (; n < 19; n++)
        {
            GameObject car2 = Instantiate(TrafficCarsPrefab[1], CarsParent);
            car2.transform.localPosition = ParkingPos * n;
            car2.transform.localPosition += new Vector3(0, 0, 200);
            car2.GetComponent<TrafficCar>().ID = L;
            carsList.Add(car2);
        }
        for (; n < 28; n++)
        {
            GameObject car3 = Instantiate(TrafficCarsPrefab[0], CarsParent);
            car3.transform.localPosition = ParkingPos * n;
            car3.transform.localPosition += new Vector3(0, 0, 200);
            car3.GetComponent<TrafficCar>().ID = L;
            carsList.Add(car3);
        }
        for (; n < 50; n++)
        {
            GameObject car4 = Instantiate(TrafficCarsPrefab[0], CarsParent);
            car4.transform.localPosition = ParkingPos * n;
            car4.transform.localPosition += new Vector3(0, 0, 200);
            car4.GetComponent<TrafficCar>().ID = L;
            carsList.Add(car4);
        }


        /*for(byte i = 0; i < carsList.Count; i++)
            carsList[i].transform.SetParent(CarsParent.GetChild(L));
        */
          CarsParent.GetComponent<TrafficGeneratorSpawn>().SetTrafficCars(carsList);

    }
  
}
