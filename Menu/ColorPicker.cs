using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public bool isBrighnessActive;
    public Vector3[,] ColorsBrightness = new Vector3[10, 5] { {new Vector3(0, 0, 0.588f), new Vector3(0, 0.2f, 1), new Vector3(0.2745f, 0.2745f, 1), new Vector3(0.51f, 0.51f, 1), new Vector3(0.3137f, 0.3137f, 0.588f) },//
        {new Vector3(0.5882f, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0.3137f, 0.3137f), new Vector3(1, 0.5882f, 0.5882f), new Vector3(0.5882f, 0.3137f, 0.3137f) },//red
        { new Vector3(0.4716f, 0.4716f, 0), new Vector3(0.7452f, 0.7452f, 0), new Vector3(1, 0.8628f, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0.6556f) },//yellow

        { new Vector3(0, 0, 0), new Vector3(0.196f, 0.196f, 0.196f), new Vector3(0.4705f, 0.4705f, 0.4705f), new Vector3(0.745f, 0.745f, 0.745f), new Vector3(1, 1, 1) },//black

        {  new Vector3(0, 0.4313f, 0), new Vector3(0, 1, 0), new Vector3(0.3921f, 1, 0.3921f), new Vector3(0.6274f, 1, 0.6274f), new Vector3(0.3007f, 0.5849f, 0.3007f) },//green

        { new Vector3(1, 0, 1), new Vector3(1, 0.3921f, 1), new Vector3(1, 0.6862f, 1), new Vector3(0.7843f, 0.196f, 0.7843f), new Vector3(0.5098f, 0, 0.5098f) },//pink

        { new Vector3(1, 0.6274f, 0), new Vector3(1, 0.549f, 0), new Vector3(1, 0.7058f, 0.2549f), new Vector3(0.549f, 0.3529f, 0), new Vector3(0.6666f, 0.5098f, 0.196f) },//orang

        { new Vector3(0, 1, 0.7843f), new Vector3(0, 0.7058f, 0.5882f), new Vector3(0.3529f, 1, 0.8431f), new Vector3(0.2941f, 0.7843f, 0.7058f), new Vector3(0.6666f, 1, 0.9411f) }, // blue-green

        { new Vector3(0, 1, 1), new Vector3(0.3921f, 1, 1), new Vector3(0.7058f, 1, 1), new Vector3(0.1568f, 0.7254f, 0.7058f), new Vector3(0, 0.549f, 0.549f) }, // cyen

        { new Vector3(0.8235f, 1, 0), new Vector3(0.9019f, 1, 0.4313f), new Vector3(0.9607f, 1, 0.745f), new Vector3(0.6666f, 0.7058f, 0.0784f), new Vector3(0.4901f, 0.549f, 0.2745f) }, // lemon yellow
    };

    public Vector3[] ShowColorBrightness(byte baseColor)
    {
        Vector3[] cbr = new Vector3[5];
        for (byte i = 0; i < 5; i++)
            cbr[i] = ColorsBrightness[baseColor, i];
        return cbr;
    }


    
}
