using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // 마우스 커서가 보이도록 설정
        Cursor.visible = true; // 마우스 커서를 보이도록 설정 
    }
    public void OnClickEndButton()
    {
        Debug.Log("Menu - Exit 누름");
        SceneManager.LoadScene("TitleScene");

    }
}
