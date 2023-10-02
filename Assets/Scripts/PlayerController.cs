using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float lookSensitivity;

    //private float cameraRotationLimit;
    private float currentCameraRotationX;
    private float currentCameraRotationY;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
    }

    void Update()
    {
        CameraTranspose();
        CameraRotation();      
    }

    private void CameraTranspose()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _yRotation = Input.GetAxisRaw("Mouse X");

        float _cameraRotationX = _xRotation * lookSensitivity;
        float _characterRotationY = -_yRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationY -= _characterRotationY;

        //currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        //currentCameraRotationY = Mathf.Clamp(currentCameraRotationY, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, currentCameraRotationY, 0f);
    }
}