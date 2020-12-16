using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private byte modenum;
    private byte enviromentnum;

    public GameObject[] MapsPrefab;
    public GameObject[] EnviromentsPrefab;

    public Transform MapParent;

    private static LoadLevel instance;
    public static LoadLevel Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void StartLoading(byte ModeNumber, byte EnviromentNumber)
    {
        modenum = ModeNumber;
        enviromentnum = EnviromentNumber;

        LoadMap();
    }
    private void LoadMap()
    {
        MapGenerator.Instance.StartGenerate(modenum);
    }
   
    private void LoadCar()
    {

    }

    private void LoadStreetCar()
    {

    }

    //-----------------

}
