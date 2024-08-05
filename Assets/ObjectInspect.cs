using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private Button returnObject;

    [SerializeField]
    private ObjectSelector objectSelector;

    [SerializeField]
    private float zoomSpeed = 1f;
    
    private bool isObjectSelected = false;
    private Vector3 objectOriginPos;
    private GameObject selectedObject;

    private void Start()
    {
        returnObject.onClick.AddListener(ReturnObject);
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
        }
    }

    private void SelectObject(GameObject obj)
    {
        objectSelector.ResetCameraButton.gameObject.SetActive(false);

        isObjectSelected = true;
        returnObject.gameObject.SetActive(true);

        selectedObject = obj;
        objectOriginPos = obj.transform.position;

        obj.transform.DOMove(inspectionPoint.position, zoomSpeed);
    }

    private void ReturnObject()
    {
        selectedObject.transform.DOMove(objectOriginPos, zoomSpeed);

        returnObject.gameObject.SetActive(false);
        objectSelector.ResetCameraButton.gameObject.SetActive(true);

        isObjectSelected = false;
    }
}
