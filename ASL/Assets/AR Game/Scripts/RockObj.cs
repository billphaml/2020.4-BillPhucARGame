using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObj : MonoBehaviour
{
    public float moveSpeed = 0.0002f;
    private Vector3 oldLoc;
    private Vector3 newLoc;
    private float offsetX=0.0f;
    private float offsetY=0.0f;
    private bool isDragging = false;
    private bool isSelected = false;

    
    // On touch
    void OnMouseDown()
    {
        newLoc=oldLoc=Input.mousePosition;
        GameVariables.debugStr="OnMouseDown";
        isDragging = true;
        isSelected = true;

    }
    
    // On drag
    void OnMouseDrag()
    {
        isDragging = true;
        newLoc=Input.mousePosition;

    }
    
    // On release
    void OnMouseUp()
    {
        isDragging = false;
        oldLoc=newLoc;

    }
    
    void Update(){
        float totalX = 0.0f;
        float totalY=0.0f;
        if(!isDragging)
        {
            offsetX=0;
            offsetY=0;
            totalX=0.0f;
            totalY=0.0f;
        }else{
        offsetX = newLoc.x-oldLoc.x;
        offsetY = newLoc.y-oldLoc.y;
        totalX= offsetX * moveSpeed + Input.GetAxis("Mouse X") * moveSpeed;// X Dir
        totalY= offsetY * moveSpeed + Input.GetAxis("Mouse Z") * moveSpeed;// Z Dir
        }
        
        gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            float[] direction = new float[]
            {
                totalX, // X Dir
                0.0f,                                                                      // Y Dir
                totalY,     // Z Dir
                0.0f    // Unused
            };
            gameObject.GetComponent<ASL.ASLObject>().SendFloatArray(direction);

        });
        offsetX=0.0f;
        offsetY=0.0f;
        
        /// Check if the rock is selected to be removed
        toRemove();
    }
    
    /// Remove rock
    void toRemove(){
        if(isSelected &&  GameVariables.isRemoveObject)
        {
            gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                gameObject.GetComponent<ASL.ASLObject>().DeleteObject();
                GameVariables.isRemoveObject=false;
                isSelected = false;

            });
        }
    }
    
}
