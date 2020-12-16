using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Car : MonoBehaviour
{
    public byte ID;
    public string Name;
    //name
    //weight
    //driftratio : tires 
    //steering
    //gas
    //brake

    [Header("Motor")]
    public ushort MaxTorque = 157;
    public const float ConstCarSpeed = 1.3f;
    public ushort HorsePower;

    [Header("GearBox")]
    public byte MaxGear = 4;
    public List<byte> SpeedsOfGears = new List<byte> {30, 45, 65, 90, 120}; //[0]: Rear Gear
    public List<float> SpeedRatio = new List<float> {7f, 8f, 7f, 6f, 4f};

   
    public float GearupCooldown = 1.3f;
    
    

    [Header("Steering")]
    public float SteerPower = 3;
    public float _rearTrack;
    public float _turnRadius;

    [Header("Brake")]
    public ushort BrakePower = 20;


    [Header("Colors")]
    public bool[] BaseColors = new bool[10];
    public bool[,] ColorBrighness = new bool[10, 5];
    
}
