using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject connected_ui;
    //Build()�� �˾��� ȭ�鿡 ������ ������ ȣ��˴ϴ�. �̸��� Build�� ������... �׳� ��ȸ �����̶�� ����.
    public virtual void Build()
    {
        connected_ui.SetActive(true);
        //�˾��� �����, ������ �Ͻ��������ݴϴ�
        //if (GameControl.main != null) GameControl.Pause();
    }

    public virtual void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        connected_ui.SetActive(false);
        //�˾��� �ݰ� ���� �˾��� ���ٸ�, ������ �簳�մϴ�
        //if (GameControl.main != null && !GameControl.main.DialogOpen()) GameControl.Unpause();
    }
}
