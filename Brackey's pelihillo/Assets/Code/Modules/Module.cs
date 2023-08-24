using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public const int HEIGHT = 30;
    public float minDepth = 0;
    public float maxDepth = HEIGHT;

    public Collectible[] collectibles = null;
    private CollectibleSpawnPoint[] spawnPoints = null;
    public int spawnAmount = 1;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<CollectibleSpawnPoint>();
        spawnAmount = Mathf.Clamp(spawnAmount, 1, spawnPoints.Length);
    }

    public void Setup(float depth)
    {
        transform.position = new Vector2(0, -depth);
        minDepth = depth - HEIGHT * 0.5f;
        maxDepth = depth + HEIGHT * 0.5f;
        if (collectibles != null && spawnPoints != null)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var spawnPoint = GetSpawnPoint();
                spawnPoint.Place(collectibles[Random.Range(0, collectibles.Length)]);
            }
        }

        CollectibleSpawnPoint GetSpawnPoint()
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            int tries = 1;
            while (spawnPoint.itemsPlaced > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                tries++;
                if (tries > spawnPoints.Length)
                {
                    break;
                }
            }
            return spawnPoint;
        }
    }
}
