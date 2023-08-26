using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Collectible : MonoBehaviour, ICollectible
{
    private BoxCollider2D _collider;

    public static event Action<ICollectible> onCollect;

    public virtual void Collect()
    {
        if (onCollect != null)
        {
            onCollect.Invoke(this);
        }
        Destroy(gameObject);
    }

    protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }
}
