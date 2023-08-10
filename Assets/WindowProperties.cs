using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowProperties : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionMenu;

    private int[,] resolutions = {
        {3840, 2160},
        {2560, 1440},
        {1920, 1080},
        {854, 480},
        {640, 360},
        {426, 240},
    };

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution()
    {
        int resolutionIndex = resolutionMenu.value;
        int x = resolutions[resolutionIndex, 0];
        int y = resolutions[resolutionIndex, 1];
        Screen.SetResolution(x, y, true);
    }
}
