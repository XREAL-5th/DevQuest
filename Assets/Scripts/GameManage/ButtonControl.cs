using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject quitPanel;
   [SerializeField] private bool isQuitPanelActive = false;


    private void Update()
    {
        // null 체크를 하여 변수가 할당되었는지 확인과 동시에 Q키를 눌렀을 때 Panel 활성화
        if(quitPanel != null && Input.GetKeyDown(KeyCode.Q))
        {
            ActivePanel();
        }

        // 게임 종료 패널이 활성화된 상태에서 Y 키를 누르면 게임 종료
        if (quitPanel != null && quitPanel.activeSelf && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("게임 종료 허락");
            QuitGame(); 
        }

        // L 키를 누를 시 시작 화면 전환
        if (quitPanel != null && quitPanel.activeSelf && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("시작 씬으로 이동");
            LobyScene();
        }
    }

    // 게임 화면 전환
    public void StartGame()
    {
        SceneManager.LoadScene("Assignment");
    }

    public void LobyScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        // 게임 종료

#if   UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //에디터에서 작동
#else
		Application.Quit(); // 빌드 시 작동
#endif

    }

    public void ActivePanel()
    {
        isQuitPanelActive = !isQuitPanelActive;
        quitPanel.SetActive(isQuitPanelActive);

    }

}
