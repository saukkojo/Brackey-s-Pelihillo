using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : OffscreenSpawner
{
    public Bubble[] bubbles = null;

    protected override void Spawn(Vector2 pos)
    {
        var bubble = Instantiate(bubbles[Random.Range(0, bubbles.Length)], pos, Quaternion.identity);
        bubble.Spawn();
    }
}
