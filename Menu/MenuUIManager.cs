using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public ToggleGroup tglModes;
    public ToggleGroup tglEnviroments;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_Play()
    {

    }
    public void OnClick_StartGame()
    {
        
        for (byte i = 0; i < tglModes.transform.childCount; i++)
            if (tglModes.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                PlayerPrefs.SetInt("GameMode", i+1);
                break;
            }
        for (byte i = 0; i < tglEnviroments.transform.childCount; i++)
            if (tglEnviroments.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                PlayerPrefs.SetInt("CurrentEnviroment", i);
                break;
            }

       // Debug.Log(PlayerPrefs.GetInt("CurrentMode") + " en: " + PlayerPrefs.GetInt("CurrentEnviroment"));
        SceneManager.LoadScene("Game");
    }

}
