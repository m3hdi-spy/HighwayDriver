using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private float ScoreRatio = 0.3f;
    private static float score = 0;
    public static float Score
    {
        get { return score; }
    }
    private static float StreetSpeed;

    private CarMovement theCar;

    public byte LevelDifficultly
    {
        get { return (byte)PlayerPrefs.GetInt("Leveldifficultly"); }
    }
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        theCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
        TrafficGeneratorController.Instance.GenerateNewTrafficCars();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float StreetSpeed = Street.GetMovingSpeed;
        score += StreetSpeed * ScoreRatio;

    }



    private void OnGUI()
    {
      //  GUI.TextArea(new Rect(new Vector2(20, 150), new Vector2(100, 35)), score.ToString("0"));
    }
}
