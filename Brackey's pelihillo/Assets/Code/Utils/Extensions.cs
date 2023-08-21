using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T AddOrGetComponent<T>(this Component component) where T : Component
    {
        if (component.gameObject.TryGetComponent(out T t))
        {
            return t;
        }
        else
        {
            return component.gameObject.AddComponent<T>();
        }
    }

    public static void Enable(this GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public static void Disable(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
