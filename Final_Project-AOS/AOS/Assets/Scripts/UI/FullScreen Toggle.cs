using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenToggle : MonoBehaviour
{
    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
