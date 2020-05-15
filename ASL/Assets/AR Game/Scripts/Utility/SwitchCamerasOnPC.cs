using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Turns off the AR camera if using a windows or osx client in order to use pc camera
public class SwitchCamerasOnPC : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
