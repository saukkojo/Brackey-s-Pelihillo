using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : OffscreenSpawner
{
    public Fish[] _fish;

    private void Awake()
    {
        Debug.Assert(_fish != null);
    }

    private void Start()
    {
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

    protected override void Spawn(Vector2 pos)
    {
        var fish = Instantiate(_fish[Random.Range(0, _fish.Length)], pos, Quaternion.identity);
        //if (pos.x > 0)
        //{
        //    fish.Spawn(Vector2.left);
        //}
        //else
        //{
        //    fish.Spawn(Vector2.right);
        //}
        fish.Spawn(Random.insideUnitCircle);
    }
}
