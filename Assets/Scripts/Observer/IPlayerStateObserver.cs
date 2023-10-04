using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 옵저버 인터페이스
public interface IPlayerStateSubject
{
    // 옵저버 등록
    void ResisterObserver(IObserver observer);

    // 옵저버 제거
    void RemoveObserver(IObserver observer);

    // 옵저버들에게 내용 전달
    void NotifyObserversHPData();
    void NotifyObserversTimeData();
}

public interface IObserver
{
    // 플레이어의 HP와 RemainTime 업데이트
    void UpdateHPData(int plusHP);
    void UpdateTimeData(float plusTime);
}
