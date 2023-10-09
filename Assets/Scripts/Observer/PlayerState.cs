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

    // ���� Player�� HP
    public int currentPlayerHp;


    // Property
    public int PlusHP
    {
        get { return plusHp; }

        set 
        { 
            // HP���� �޾� �����ϰڴٴ� ���� ���������� �˸�..
            plusHp = value;
            NotifyObserversHPData();
            // ������ �˸� �� plusHp ���� 0���� �����. -> 0���� ������ �ʴ´ٸ�,
            // HP ������ 2�� �̻��� ��Ȳ������ plusHp ������ ���� �����Ǿ� ���� HP�� 200, 300 �̷������� �þ�� �Ǵ� ������ �߻��Ѵ�.
            plusHp = 0;
        }
    }

    public float RemainTime
    {
        get { return plusTime; }

        set 
        { 
            plusTime = value;
            //   Debug.Log("�� ����.. ");
            NotifyObserversTimeData();
        }
    }

   public bool IsAttack 
    {
      get { return isAttack; }
      set { isAttack = value; }
    }


    // Start �޼��忡�� PlayerHP�� Text�� �������� ���� Awake �޼��忡 �ʱ�ȭ�Ѵ�.
    private void Awake()
    {
        currentPlayerHp = 50;
    }


    // Portal�� �浹���� �� Ż�� ������ �޴´�.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            // Ż�⿡ ����..!
            // Ż�� �ߴٴ� ��Ȳ�� �����Ѵ�.
            GameControl.Instance.SetGameResult(GameResultType.Escape);
        }
    }


    public void ResisterObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log("������ ���");
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



