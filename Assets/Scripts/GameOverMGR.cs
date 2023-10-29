using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMGR : MonoBehaviour
{
    //�̱����� �ڽ��� static reference�� ������ �ֽ��ϴ�.
    public static GameOverMGR main;

    //HideInInspector�� ���� playercontrol, cam, camc�� �ʱ�ȭ ���� �� 
    //�ڿ������� �����Ǹ�, �ν����͸� ���� ������ �ʿ䰡 ���ٴ� ���� 
    //(���� �����ڳ� �̷��� �� �ڽſ���) �˷��ݴϴ�.
    [HideInInspector] public GameObject gameclearwindow;
    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public int enemiesnumleft;

    [HideInInspector] public GameObject gameexitwindow;

    //�̱����� �ʵ�� - �̹� �ν����� â���� �����Ǿ� �ֽ��ϴ�.
    //public GameObject[] enemies = { };

    //non-lazy, non-DDOL
    private void Awake()
    {
        //�̱��� ����
        main = this;
        //�̱��� �ʱ�ȭ
        gameclearwindow = GameObject.FindGameObjectWithTag("ClearWindow");
        gameclearwindow.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesnumleft = enemies.Length;

        gameexitwindow = GameObject.FindGameObjectWithTag("ExitWindow");
        gameexitwindow.SetActive(false);

    }

   
    //�̱����� �޼ҵ�. ��� ������ �׾������� false ��ȯ
    public bool IfAllEnemiesdead()
    {
        // ������ ���� ����Ʈ�� �̱��濡 �ʵ�� ������ ��������! ��� �Ǿ��־ �׷��� �ϰ�;��µ���.. �����߽��ϴ�. �Ф� 
        // enemy�� �� ���ִ� ���� ������Ʈ �迭�� �����, ��ȸ�ϸ�, ��� enemy�� null�� ��� ���� clearȭ���� �߰� �ϰ�;�����
        // Debug�� ���� destroy �Ȱ��̶� �ν��� �Ǵ��󱸿�. 
        // �� ��� ���� ���� �ʾƼ� �ᱹ �ٸ� ����� ����ϴ�. �迭�� �ѹ� ������ Destroy�Ǵ��� �Ҵ�Ǿ��� ������Ʈ�� ����ؼ� �ν��ϴ� ���ΰ���? ��� �ذ��ؾ��ϴ��� �𸣰ڽ��ϴ�..

        /*
        //1�� ���
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.activeInHierarchy)
            {
                Debug.Log(enemy);
                return false;
            }
        }
        //2�� ���
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                Debug.Log(enemies[i]);
            return false;
        }
        */

        Debug.Log(enemiesnumleft);
        if (enemiesnumleft > 0)
        {
            //Debug.Log(enemiesnumleft);
            return false;
        }
        else
        {
            //Debug.Log(enemiesnumleft);
            gameclearwindow.SetActive(true);
            return true;
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOverUIClicked();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void GameOverUIClicked()
    {
        gameexitwindow.SetActive(true);
        Debug.Log("Ŭ����");
        
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
