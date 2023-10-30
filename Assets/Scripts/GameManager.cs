using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    //HideInInspector�� ���� playercontrol, cam, camc�� �ʱ�ȭ ���� �� 
    //�ڿ������� �����Ǹ�, �ν����͸� ���� ������ �ʿ䰡 ���ٴ� ���� 
    //(���� �����ڳ� �̷��� �� �ڽſ���) �˷��ݴϴ�.
    [HideInInspector] public PlayerControl player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public CameraControl camc;

    //�̱����� �ʵ�� - �̹� �ν����� â���� �����Ǿ� �ֽ��ϴ�.
    public List<GameObject> Enemys;
    public int score;
    public bool clear;

    //non-lazy, non-DDOL
    private void Awake()
    {
        //�̱��� ����
        if (main != null && main != this)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);
        //�̱��� �ʱ�ȭ
        player = GameObject.FindObjectOfType<PlayerControl>();
        cam = Camera.main;
        camc = GameObject.FindObjectOfType<CameraControl>();
        Enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        score = 0;
        clear = false;
    }


    //�̱����� �޼ҵ�
    public bool CheckEnemyLeft()
    {
        foreach (GameObject enemy in Enemys)
        {
            if (enemy.activeSelf) return true;
        }
        return false;
    }

    public void GameEnd()
    {
        if (player.healthPoint >= 0 && !CheckEnemyLeft())//���� Ŭ����
        {
            clear = true;
            Debug.Log("Game Clear");
        }
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void GenerateItem(float code)
    {
        switch (code)
        {
            case 1: break;
            default: break;
        }
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
