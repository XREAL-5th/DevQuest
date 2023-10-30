using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // 마우스 커서가 보이도록 설정
        Cursor.visible = true; // 마우스 커서를 보이도록 설정 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; //에디터에서 작동
            #else
                    Application.Quit(); // 빌드 시 작동
            #endif
    }
}
