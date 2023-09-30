using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField][Range(1f, 20f)] private float sensitivity = 10f;
    private float mouseX, mouseY;
    private Transform playerTransform;

    private bool zoom = false;          //줌
    private float zoomTime = 0.0f;      //줌 제어
    private void Start()
    {
        //원활한 Debugging을 위해 마우스 커서를 보이지 않도록 하였습니다, Play 중 Esc 키를 누르면 마우스를 볼 수 있습니다.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent;
    }

    private void FixedUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, mouseX, 0));
        
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -75f, 75f);
        transform.localRotation = Quaternion.Euler(new Vector3(-mouseY, 0, 0));

        if (!zoom && Input.GetMouseButtonDown(1))
        {
            zoom = true;
            this.GetComponent<Camera>().fieldOfView = 40f;
            Debug.Log("마우스 우클릭");
        }
        if (zoom)
        {
            zoomTime += Time.deltaTime;
            if (Input.GetMouseButtonDown(1) && zoomTime >= 0.6f)
            {
                zoom = false;
                this.GetComponent<Camera>().fieldOfView = 60f;
                zoomTime = 0.0f;
            }        
        }
    }
}