using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour
{
    public Button addObjButton;
    public Button removeObjButton;
    public Button startButton;

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
    }
    
    void clickAdd(){
        GameVariables.isAddObject = true;
    }
    
    void clickRemove(){
        GameVariables.isRemoveObject = true;
    }
    
    void clickStart(){
        GameVariables.gameStarted = !GameVariables.gameStarted;
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
