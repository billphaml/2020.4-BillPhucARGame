﻿/*
 ******************************************************************************
 * TurnPCCamOff.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Turns the PC camera off when on Android client so only one main camera
 * exists
 ******************************************************************************
*/

using UnityEngine;

public class TurnPCCamOff : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
