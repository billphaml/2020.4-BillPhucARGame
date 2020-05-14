using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour
{
    public Button addObjButton;
    public Button removeObjButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listener
        Button btnAdd = addObjButton.GetComponent<Button>();
        btnAdd.onClick.AddListener(clickAdd);
        
        Button btnRemove = removeObjButton.GetComponent<Button>();
        btnRemove.onClick.AddListener(clickRemove);
    }
    
    void clickAdd(){
        GameVariables.isAddObject = true;
    }
    
    void clickRemove(){
        GameVariables.isRemoveObject = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
