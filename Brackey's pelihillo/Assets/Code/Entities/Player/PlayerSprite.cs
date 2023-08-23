using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float dot = Vector2.Dot(transform.right, Vector2.right);
        _renderer.flipY = dot < 0;
    }

    public void Hit()
    {
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        float duration = 0.25f;
        _renderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        _renderer.color = Color.white;
    }
}
