using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject exitPopUp;
    public GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopUpUI();
        }
    }
    public void exitPopUpUI()
    {
        player.GetComponent<PlayerAttack>().canShoot = false;
        exitPopUp.SetActive(true);
        Cursor.visible = true;
    }
    public void exit()
    {

        Quit();
    }
    public void gameContinue()
    {
        exitPopUp.SetActive(false);
        Cursor.visible = false;
        player.GetComponent<PlayerAttack>().canShoot = true;
    }

    public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //�����Ϳ��� �۵�
    #else
				    Application.Quit(); // ���� �� �۵�
    #endif
        }

}
