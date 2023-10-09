using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LazyDDOLSingletonMonoBehavior<T> : LazySingletonMonoBehavior<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}