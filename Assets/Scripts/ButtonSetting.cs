using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : MonoBehaviour
{
    [SerializeField] public GameObject Dialog;

    public void Stop()
    {
        Dialog.SetActive(true);
    }

    public void Resume()
    {
        Dialog.SetActive(false);
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

