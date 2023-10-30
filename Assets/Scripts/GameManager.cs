using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    //HideInInspector를 통해 playercontrol, cam, camc는 초기화 과정 중 
    //자연스럽게 설정되며, 인스펙터를 통해 설정할 필요가 없다는 점을 
    //(동료 개발자나 미래의 나 자신에게) 알려줍니다.
    [HideInInspector] public PlayerControl player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public CameraControl camc;

    //싱글톤의 필드들 - 이미 인스펙터 창으로 설정되어 있습니다.
    public List<GameObject> Enemys;
    public int score;
    public bool clear;

    //non-lazy, non-DDOL
    private void Awake()
    {
        //싱글톤 설정
        if (main != null && main != this)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);
        //싱글톤 초기화
        player = GameObject.FindObjectOfType<PlayerControl>();
        cam = Camera.main;
        camc = GameObject.FindObjectOfType<CameraControl>();
        Enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        score = 0;
        clear = false;
    }


    //싱글톤의 메소드
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
        if (player.healthPoint >= 0 && !CheckEnemyLeft())//게임 클리어
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
                UnityEditor.EditorApplication.isPlaying = false; //에디터에서 작동
        #else
				Application.Quit(); // 빌드 시 작동
        #endif
    } 

}
