using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

// When user clicks a ui element or triggers this script, a copy of this object
// will be made and its position will be set the this position and attached
// to the level object
// Note: for a copy to be made a copy of this object must exist under
//          Assets/Resources/MyPrefabs
public class ImageObjectAttach : MonoBehaviour
{
    private Vector3 objPosition;

    // Reference to image manager
    private ARTrackedImageManager _arTrackedImageManager;

    void Update()
    {
        if (_arTrackedImageManager == null)
        {
            _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }

        if (_arTrackedImageManager != null)
        {
            objPosition = new Vector3(_arTrackedImageManager.trackedImagePrefab.transform.position.x, 0.0f, _arTrackedImageManager.trackedImagePrefab.transform.position.z);
        }
    }

    // When called, spawns a gameobject copy into the level
    public void spawnObjectIntoLevel()
    {
        if (_arTrackedImageManager != null)
        {
            ASL.ASLHelper.InstanitateASLObject("Obstacle_Rock_Variant", objPosition, Quaternion.identity, GameObject.Find("Level(Clone)").GetComponent<ASL.ASLObject>().m_Id);
        }
    }
}