using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton pattern.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public abstract bool doPersist { get; }
    public static bool exists => _current != null;

    private static T _current;
    public static T current
    {
        get
        {
            if (_current == null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var obj = FindObjectOfType<T>();
                    if (obj != null)
                    {
                        _current = obj;
                        return _current;
                    }
                }
#endif
                T prefab = Resources.Load<T>($"Singletons/{typeof(T).Name}");

                if (prefab)
                {
                    _current = Instantiate(prefab);
                }
                else
                {
                    var gameobject = new GameObject(typeof(T).Name);
                    _current = gameobject.AddComponent<T>();
                }
            }

            return _current;
        }
    }

    protected void Awake()
    {
        if (_current == null)
        {
            _current = this as T;
        }

        if (_current != this)
            Destroy(gameObject);

        if (doPersist)
        {
            DontDestroyOnLoad(gameObject);
        }

        Init();
    }

    /// <summary>
    /// Method to be used to initialize the singleton.
    /// </summary>
    protected virtual void Init()
    {
    }
}
