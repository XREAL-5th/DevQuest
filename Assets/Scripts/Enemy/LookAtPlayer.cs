using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy�� HP �ٰ� �÷��̾ ���� ���ƺ����� ȸ��
public class LookAtPlayer : MonoBehaviour
{
    Camera m_camera = null;
    [SerializeField] private Transform cam;

    private void Start()
    {
        m_camera = Camera.main;
        cam = m_camera.GetComponent<Transform>();   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
