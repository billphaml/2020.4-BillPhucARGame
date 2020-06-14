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

    // Reference to controller so that we can send floats to update game state
    public GameObject controller;

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
            controller.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                // Var to store the state of the game as 0 (false), 1 (true)
                float gameState = 0;

                // Send the new game state to all clients
                float[] gameStateArray = new float[1];
                gameStateArray[0] = gameState;
                controller.GetComponent<ASL.ASLObject>().SendFloatArray(gameStateArray);
            });
        }

        timeRemainingInSeconds -= Time.deltaTime;
        lbl.text = "Time: " + Mathf.Round(timeRemainingInSeconds) + "s";
    }
}
