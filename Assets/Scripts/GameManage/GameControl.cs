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


// ������ �������� ��Ȳ�� �����Ѵ�. => ������ ��Ȳ�� �ľ��ϰ�, ������ ��Ȳ�� ���� ������ �ݿ��Ѵ�.
public class GameControl : MonoBehaviour
{
    private static GameControl instance;
    public static GameControl Instance { get { return instance; } }

    // ���� ��� ���¸� ��Ÿ���� Enum Ÿ�� ���� ����
    private GameResultType gameResult = GameResultType.Playing;
    // Ż�� Portal 
    public GameObject EscapePortal;



    // �̱��� �ν��Ͻ��� �ٸ� ������ �̵��� �� �ı����� �ʵ��� �մϴ�.
    private void Awake()
    {
        // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ���� �ı��մϴ�.
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

    // ������ ����� �����Ѵ�.
    public void SetGameResult(GameResultType SetResult)
    {
        gameResult = SetResult;
        SwitchGameResult();
    }

    // ������ ����� �����Ѵ�.
    public GameResultType GetGameResult()
    {
        return gameResult;
    }

    // ���� ����� ���� ó���� ���⿡ �߰�
    private void SwitchGameResult()
    {
        switch (gameResult)
        {
            case GameResultType.PlayerDie:
                // �÷��̾ ���� ����� ó��
                EndGame();
                break;

            case GameResultType.TimeOver:
                // �ð��� �ʰ��� ����� ó��
                EndGame();
                break;

            case GameResultType.Escape:
                // Ż�� ������ ����� ó��
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
