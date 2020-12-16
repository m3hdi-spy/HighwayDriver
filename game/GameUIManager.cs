using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
public class GameUIManager : MonoBehaviour
{
    public CinemachineVirtualCamera cinema;
    public GameObject LoadingPanel;

    public GameObject pnlLose;

    [SerializeField]
    private bool bChangeFOV;
    private float DeadFOV = 30;


    //-------- UI Elements
    public Text txtScore;
    public Text txtCombo;

    public Text txtSpeed;

    public GameObject Player;
    private static GameUIManager instance;
    public static GameUIManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
        cinema = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        txtScore.text = "Score: " + System.Convert.ToUInt32(LevelManager.Score);
        txtSpeed.text = System.Convert.ToInt16(CarMovement.CarSpeed) + " km/h";

        if (bChangeFOV)
        {
            cinema.m_Lens.FieldOfView = Mathf.Lerp(cinema.m_Lens.FieldOfView, DeadFOV, Time.deltaTime);
            Debug.Log(CarMovement.CarSpeed);
            if (cinema.m_Lens.FieldOfView < DeadFOV + 1)
                bChangeFOV = false;
        }
    }

    public void OnClick_RestartGame()
    {
        SceneManager.LoadScene("Game");
       
        // After complete codes. you have to call GameManager.StartGame(); Function to get ID's. ****
    }

    public void LoseGame()
    {
        pnlLose.SetActive(true);
        bChangeFOV = true;
        Player.GetComponent<CarMovement>().bSteering = false;
        Player.GetComponent<ECU>().LoseGame();
        Player.GetComponent<ECU>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;

    }
    public void StartLoading()
    {
        LoadingPanel.SetActive(true);
    }
    public void StopLoading()
    {
        LoadingPanel.SetActive(false);
    }
}
