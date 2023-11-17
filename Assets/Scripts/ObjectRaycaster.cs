using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ObjectRaycaster : MonoBehaviour
{
    private Camera mainCamera;
    public MuseumDatabase museumDatabase;

    public Image targetImage; // Reference to the target Image component
    public TextMeshProUGUI targetText; // Reference to the target TextMeshProUGUI component

    public GameObject SidePort; // Side Port Canvas

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string groupName = hit.collider.gameObject.name; // Use the object's name as the group identifier
                museumDatabase.DisplayGroupInfo(groupName, targetImage, targetText);
                RunSidePort();
            }
            else
            {
                Debug.Log("No object detected");
            }
        }
    }

   private void RunSidePort()
{
    if (SidePort != null)
    {
        if (!SidePort.activeInHierarchy)
        {
            // If SidePort is inactive, set it to active (visible).
            SidePort.SetActive(true);
        }
        else
        {
            // If SidePort is already active, you can implement behavior for what to do when it's clicked again or interacted with.
            // For example, you could toggle it off or show additional information.
        }
    }
}
}
