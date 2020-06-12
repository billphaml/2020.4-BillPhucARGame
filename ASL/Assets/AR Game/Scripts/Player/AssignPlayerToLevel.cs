/*
 ******************************************************************************
 * AssignPlayerToLevel.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Parents the player object to the level object when the level is spawned in
 * so that they move as one entity with ASL updates.
 ******************************************************************************
*/

using UnityEngine;

public class AssignPlayerToLevel : MonoBehaviour
{
    // Reference to parent which should be the level
    private GameObject parent;

    void Update()
    {
        // Searches the object hierarchy for the level
        if (parent == null)
        {
            parent = GameObject.Find("Level(Clone)");
        }

        // Sets the parent for the player
        if (transform.parent == null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent.transform, false);
            }
        }
    }
}
