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
        // null üũ�� �Ͽ� ������ �Ҵ�Ǿ����� Ȯ�ΰ� ���ÿ� QŰ�� ������ �� Panel Ȱ��ȭ
        if(quitPanel != null && Input.GetKeyDown(KeyCode.Q))
        {
            ActivePanel();
        }

        // ���� ���� �г��� Ȱ��ȭ�� ���¿��� Y Ű�� ������ ���� ����
        if (quitPanel != null && quitPanel.activeSelf && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("���� ���� ���");
            QuitGame(); 
        }

        // L Ű�� ���� �� ���� ȭ�� ��ȯ
        if (quitPanel != null && quitPanel.activeSelf && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("���� ������ �̵�");
            LobyScene();
        }
    }

    // ���� ȭ�� ��ȯ
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
        // ���� ����

#if   UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //�����Ϳ��� �۵�
#else
		Application.Quit(); // ���� �� �۵�
#endif

    }

    public void ActivePanel()
    {
        isQuitPanelActive = !isQuitPanelActive;
        quitPanel.SetActive(isQuitPanelActive);

    }

}
