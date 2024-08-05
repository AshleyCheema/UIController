using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectSelector : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float zoomSpeed = 1f;

    [SerializeField]
    private float zoomDistance = 2f;

    [SerializeField]
    private List<GameObject> selectableObjects = new List<GameObject>();

    [SerializeField]
    private Button resetCameraButton;
    public Button ResetCameraButton { get { return resetCameraButton; } set { resetCameraButton = value; } }

    private Vector3 cameraDefaultPos;
    private Vector3 cameraDefaultRot;

    private bool isObjectSelected = false;
    public bool IsObjectSelected { get { return isObjectSelected; } set { isObjectSelected = value; } }

    private Animation objectAnim;

    private void Start()
    {
        cameraDefaultPos = mainCamera.transform.position;
        cameraDefaultRot = mainCamera.transform.localEulerAngles;

        resetCameraButton.onClick.AddListener(RestCameraPosition);
        resetCameraButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (selectableObjects.Contains(hit.transform.gameObject) && isObjectSelected == false)
                {
                    ZoomToObject(hit.transform.gameObject);
                }
            }
        }
    }

    private void ZoomToObject(GameObject selectedObject)
    {
        isObjectSelected = true;

        if(selectedObject.TryGetComponent(out Animation anim))
        {
            objectAnim = anim;
            objectAnim.Play();
        }

        Vector3 newDistance = Vector3.MoveTowards(selectedObject.transform.position, mainCamera.transform.position, zoomDistance);

        mainCamera.transform.DOLookAt(selectedObject.transform.position, zoomSpeed);
        mainCamera.transform.DOMove(newDistance, zoomSpeed);
        
        resetCameraButton.gameObject.SetActive(true);

    }

    private void RestCameraPosition()
    {
        if(objectAnim != null)
        {
            objectAnim.Rewind();
            objectAnim = null; 
        }

        mainCamera.transform.DORotate(cameraDefaultRot, zoomSpeed);
        mainCamera.transform.DOMove(cameraDefaultPos, zoomSpeed);
        resetCameraButton.gameObject.SetActive(false);

        isObjectSelected = false;
    }
}
