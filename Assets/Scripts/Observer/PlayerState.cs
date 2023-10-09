using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerState : MonoBehaviour, IPlayerStateSubject
{
    private List<IObserver> observers = new List<IObserver>();

    [Header("Player State")]
    [SerializeField] private int plusHp;
    [SerializeField] private float plusTime;
    [SerializeField] private bool isAttack = false;

    // 현재 Player의 HP
    public int currentPlayerHp;


    // Property
    public int PlusHP
    {
        get { return plusHp; }

        set 
        { 
            // HP값을 받아 갱신하겠다는 것을 옵저버에게 알림..
            plusHp = value;
            NotifyObserversHPData();
            // 갱신을 알린 후 plusHp 값을 0으로 만든다. -> 0으로 만들지 않는다면,
            // HP 물약이 2개 이상인 상황에서는 plusHp 변수의 값이 누적되어 갱신 HP가 200, 300 이런식으로 늘어나게 되는 문제가 발생한다.
            plusHp = 0;
        }
    }

    public float RemainTime
    {
        get { return plusTime; }

        set 
        { 
            plusTime = value;
            //   Debug.Log("값 받음.. ");
            NotifyObserversTimeData();
        }
    }

   public bool IsAttack 
    {
      get { return isAttack; }
      set { isAttack = value; }
    }


    // Start 메서드에서 PlayerHP가 Text로 보여지기 전에 Awake 메서드에 초기화한다.
    private void Awake()
    {
        currentPlayerHp = 50;
    }


    // Portal에 충돌했을 때 탈출 판정을 받는다.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            // 탈출에 성공..!
            // 탈출 했다는 상황을 전달한다.
            GameControl.Instance.SetGameResult(GameResultType.Escape);
        }
    }


    public void ResisterObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log("옵저버 등록");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserversHPData()
    {
        foreach (IObserver observer in observers)
        {
            observer.UpdateHPData(plusHp);
        }
    }
    public void NotifyObserversTimeData()
    {
        foreach(IObserver observer in observers)
        {
            observer.UpdateTimeData(plusTime);
        }
    }

}



