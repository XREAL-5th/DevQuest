using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMGR : MonoBehaviour
{
    //싱글톤은 자신의 static reference를 가지고 있습니다.
    public static GameOverMGR main;

    //HideInInspector를 통해 playercontrol, cam, camc는 초기화 과정 중 
    //자연스럽게 설정되며, 인스펙터를 통해 설정할 필요가 없다는 점을 
    //(동료 개발자나 미래의 나 자신에게) 알려줍니다.
    [HideInInspector] public GameObject gameclearwindow;
    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public int enemiesnumleft;

    //싱글톤의 필드들 - 이미 인스펙터 창으로 설정되어 있습니다.
    //public GameObject[] enemies = { };

    //non-lazy, non-DDOL
    private void Awake()
    {
        //싱글톤 설정
        main = this;
        //싱글톤 초기화
        gameclearwindow = GameObject.FindGameObjectWithTag("ClearWindow");
        gameclearwindow.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesnumleft = enemies.Length;

    }

   
    //싱글톤의 메소드. 모든 오리가 죽어있으면 false 반환
    public bool IfAllEnemiesdead()
    {
        // 과제에 과녁 리스트를 싱글톤에 필드로 저장해 놓으세요! 라고 되어있어서 그렇게 하고싶었는데요.. 실패했습니다. ㅠㅠ 
        // enemy가 다 들어가있는 게임 오브젝트 배열을 만들고, 순회하며, 모든 enemy가 null일 경우 게임 clear화면이 뜨게 하고싶었으나
        // Debug를 찍어보니 destroy 된것이라도 인식이 되더라구요. 
        // 두 방법 전부 되지 않아서 결국 다른 방식을 썼습니다. 배열에 한번 넣으면 Destroy되더라도 할당되었던 오브젝트를 계속해서 인식하는 것인가요? 어떻게 해결해야하는지 모르겠습니다..

        /*
        //1번 방법
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.activeInHierarchy)
            {
                Debug.Log(enemy);
                return false;
            }
        }
        //2번 방법
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

}
