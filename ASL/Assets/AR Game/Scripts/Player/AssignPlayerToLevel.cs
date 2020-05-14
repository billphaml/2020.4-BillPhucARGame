using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parents the player object to the level object when the level is spawned in
// so that they move as one entity
public class AssignPlayerToLevel : MonoBehaviour
{
    private GameObject parent;

    // Update is called once per frame
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
            if(parent!=null)
            gameObject.transform.SetParent(parent.transform, false);
        }
    }
}
