using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LazySingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new(typeof(T).Name);
                _instance = gameObject.AddComponent<T>();
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}