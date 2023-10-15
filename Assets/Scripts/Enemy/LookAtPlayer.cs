using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy의 HP 바가 플레이어를 향해 돌아보도록 회전
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
