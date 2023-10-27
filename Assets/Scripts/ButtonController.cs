using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject menuSet;

    public void RestartGame()
    {
        // "Assignment" 씬으로 재시작
        Debug.Log("RestartGame()");
        SceneManager.LoadScene("Assignment");
    }

    public void QuitGame()
    {
        // 게임 종료
        Debug.Log("QuitGame()");
        Application.Quit();
    }
    public void Quit()
    {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; //에디터에서 작동
            #else 
                    Application.Quit(); // 빌드 시 작동
            #endif
    }

    public void CloseButtonDown()
    {
        if (menuSet.activeSelf)
        {
            menuSet.SetActive(false);
        }
    }

    public void OpenButtonDown()
    {
        menuSet.SetActive(true);
    }

}
