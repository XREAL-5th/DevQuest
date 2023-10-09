using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ ���ϰ� �̱������� ������ GameControl ���� ������ �������� ��Ȳ�� UI- Text�� �ݿ��Ѵ�.
public class UITextManager : MonoBehaviour, IObserver
{
    [SerializeField] PlayerState playerState;

    [SerializeField] private float setTime = 30.0f;
    [SerializeField] private Text RemainTimeText;
    [SerializeField] private Text CurrentPlayerHPText;

    // HP�� ��� ������Ʈ�� ��� �����ϱ� ���� boolŸ�� ����
    private bool stopUpdate = false;

    private void Start()
    {
        UpdateCurrentPlayerHP();
    }

    private void Update()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
        }

        else if (setTime <= 0)
        {
            Time.timeScale = 0.0f;
            // TimeOver ��Ȳ�� �����Ѵ�. 
            GameControl.Instance.SetGameResult(GameResultType.TimeOver);
        }

        RemainTimeText.text = "Remain Time : " + Mathf.Round(setTime).ToString();

        if (playerState.currentPlayerHp <= 0)
        {
            // �÷��̾��� HP�� 0�� �� Player�� ���� ��Ȳ�� �����Ѵ�.
            GameControl.Instance.SetGameResult(GameResultType.PlayerDie);
        }
    }


    // ������Ʈ Ȱ��ȭ �� �������� ����Ѵ�.
    private void OnEnable()
    {
        playerState.ResisterObserver(this);
    }

    private void OnDisable()
    {
        playerState.RemoveObserver(this);
    }



    // ���� Update()�� �ȿ��� HP�� ���� ������ �ؽ�Ʈ�� ������Ʈ ������, HP������ ���� �� HP ������ �ؽ�Ʈ�� ������Ʈ�� �̷������ �ʾ���.
    // Update ���� �� ���� �и��Ͽ� �������� HP ������ �˾����� �� stopUpdate���� true�� ����. �̾ �ռ� ����� �ؽ�Ʈ ������Ʈ�� ��� �������� ��.
    // ���� ���� �� ���� HP�� �޾ƿ� ���ο� HP�� �ؽ�Ʈ�� �ݿ��Ѵ�.
    // ���ŵ� HP �ؽ�Ʈ �ݿ��� �Ϸ�Ǿ��ٸ�, stopUpdate �� �ٽ� false�� �ٲ�, HP�� ��� ������Ʈ�� �����Ѵ�.
    private void FixedUpdate()
    {
      // ������ �����Ǹ� Update �޼��带 ����
        if (stopUpdate)
        {
            return;
        }
        // �� �����Ӹ��� UI �ؽ�Ʈ ������Ʈ
         UpdateCurrentPlayerHP();  
    }


    // ���� �÷��̾��� HP�� UI �ؽ�Ʈ�� �ݿ�
    private void UpdateCurrentPlayerHP()
    {
        CurrentPlayerHPText.text = "Player HP : " + playerState.currentPlayerHp;
    }

    // HP ������ ȹ���Ͽ� ���ŵ� HP�� �ؽ�Ʈ�� �ݿ�
    public void UpdateHPData(int plusHP)
    {
        stopUpdate = true;
        playerState.currentPlayerHp = plusHP;
        CurrentPlayerHPText.text = "Player HP : " + playerState.currentPlayerHp;
        stopUpdate = false;
    }

    // Coin�� ȹ���Ͽ� ���ŵ� Time �ؽ�Ʈ�� �ݿ�
    public void UpdateTimeData(float plusTime)
    {
        setTime += plusTime;
        RemainTimeText.text = "Remain Time : " + Mathf.Round(setTime).ToString();
    }
}
