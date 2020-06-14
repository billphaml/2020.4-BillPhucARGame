/*
 ******************************************************************************
 * HandleClick.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Controller for UI clicks. Handles updating of game state.
 * 
 * Notes:
 * Start button doubles as reset button
 ******************************************************************************
*/

using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour
{
    //public Button addObjButton;
    //public Button removeObjButton;
    public Button startButton;
    //public Button popoutButton;

    public delegate void OnButtonClickDelegate ();
    //public static OnButtonClickDelegate popClickDelegate;
    //public static OnButtonClickDelegate rockClickDelegate;
    //public static OnButtonClickDelegate removeClickDelegate;
    public static OnButtonClickDelegate resetClickDelegate;

    // Reference to controller so that it can receive ASL floats
    private GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        Button btnStart = startButton.GetComponent<Button>();
        btnStart.onClick.AddListener(clickStart);

        controller = gameObject;

        gameObject.GetComponent<ASL.ASLObject>()._LocallySetFloatCallback(ChangeGameState);

        // Add listener
        //Button btnAdd = addObjButton.GetComponent<Button>();
        //btnAdd.onClick.AddListener(clickAdd);

        //Button btnRemove = removeObjButton.GetComponent<Button>();
        //btnRemove.onClick.AddListener(clickRemove);

        //Button btnPop = popoutButton.GetComponent<Button>();
        //btnPop.onClick.AddListener(clickPopout);
    }

    void Update()
    {
        if (GameVariables.gameStarted)
        {
            startButton.GetComponentInChildren<Text>().text = "Reset";
        }
        else
        {
            startButton.GetComponentInChildren<Text>().text = "Start";
        }
    }

    //void clickAdd() {
    //    popClickDelegate();
    //}

    //void clickRemove()
    //{
    //    //        GameVariables.isRemoveObject = true;
    //    removeClickDelegate();
    //}

    void clickStart()
    {
        controller.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            // Var to store the state of the game as 0 (false), 1 (true)
            float gameState;

            // Sets game state to false if currently true and vice versa
            if (GameVariables.gameStarted)
            {
                gameState = 0;
            }
            else
            {
                gameState = 1;
            }

            // Send the new game state to all clients
            float[] gameStateArray = new float[1];
            gameStateArray[0] = gameState;
            controller.GetComponent<ASL.ASLObject>().SendFloatArray(gameStateArray);
        });
    }

    //void clickRock(){
    //    GameVariables.spellSelected = 0;
    //}
    
    //void clickPopout(){
    //    popClickDelegate();
    //}

    public void ChangeGameState(string _id, float[] f)
    {
        // Ends the round if 0 (false), and vice versa
        if (f[0] == 0)
        {
            GameVariables.gameStarted = false;
        }
        else
        {
            GameVariables.gameStarted = true;
        }

        if (!GameVariables.gameStarted)
        {
            resetClickDelegate();
        }
    }
}
