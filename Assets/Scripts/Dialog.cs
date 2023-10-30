using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject connected_ui;
    //Build()는 팝업이 화면에 보여질 때마다 호출됩니다. 이름이 Build인 이유는... 그냥 학회 전통이라고 하죠.
    public virtual void Build()
    {
        connected_ui.SetActive(true);
        //팝업이 생기면, 게임을 일시중지해줍니다
        //if (GameControl.main != null) GameControl.Pause();
    }

    public virtual void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        connected_ui.SetActive(false);
        //팝업을 닫고 남은 팝업이 없다면, 게임을 재개합니다
        //if (GameControl.main != null && !GameControl.main.DialogOpen()) GameControl.Unpause();
    }
}
