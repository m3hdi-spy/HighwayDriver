using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGeneratorController : MonoBehaviour
{
    private static TrafficGeneratorController instance;
    public static TrafficGeneratorController Instance
    {
        get { return instance; }
    }



    public byte LastFreeLane;

    public byte MaxCarsInColumn = 5;
    public byte MinCarsInColumn = 2;

    protected byte GameMode;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameMode = (byte)PlayerPrefs.GetInt("GameMode");
        GameMode = 1;

    }
    // Update is called once per frame
    public void GenerateNewTrafficCars()
    {
        gameObject.GetComponent<TrafficGeneratorFormul>().StartGenerate(10);
    }

    public void CalculateHardnessCurve()
    {
        uint score = (ushort)LevelManager.Score;
        float scoreRate = 0;
        if (score > 1000 && score < 10000)
        {
            scoreRate = score / 10;
        }
        else if(score > 10000 && score < 100000)
        {
            scoreRate = score / 100;
        }
        
        if (scoreRate == 0)
        {
            scoreRate = score;
        }
        if (score >= 0 && score < 200)
        {
            MaxCarsInColumn = 2;
            MinCarsInColumn = 0;
        }

        if (scoreRate > 0 && scoreRate < 200)
        {
            MaxCarsInColumn = 3;
            MinCarsInColumn = 1;
        }
        else if(scoreRate >= 200 && scoreRate < 350)
        {
            MaxCarsInColumn = 3;
            MinCarsInColumn = 2;
        }
        else if(scoreRate >= 350 && scoreRate < 500)
        {
            MaxCarsInColumn = 4;
            MinCarsInColumn = 1;
        }
        else if(scoreRate >= 500 && scoreRate < 650)
        {
            MaxCarsInColumn = 4;
            MinCarsInColumn = 2;
        }
        else if(scoreRate >= 800 && scoreRate < 800) // High Density
        {
            MaxCarsInColumn = 5;
            MinCarsInColumn = 3;
        }
        else if(scoreRate >= 800 && scoreRate < 920)
        {
            MaxCarsInColumn = 4;
            MinCarsInColumn = 1;
        }
        else if(scoreRate >= 920 && scoreRate <= 1000)
        {
            MaxCarsInColumn = 4;
            MinCarsInColumn = 2;
        }
        
    }

    public void RestartGame()
    {
        
    }
}
