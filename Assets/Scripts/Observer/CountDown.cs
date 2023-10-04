using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour, IObserver
{
    [SerializeField] PlayerState playerState;

    [SerializeField] private float setTime = 30.0f;
    [SerializeField] private Text RemainTimeText;
    [SerializeField] private Text CurrentPlayerHPText;

    
    private void Start()
    {
     //   RemainTimeText.text = "Remain Time : " + setTime;
    }

    private void Update()
    {
        if(setTime > 0)
        {
            setTime -= Time.deltaTime;
        }

        else if(setTime <= 0) 
        {
            Time.timeScale = 0.0f;
        }

        RemainTimeText.text = "Remain Time : "  + Mathf.Round(setTime).ToString();
    }


    private void OnEnable()
    {
        // 오브젝트 활성화 시 옵저버로 등록한다.
        playerState.ResisterObserver(this);
    }

    private void OnDisable()
    {
        playerState.RemoveObserver(this);
    }


    public void UpdateHPData(int plusHP)
    {
        CurrentPlayerHPText.text = "Player HP : " + plusHP;
    }

    public void UpdateTimeData(float plusTime)
    {
        setTime += plusTime;
        RemainTimeText.text = "Remain Time : " + Mathf.Round(setTime).ToString();
    }
}
