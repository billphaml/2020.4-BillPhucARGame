using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour
{
    public Button addObjButton;
    
    // Start is called before the first frame update
    void Start()
    {
        Button btn = addObjButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
    }
    void TaskOnClick(){
        GameVariables.isAddObject = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
