using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour
{
    public Button addObjButton;
    public Button removeObjButton;
    public Button startButton;
    public Button popoutButton;
    public Button rockButton;
    public Button spell1Button;

    public delegate void OnButtonClickDelegate ();
    public static OnButtonClickDelegate popClickDelegate;
    public static OnButtonClickDelegate rockClickDelegate;
    public static OnButtonClickDelegate removeClickDelegate;
    public static OnButtonClickDelegate resetClickDelegate;


    // Start is called before the first frame update
    void Start()
    {
        // Add listener
        Button btnAdd = addObjButton.GetComponent<Button>();
        btnAdd.onClick.AddListener(clickAdd);
        
        Button btnRemove = removeObjButton.GetComponent<Button>();
        btnRemove.onClick.AddListener(clickRemove);
        
        Button btnStart = startButton.GetComponent<Button>();
        btnStart.onClick.AddListener(clickStart);
        
        Button btnRock = rockButton.GetComponent<Button>();
        btnRock.onClick.AddListener(clickRock);
        
        Button btnSpell1 = spell1Button.GetComponent<Button>();
        btnSpell1.onClick.AddListener(clickSpell1);

        Button btnPop = popoutButton.GetComponent<Button>();
        btnPop.onClick.AddListener(clickPopout);


    }
    
    void clickAdd() {
        popClickDelegate();
    }
    
    void clickRemove(){
//        GameVariables.isRemoveObject = true;
        removeClickDelegate();
    }
    
    void clickStart(){
        GameVariables.gameStarted = !GameVariables.gameStarted;
        if(!GameVariables.gameStarted)
        resetClickDelegate();
    }

    void clickRock(){
        GameVariables.spellSelected = 0;
    }
    
    void clickPopout(){
        popClickDelegate();
    }
    
    void clickSpell1(){
        GameVariables.spellSelected = 1;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(GameVariables.gameStarted)
        startButton.GetComponentInChildren<Text>().text = "Reset";
        else
        {
        startButton.GetComponentInChildren<Text>().text = "Start";
        GameVariables.collectCoins = 0;
        }
    }
}
