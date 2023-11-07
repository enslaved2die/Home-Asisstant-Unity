using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Limiter : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 30;
    }
}
