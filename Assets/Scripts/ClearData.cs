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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
