using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

// 옵저버 패턴과 싱글톤으로 구현된 GameControl 으로 게임의 전반적인 상황을 UI- Text에 반영한다.
public class UITextManager : MonoBehaviour, IObserver
{
    [SerializeField] PlayerState playerState;
    private ShootRailGun RailGun;

    [SerializeField] private float setTime = 30.0f;
    [SerializeField] private Text RemainTimeText;
    [SerializeField] private Text CurrentPlayerHPText;
    [SerializeField] private Text CurrentAttackState;

    // HP를 깎는 업데이트를 잠시 중지하기 위한 bool타입 변수
    private bool stopUpdate = false;

    private void Start()
    {
        UpdateCurrentPlayerHP();

        // 플레이어의 공격 상태 여부를 알기 위해 ShootRailGun 컴포넌트를 가져온다.
        RailGun = playerState.GetComponentInChildren<ShootRailGun>();

        if (RailGun != null )
        {
            CurrentAttackState.text = "Attack State : " + RailGun.canAttack;
        }
        else
        {
            Debug.LogError("ShootRailGun 컴포넌트를 찾을 수 없습니다.");
        }
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
            // TimeOver 상황을 전달한다. 
            GameControl.Instance.SetGameResult(GameResultType.TimeOver);
        }

        RemainTimeText.text = "Remain Time : " + Mathf.Round(setTime).ToString();
        // 현재 플레이어의 공격 상태 여부를 알려준다. 
        CurrentAttackState.text = "Attack State : " + RailGun.canAttack;

        if (playerState.currentPlayerHp <= 0)
        {
            // 플레이어의 HP가 0일 때 Player의 죽음 상황을 전달한다.
            GameControl.Instance.SetGameResult(GameResultType.PlayerDie);
        }
    }


    // 오브젝트 활성화 시 옵저버로 등록한다.
    private void OnEnable()
    {
        playerState.ResisterObserver(this);
    }

    private void OnDisable()
    {
        playerState.RemoveObserver(this);
    }



    // 기존 Update()문 안에서 HP를 깎일 때마다 텍스트를 업데이트 했지만, HP물약을 먹은 후 HP 갱신이 텍스트에 업데이트가 이루어지지 않았음.
    // Update 문을 두 개로 분리하여 옵저버가 HP 갱신을 알아차릴 때 stopUpdate문을 true로 만듬. 이어서 현재 HP 텍스트 업데이트를 잠시 강제종료 함.
    // 강제 종료 후 갱신 HP를 받아와 새로운 HP로 텍스트에 반영한다.
    // 갱신된 HP 텍스트 반영이 완료되었다면, stopUpdate 를 다시 false로 바꿔, HP를 깎는 업데이트를 진행한다.
    private void FixedUpdate()
    {
      // 조건이 충족되면 Update 메서드를 중지
        if (stopUpdate)
        {
            return;
        }
        // 매 프레임마다 UI 텍스트 업데이트
         UpdateCurrentPlayerHP();  
    }


    // 현재 플레이어의 HP를 UI 텍스트에 반영
    private void UpdateCurrentPlayerHP()
    {
        CurrentPlayerHPText.text = "Player HP : " + playerState.currentPlayerHp;
    }

    // HP 물약을 획득하여 갱신된 HP를 텍스트에 반영
    public void UpdateHPData(int plusHP)
    {
        stopUpdate = true;
        playerState.currentPlayerHp = plusHP;
        CurrentPlayerHPText.text = "Player HP : " + playerState.currentPlayerHp;
        stopUpdate = false;
    }

    // Coin을 획득하여 갱신된 Time 텍스트에 반영
    public void UpdateTimeData(float plusTime)
    {
        setTime += plusTime;
        RemainTimeText.text = "Remain Time : " + Mathf.Round(setTime).ToString();
    }
}
