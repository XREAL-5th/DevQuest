using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private void Awake()
    {
        coinText.text = "Coin Count : " + GameManager.coinCount;
    }
}
