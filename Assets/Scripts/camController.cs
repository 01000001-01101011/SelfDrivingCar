using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class camController : MonoBehaviour
{
    public player player;
    public float mouseSensitivity = 100f;
    public GameObject yRotBody;
    private Transform yRotTransform;
    public GameObject xRotBody;
    private Transform xRotTransform;
 

    private Camera cam;
    private float xRotation = 0f;
    private float scrollDelta;


    private float mouseX;
    private float mouseY;

    private void Start()
    {
        yRotTransform = yRotBody.transform;
        xRotTransform = xRotBody.transform;

        cam = gameObject.GetComponent<Camera>();


        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    }

    private void LateUpdate()
    {
        if (!IsPointerOverUIObject())
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0, 90);


            xRotTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            yRotTransform.Rotate(Vector3.up * mouseX);



            scrollDelta = Input.mouseScrollDelta.y;
            if (scrollDelta != 0)
            {
                //cam.gameObject.transform.localPosition = cam.gameObject.transform.localPosition + Vector3.forward * scrollDelta * 5f;
                cam.gameObject.transform.localPosition = new Vector3(0, 0, Mathf.Min(-10, cam.gameObject.transform.localPosition.z + scrollDelta * 5f));
            }
        }
    }

    /// <summary>
    /// Checks if the pointer, or any touch is over (Raycastable) UI.
    /// WARNING: ONLY WORKS RELIABLY IF IN LateUpdate/LateTickable!
    /// </summary>
    private bool IsPointerOverUIObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        for (int touchIndex = 0; touchIndex < Input.touchCount; touchIndex++)
        {
            Touch touch = Input.GetTouch(touchIndex);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return true;
        }

        return false;
    }

}



