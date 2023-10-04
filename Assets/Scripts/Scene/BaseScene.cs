using UnityEngine;
using System.Collections;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {

    }

    protected virtual void Init()
    {

    }

    public abstract void Clear();
}
