using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffscreenSpawner : MonoBehaviour
{
    public float interval = 18f;
    public float randomness = 4f;
    public int spawnAmount = 3;
    public Vector2 offset = new Vector2(10, 20);

    private Coroutine _waitRoutine = null;

    private void Update()
    {
        if (_waitRoutine != null)
        {
            return;
        }

        float waitDuration = interval + Random.Range(-randomness, randomness);
        _waitRoutine = StartCoroutine(WaitAndSpawn(waitDuration));
    }

    private IEnumerator WaitAndSpawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        int amount = Random.Range(1, spawnAmount);
        Vector2 position = Player.current.transform.position;
        position.y -= offset.y;
        position.x = Random.Range(-offset.x, offset.x);
        for (int i = 0; i < amount; i++)
        {
            Spawn(position);
        }
        _waitRoutine = null;
    }

    protected abstract void Spawn(Vector2 pos);
}
