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
    }

    private void FixedUpdate()
    {
        float current = transform.position.y;
        float change = Mathf.Lerp(current, _playerTransform.position.y, Time.fixedDeltaTime * lerp);
        Vector3 movement = (Vector3)offset + new Vector3(0, change, -10);
        transform.position = Vector3.Lerp(transform.position, movement, Time.fixedDeltaTime * lerp);
    }
}
