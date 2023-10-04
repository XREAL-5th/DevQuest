//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class UITextObserver : MonoBehaviour, IPlayerStateObserver
//{
//    [SerializeField] private CountDown countDownText;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // 플레이어 상태 변화 감지를 위해 옵저버 등록
//        GameManager.Instance.PlayerState.OnStateChanged += OnPlayerStateChanged;
//    }

//    public void OnPlayerStateChanged(PlayerState playerState)
//    {
//        // 플레이어 상태가 변경될 때마다 호출되는 메서드
//        UpdateUIText(playerState);
//    }

//    private void UpdateUIText(PlayerState playerState)
//    {
//        // 여기에서 UI Text를 업데이트하는 로직을 작성
//        // uiText.text = $"HP: {playerState.HP}\nRemain Time: {playerState.RemainTime}\nIs Attack: {playerState.IsAttack}";

//        //countDownText.RemainTimeText.text = $"Remain Time: {countDownText.setTime + playerState.RemainTime}";

//        Debug.Log(playerState.RemainTime);
        
//        countDownText.RemainTimeText.text = "Remain Time : " + countDownText.setTime + playerState.RemainTime;
    
//    }

//}
