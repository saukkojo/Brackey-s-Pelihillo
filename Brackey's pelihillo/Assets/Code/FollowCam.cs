using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float lerp = 10;
    public Vector2 offset = Vector2.zero;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = Player.current.transform;
        GameManager.onStateChange += OnStateChange;
    }

    private void OnDestroy()
    {
        GameManager.onStateChange -= OnStateChange;
    }

    private void OnStateChange(GameManager.GameState state)
    {
        enabled = state == GameManager.GameState.Begun;
    }

    private void FixedUpdate()
    {
        float current = transform.position.y;
        float change = Mathf.Lerp(current, _playerTransform.position.y, Time.fixedDeltaTime * lerp);
        Vector3 movement = new Vector3(0, change, -10);
        movement += (Vector3)offset;
        transform.position = Vector3.Lerp(transform.position, movement, Time.fixedDeltaTime * lerp);
    }
}
