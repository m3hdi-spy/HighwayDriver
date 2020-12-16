using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private byte LevelMode;
    private byte enviromentNumber;

    private bool gameStart;
    public bool GameStart
    {
        get { return gameStart; }
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public GameUIManager scriptGUI;
    void Awake()
    {
        instance = this;
        gameStart = false;
       // scriptGUI.StartLoading();
    }
    void Start()
    {
        LevelMode = (byte)PlayerPrefs.GetInt("GameMode");
        enviromentNumber = (byte)PlayerPrefs.GetInt("CurrentEnviroment");

        LoadLevel.Instance.StartLoading(0, 0);
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
