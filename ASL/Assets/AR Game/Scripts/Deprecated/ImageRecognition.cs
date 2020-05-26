using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ImageRecognition : MonoBehaviour
{
    public GameObject prefab;

    // Reference to image manager
    private ARTrackedImageManager _arTrackedImageManager;

    // Custom prefab
    private Vector3 objPos;

    private Button _spawnObj;

    // variable to know when to instantiate asl object into level
    private bool isCopyingObject = false;

    // Finds image manager on start
    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        _spawnObj = FindObjectOfType<Button>();
        _spawnObj.onClick.AddListener(copyObject);
    }

    // when the tracked image manager is enabled add binding to the tracked 
    // image changed event handler by calling a method to iterate through 
    // image reference’s changes
    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    // when the tracked image manager is disabled remove binding to the 
    // tracked image changed event handler by calling a method to iterate //through image reference’s changes
    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    // Do an action based on if an image is detected or not
    // method to iterate tracked image changed event handler arguments
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            // SpawnObject(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            UpdateObject(trackedImage);

            // If a UI button is press then copy the object into the ASL Level
            if (isCopyingObject == true)
            {
                // In the future, only allow for spawning after a level has been generated
                spawnObjectIntoLevel(trackedImage);
                isCopyingObject = false;
            }
        }

        // Deletes the object if the object isn't in the FOV, different from not in render range (update function)
        foreach (var trackedImage in args.removed)
        {
            Destroy(trackedImage.gameObject);
        }
    }

    // When called, spawns a gameobject copy into the level
    public void spawnObjectIntoLevel(ARTrackedImage trackedImage)
    {
        Vector3 objPosTemp = new Vector3(trackedImage.transform.position.x * 20, 0.0f, trackedImage.transform.position.z * 20);
        ASL.ASLHelper.InstanitateASLObject("Obstacle_Rock_Variant", objPosTemp, Quaternion.identity, GameObject.Find("Level(Clone)").GetComponent<ASL.ASLObject>().m_Id);
    }

    // Toggles isCopyingObject to true when a UI button is clicked to let the 
    // update know to copy the object into the ASL level
    public void copyObject()
    {
        if (isCopyingObject == false)
        {
            isCopyingObject = true;
        }
    }

    // Custom prefab spawn, remember to remove the prefab from the manager before use or two objects will be spawned
    void SpawnObject(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState != TrackingState.Tracking)
        {
            if (trackedImage.referenceImage.name == "rock")
            {
                Instantiate(prefab, transform.position, transform.rotation);
            }
        }
    }

    // Hides the image if not in render difference
    void UpdateObject(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedImage.gameObject.SetActive(true);
        }
        else
        {
            trackedImage.gameObject.SetActive(false);
        }
    }
}
