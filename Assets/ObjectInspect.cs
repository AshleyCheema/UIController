using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspect : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField] 
    private Transform inspectionPoint;

    [SerializeField]
    private List<GameObject> selectableObjects = new List<GameObject>();

    [SerializeField]
    private Button resetObject;

    [SerializeField]
    private Button takeObject;

    [SerializeField]
    private Button returnObject;

    [SerializeField]
    private ObjectSelector objectSelector;

    [SerializeField]
    private float zoomSpeed = 1f;

    [SerializeField]
    private float rotationSpeed = 5f;
    
    private bool isObjectSelected = false;
    private bool isReturnObjectActive = false;
    private Vector3 objectOriginPos;
    private Vector3 objectOriginRot;
    private GameObject selectedObject;

    private void Start()
    {
        resetObject.onClick.AddListener(ResetObjectPos);
        resetObject.gameObject.SetActive(false);

        takeObject.onClick.AddListener(TakeObject);
        takeObject.gameObject.SetActive(false);

        returnObject.onClick.AddListener(() => ReturnObject());
        returnObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (objectSelector.IsObjectSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (selectableObjects.Contains(hit.transform.gameObject) && isObjectSelected == false)
                    {
                        SelectObject(hit.transform.gameObject);
                    }
                }
            }

            if(Input.GetMouseButton(0))
            {
                if(selectedObject != null)
                {
                    selectedObject.transform.RotateAround(selectedObject.transform.position, transform.right, Input.GetAxis("Mouse Y") * rotationSpeed);
                    selectedObject.transform.RotateAround(selectedObject.transform.position, transform.up, -Input.GetAxis("Mouse X") * rotationSpeed);
                }
            }   
        }
    }

    private void SelectObject(GameObject obj)
    {
        objectSelector.ResetCameraButton.gameObject.SetActive(false);

        isObjectSelected = true;
        resetObject.gameObject.SetActive(true);
        takeObject.gameObject.SetActive(true);

        selectedObject = obj;
        objectOriginPos = obj.transform.position;
        objectOriginRot = obj.transform.localEulerAngles;

        obj.transform.DOMove(inspectionPoint.position, zoomSpeed);
    }

    private void TakeObject()
    {
        ReturnObject();

        selectedObject.SetActive(false);

        StoredItem.selectedObject = selectedObject;

        ResetObjectPos();
    }

    private void ReturnObject()
    {
        if(StoredItem.selectedObject != null)
        {
            StoredItem.selectedObject.SetActive(true);
        }

        isReturnObjectActive = !isReturnObjectActive;
        returnObject.gameObject.SetActive(isReturnObjectActive);
    }

    private void ResetObjectPos()
    {
        selectedObject.transform.DOMove(objectOriginPos, zoomSpeed);
        selectedObject.transform.DORotate(objectOriginRot, zoomSpeed);
        selectedObject = null;

        resetObject.gameObject.SetActive(false);
        takeObject.gameObject.SetActive(false);
        objectSelector.ResetCameraButton.gameObject.SetActive(true);

        isObjectSelected = false;
    }
}
