using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
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
}
