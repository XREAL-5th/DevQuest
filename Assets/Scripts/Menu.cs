using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ���� ���̵��� ����
        Cursor.visible = true; // ���콺 Ŀ���� ���̵��� ���� 
    }
    public void OnClickEndButton()
    {
        Debug.Log("Menu - Exit ����");
        SceneManager.LoadScene("TitleScene");

    }
}
