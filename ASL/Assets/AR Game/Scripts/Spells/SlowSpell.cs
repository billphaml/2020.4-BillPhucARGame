using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlowSpell : MonoBehaviour
{
    private Pose pose;

    private bool fireSpell = true;

    // Update is called once per frame
    void Update()
    {
        Pose? touchPose = GetTouch();

        if (touchPose == null) //If we didn't hit anything - return
        {
            return;
        }

        pose = (Pose) touchPose;

        if (fireSpell == true)
        {
            ASL.ASLHelper.InstanitateASLObject("Slow Spell", pose.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Gets the location of the user's touch
    /// </summary>
    /// <returns>Returns null if UI or nothing touched</returns>
    private Pose? GetTouch()
    {
        Touch touch;
        // If the player has not touched the screen then the update is complete.
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return null;
        }

        // Ignore the touch if it's pointing on UI objects.
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return null;
        }

        return ASL.ARWorldOriginHelper.GetInstance().Raycast(Input.GetTouch(0).position);
    }
}
