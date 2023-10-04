using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IPlayerStateSubject
{
    private List<IObserver> observers = new List<IObserver>();

    [SerializeField] private int plusHp;
    public int HP
    {
        get { return plusHp; }

        set 
        { 
            // HP값을 받아 갱신하겠다는 것을 옵저버에게 알림..
            plusHp = value;
            NotifyObserversHPData();
        }
    }

    [SerializeField] private float plusTime;
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

    public bool IsAttack { get; set; }


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



