/*
 ******************************************************************************
 * CameraController.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Script to allow movement of the camera around on the PC platform. Supports
 * a free cam mode and a fixed offset mode toggable by UI.
 ******************************************************************************
*/

using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target to lock the camera to in fixed offset mode
    [SerializeField]
    private Transform lookAt = default;

    // Toggle for free cam and fixed offset mode
    [SerializeField]
    private bool freeCamToggle = false;

    // Camera offset
    [SerializeField]
    private Vector3 offset = new Vector3(-1, 1, 0);

    // Cam speed
    [SerializeField]
    private float moveSpeed = 10f;

    void Update()
    {
        // Updates the cam mode
        freeCamToggle = GameVariables.isFreeCam;

        // Finds look at target when it spawns in
        if (lookAt == null)
        {
            GameObject level = GameObject.Find("Level(Clone)");

            if (level != null)
            {
                lookAt = level.transform;
            }
        }

        if (freeCamToggle == true)  // Free cam mode
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += (transform.forward * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position += (-transform.right * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += (-transform.forward * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += (transform.right * moveSpeed * Time.deltaTime);
            }

            // Camera pan towards mouse when holding right click
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X");
                float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y");
                transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else    // Fixed offset mode
        {
            transform.localPosition = lookAt.localPosition + offset;
            transform.LookAt(lookAt);
        }
    }
}
