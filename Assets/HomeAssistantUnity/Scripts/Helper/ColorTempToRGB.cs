using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ColorTempToRGB : MonoBehaviour
{
    
    [Range(1000, 8000)]
    public int colorTemp;

    public Image image;
    void Start()
    {
        GetComponent<Image>().color = Whitepoint.Tools.Conversions.colorTempToRGB(GetComponent<LightEntity>().kelvin);
    }
}
