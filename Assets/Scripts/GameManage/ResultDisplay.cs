using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ������ ����� UI - Text�� ����Ѵ�.
public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    private Text resultText;

    // GameConrol �̱��� �ν��Ͻ� ���
    GameControl gameControl = GameControl.Instance;
    
    private void Update()
    {
        // GameConrol ���� ��� �����ͼ� ǥ��
        string result = gameControl.GetGameResult().ToString();
        resultText.text = result;
    }
}
