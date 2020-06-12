/*
 ******************************************************************************
 * Timer.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Counts down the time players have to collect coins, stops the game when time
 * is out.
 ******************************************************************************
*/

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Reference to time label
    public Text lbl;

    public float timeRemainingInSeconds = 120;

    // Update is called once per frame
    void Update()
    {
        if (!GameVariables.gameStarted)
        {
            timeRemainingInSeconds = 120;

            lbl.text = "Time: " + Mathf.Round(timeRemainingInSeconds) + "s";

            return;
        }

        // End the game once time is zero
        if (timeRemainingInSeconds <= 0)
        {
            GameVariables.gameStarted = false;
        }

        timeRemainingInSeconds -= Time.deltaTime;
        lbl.text = "Time: " + Mathf.Round(timeRemainingInSeconds) + "s";
    }
}
