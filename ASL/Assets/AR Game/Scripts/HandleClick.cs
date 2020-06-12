/*
 ******************************************************************************
 * HandleClick.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Controller for UI clicks
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


    // Start is called before the first frame update
    void Start()
    {
        Button btnStart = startButton.GetComponent<Button>();
        btnStart.onClick.AddListener(clickStart);

        // Add listener
        //Button btnAdd = addObjButton.GetComponent<Button>();
        //btnAdd.onClick.AddListener(clickAdd);

        //Button btnRemove = removeObjButton.GetComponent<Button>();
        //btnRemove.onClick.AddListener(clickRemove);

        //Button btnPop = popoutButton.GetComponent<Button>();
        //btnPop.onClick.AddListener(clickPopout);
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
        // Set game status to true if not
        GameVariables.gameStarted = true;

        // Reset player coins
        GameVariables.collectCoins = 0;

        if (!GameVariables.gameStarted)
        {
            resetClickDelegate();
        }
    }

    //void clickRock(){
    //    GameVariables.spellSelected = 0;
    //}
    
    //void clickPopout(){
    //    popClickDelegate();
    //}
    
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
}
