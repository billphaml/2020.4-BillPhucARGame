using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text lbl;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameVariables.gameStarted)
        {
            seconds=0;
            lbl.text="Time: "+seconds/60+"s";
            return;
        }
        seconds++;
        lbl.text="Time: "+seconds/60+"s";
    }
}
