using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Street")
        {
            TrafficGeneratorController.Instance.GenerateNewTrafficCars();
          GameObject.Find("StreetsParent").GetComponent<MapGenerator>().BuildMap(other.gameObject);
        }
        else if(other.gameObject.tag == "TrafficCar")
        {
            GameObject car = other.gameObject;
            TrafficCar trCar = car.GetComponent<TrafficCar>();
            byte par = trCar.ID;
            car.transform.GetComponentInParent<TrafficGeneratorSpawn>().Cars.Add(car);
            trCar.enabled = false;
        }
    }
}
