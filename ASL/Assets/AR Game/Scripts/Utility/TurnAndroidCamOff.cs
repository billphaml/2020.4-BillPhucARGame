/*
 ******************************************************************************
 * TurnAndroidCamOff.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Turns the Android camera off when on non-Android client so only one main
 * camera exists
 ******************************************************************************
*/

using UnityEngine;

public class TurnAndroidCamOff : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
