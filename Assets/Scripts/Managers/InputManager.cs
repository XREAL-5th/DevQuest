using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (Input.GetMouseButton(0))
        {
            MouseAction.Invoke(Define.MouseEvent.PointerDown);
            _pressed = true;        }
        else
        {
            if (_pressed)
                MouseAction.Invoke(Define.MouseEvent.PointerUp);
            _pressed = false;
        }
    }
}
