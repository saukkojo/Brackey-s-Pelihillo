using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Vector2 startPos;
    private float time = 0;
    private float randomX, randomY;
    private void Awake()
    {
        randomX = Random.value;
        randomY = Random.value;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-15, 5));
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        time += Time.deltaTime;
        float x = Mathf.Sin(time * 0.3f * randomX);
        float y = Mathf.Cos(time * 0.2f * randomY);
        var offset = new Vector2(x, y);
        transform.position = startPos + offset;
    }
}
