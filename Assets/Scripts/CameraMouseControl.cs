using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    private bool enableCameraControl = true;
    public float sensitivity = 2.0f; // Adjust this value to control the camera rotation speed
    public float zoomSpeed = 5.0f; // Adjust this value to control the zoom speed
    private Vector3 rotation = Vector3.zero;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (enableCameraControl)
        {
            // Camera Rotation
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

            // Camera Zoom
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");
            if (zoomInput != 0)
            {
                // Zoom using the scroll wheel
                mainCamera.fieldOfView += zoomInput * zoomSpeed;
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 5.0f, 100.0f); // Adjust the min and max field of view as needed
            }

            if (Input.touchCount == 2)
            {
                // Zoom using pinch gesture with two fingers
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                mainCamera.fieldOfView += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 5.0f, 100.0f); // Adjust the min and max field of view as needed
            }
        }
    }

    public void ToggleCameraControl()
    {
        enableCameraControl = !enableCameraControl;
    }

    public void ResetCameraOrigin()
    {
        // Reset the camera rotation to 0,0,0
        rotation = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
