using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Use only for Level prefab
// Moves the level to where ever the user taps
public class MoveOnTap : MonoBehaviour
{
    void Update()
    {
        if(!GameVariables.isRayCasting)
        return;
        if (Application.platform == RuntimePlatform.Android)
        {
            // Gets the touch position
            Pose? touchPose = GetTouch();

            // Updates level to touch position
            gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                gameObject.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition((Vector3)touchPose?.position);
            });
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

// Custom variant that has smoothed placement
// If possible integrate with ASL
/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Use only for Level prefab
// Moves the level to whereever the user taps
public class MoveOnTap : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPos;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        // Stores a reference to this object so spawnlevel.cs can check if a level exists
        ReferenceManager.Instance.myRef1 = gameObject;
    }

    bool TryGetTouchPos(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    void Update()
    {
        // Acquires a reference for the raycast manager from the ref manager.
        if (_arRaycastManager == null)
        {
            _arRaycastManager = ReferenceManager.Instance.myRef2.GetComponent<ARRaycastManager>();
        }

        if (!TryGetTouchPos(out Vector2 touchPos))
            return;

        if (_arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                gameObject.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(hitPose.position);
            });
        }
    }
}
 */
