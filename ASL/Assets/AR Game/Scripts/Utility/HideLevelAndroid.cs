using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLevelAndroid : MonoBehaviour
{
    public GameObject levelBase;
    public GameObject levelUnder;

    private bool isVisable;

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            isVisable = true;
        }
        else
        {
            isVisable = false;
        }
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        if (isVisable == true)
        {
            levelBase.GetComponent<Renderer>().enabled = true;
            levelUnder.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            levelBase.GetComponent<Renderer>().enabled = false;
            levelUnder.GetComponent<Renderer>().enabled = false;
        }
    }
}
