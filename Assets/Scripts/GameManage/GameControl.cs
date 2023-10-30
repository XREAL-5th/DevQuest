using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameResultType
{
    Playing,
    PlayerDie,
    TimeOver,
    Escape
}


// 게임의 전반적인 상황을 제어한다. => 게임의 상황을 파악하고, 마지막 상황을 다음 씬에게 반영한다.
public class GameControl : MonoBehaviour
{
    private static GameControl instance;
    public static GameControl Instance { get { return instance; } }

    // 게임 결과 상태를 나타내는 Enum 타입 변수 선언
    private GameResultType gameResult = GameResultType.Playing;
    // 탈출 Portal 
    public GameObject EscapePortal;



    // 싱글톤 인스턴스가 다른 씬으로 이동할 때 파괴되지 않도록 합니다.
    private void Awake()
    {
        // 이미 인스턴스가 존재하면 새로 생성된 것을 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        EscapePortal.SetActive(false);
    }

    // 게임의 결과를 설정한다.
    public void SetGameResult(GameResultType SetResult)
    {
        gameResult = SetResult;
        SwitchGameResult();
    }

    // 게임의 결과를 전달한다.
    public GameResultType GetGameResult()
    {
        return gameResult;
    }

    // 게임 결과에 따른 처리를 여기에 추가
    private void SwitchGameResult()
    {
        switch (gameResult)
        {
            case GameResultType.PlayerDie:
                // 플레이어가 죽은 경우의 처리
                EndGame();
                break;

            case GameResultType.TimeOver:
                // 시간이 초과된 경우의 처리
                EndGame();
                break;

            case GameResultType.Escape:
                // 탈출 성공한 경우의 처리
                EndGame();
                break;

            default:
                break;
        }
    }


    private void EndGame()
    {
        SceneManager.LoadScene("Final Scene");
    }
}
