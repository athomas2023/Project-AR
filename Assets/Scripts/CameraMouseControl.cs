using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public bool enableCameraControl = true;
    public float sensitivity = 2.0f; // Adjust this value to control the camera rotation speed
    private Vector3 rotation = Vector3.zero;

    void Update()
    {
        if (enableCameraControl)
        {
            // Check for mouse input or touch input
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                // Calculate the mouse or touch input for camera rotation
                float rotationX = Input.GetAxis("Mouse X") * sensitivity;
                float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

                // Invert the controls
                rotationX = -rotationX;
                rotationY = -rotationY;

                // Update the rotation
                rotation.x += rotationY;
                rotation.y += rotationX;

                // Clamp the camera rotation to prevent flipping
                rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

                // Apply the rotation to the camera
                transform.localRotation = Quaternion.Euler(rotation);
            }
        }
    }

    public void ResetCameraOrigin()
    {
        // Reset the camera rotation to 0,0,0
        rotation = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void EnableCamera()
    {
        enableCameraControl = !enableCameraControl;
    }
}
