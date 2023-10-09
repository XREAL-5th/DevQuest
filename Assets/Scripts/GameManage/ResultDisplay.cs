using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 게임의 결과를 UI - Text에 출력한다.
public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    private Text resultText;

    // GameConrol 싱글톤 인스턴스 얻기
    GameControl gameControl = GameControl.Instance;
    
    private void Update()
    {
        // GameConrol 에서 결과 가져와서 표시
        string result = gameControl.GetGameResult().ToString();
        resultText.text = result;
    }
}
