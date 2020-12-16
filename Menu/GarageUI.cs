using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GarageUI : MonoBehaviour
{
    //----- Prent

    public ToggleGroup tglGpShoping;
    public GameObject gmStartButton;
    public GameObject gmBuyStuffButton;
    public GameObject gmBackMenu;

    //----- Colors
    [Header("Change Color")]
    public GameObject gmColorPickerParent;
    public ToggleGroup tglGroupColors;
    public Toggle[] tglColors = new Toggle[10];
    public Toggle[] tglBrightness = new Toggle[5];
    public Image[] imgColorBrightness = new Image[5];
    public Transform colorBrightnessParent;
    private byte BasicColorClicked;
    private byte BrighnessColorclicked;
    public bool[] AvalableColors = new bool[10];
    public bool[,] AvalableBrighnessOgColors = new bool[10, 5];


    //----- Paint
    [Header("Paints")]
    public GameObject gmPaintPickerParent;
    public Toggle[] tglPaints;
    public bool[] AvalablePaints = new bool[5];


    //---- Wheels
    [Header("Wheels")]
    public GameObject gmWheelsPickerParent;
    public Toggle[] tglWheels;

    // Start is called before the first frame update
    void Start()
    {
        Chemifahmom();
    }
    public void Chemifahmom()
    {
        AvalableColors = new bool[10] { true, true, false, false, false, false, true, true, true, false };
        AvalableBrighnessOgColors = new bool[10, 5] { { true, true, false, false, true}, { true, true, false, false, true }, { true, true, false, false, true }, { false, false, true, false, true }, { true, true, false, false, true }, { true, true, false, false, true },
        { true, true, false, false, true}, { true, true, false, false, true}, { true, true, false, false, true}, { true, true, false, false, true}};

        AvalablePaints = new bool[5] { true, false, false, false, false };

        for(byte i = 0; i < 10; i++)
        {
            if(AvalableColors[i])
                tglColors[i].interactable = true;

            if(i < AvalablePaints.Length)
            if (AvalablePaints[i])
                tglPaints[i].interactable = true;
        }
    }
    //======================[ On Click ]====================//
    public void OnClick_NextCar()
    {

    }

    public void OnClick_PrevioudsCar()
    {

    }
    //-------------[Update Vehicle]-----------------//
    public void OnClick_UpdateMenu()
    {

    }

    public void OnClick_UpdateEngine()
    {

    }
    public void OnClick_UpdateBrake()
    {

    }
    public void OnClick_UpdateSteering()
    {

    }
    //-------------------[Color]--------------------//
    public void OnClick_ChangeColor()
    {
        Input.multiTouchEnabled = false;
        
        if (gmColorPickerParent.activeInHierarchy)
        {
            colorBrightnessParent.GetComponent<ToggleGroup>().SetAllTogglesOff();
            tglColors[0].GetComponentInParent<ToggleGroup>().SetAllTogglesOff();
            colorBrightnessParent.gameObject.SetActive(false);
            gmColorPickerParent.GetComponentInChildren<ScrollRect>().horizontalNormalizedPosition = 0;
            gmColorPickerParent.SetActive(false);
        }
        else
            gmColorPickerParent.SetActive(true);
    }
    public void OnClick_ToggleColor()
    {
        tglGroupColors.SetAllTogglesOff();
        for (byte i = 0; i < 10; i++)
            if (tglColors[i].isOn)
            {
                BasicColorClicked = i;
               ShowBrighnessOfColor(tglColors[i].GetComponentInParent<ColorPicker>().ShowColorBrightness(i));
                colorBrightnessParent.gameObject.SetActive(true);
                colorBrightnessParent.SetParent(tglColors[i].transform.GetChild(1));
                colorBrightnessParent.localPosition = Vector3.zero;
               break;
            }
        
    }
    private void ShowBrighnessOfColor(Vector3[] colorBright)
    {
        Color bColor;
        for(byte i = 0; i < 5; i++)
        {
            bColor.r = colorBright[i].x;
            bColor.g = colorBright[i].y;
            bColor.b = colorBright[i].z;
            bColor.a = 1;
            imgColorBrightness[i].color = bColor;
        }
    }

    public void OnClick_NewColor()
    {
        for(byte i =0; i < 5; i++)
        {
            if(tglBrightness[i].isOn)
            {
                if(AvalableBrighnessOgColors[BasicColorClicked, i])
                {
                    // Add Material to car
                }
                else
                {
                    gmStartButton.SetActive(false);
                    gmBuyStuffButton.SetActive(true);
                    //Change stuff image to price of item
                }
            }
        }
    }
    public void OnClick_BuyNewColor() // Send to shoping c#
    {

    }
    //-------------------[Texture Paint]--------------------//
    public void OnClick_OpenPaintList()
    {
        if (gmPaintPickerParent.activeInHierarchy)
        {
            gmPaintPickerParent.SetActive(false);
        }
        else
            gmPaintPickerParent.SetActive(true);
    }
    public void OnClick_Paint()
    {
        for(byte i = 0; i < tglPaints.Length; i++)
        {
            if(tglPaints[i].isOn)
            {
                if(AvalablePaints[i])
                {
                    // Add Texture to car
                }
                else // Not Bought
                {
                    gmStartButton.SetActive(false);
                    gmBuyStuffButton.SetActive(true);
                    //Change stuff image to price of item
                }
            }
        }
    }
    public void OnClick_BuyPaint()// Send to shoping c#
    {

    }
    //-------------------[Update Tiers]--------------------//
    public void OnClick_OpenRingsMenu()
    {

    }
    public void OnClick_ChooseRing()
    {

    }
    public void OnClick_BuyNewRing()
    {

    }
    
    //---------------[Update Tires]-----------------//
    public void OnClick_BuyCar()
    {

    }



    //--------------- Functional
    private void CloseAll()
    {
        
        if (gmColorPickerParent.activeInHierarchy)
        {
            colorBrightnessParent.GetComponent<ToggleGroup>().SetAllTogglesOff();
            tglColors[0].GetComponentInParent<ToggleGroup>().SetAllTogglesOff();
            colorBrightnessParent.gameObject.SetActive(false);
            gmColorPickerParent.GetComponentInChildren<ScrollRect>().horizontalNormalizedPosition = 0;
            gmColorPickerParent.SetActive(false);
        }
        else if(gmPaintPickerParent.activeInHierarchy)
        {

        }
        
       
    }
}
