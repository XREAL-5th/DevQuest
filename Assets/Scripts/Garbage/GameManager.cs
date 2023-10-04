//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    // 싱글톤 인스턴스
//    public static GameManager Instance { get; private set; }

//    // 플레이어 상태 인스턴스
//    public PlayerState PlayerState;

//    private void Awake()
//    {
//        // 싱글톤 패턴 구현
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//        // 플레이어 상태 초기화
//        // PlayerState = gameObject.AddComponent<PlayerState>();
//    }

//    private void Update()
//    {
//        // 플레이어 상태 업데이트
//        PlayerState.UpdateState();
//    }
//}
