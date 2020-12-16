using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrafficCar"))
        {
            GameUIManager.Instance.LoseGame();
        }
        else if (other.CompareTag("NearbyTC"))
        {
            Debug.Log("Combo");
        }
    }
}
