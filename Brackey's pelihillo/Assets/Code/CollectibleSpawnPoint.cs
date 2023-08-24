using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnPoint : MonoBehaviour
{
    public int itemsPlaced => _itemsPlaced;
    private int _itemsPlaced = 0;
    private void Reset()
    {
        gameObject.name = this.GetType().Name;
    }

    public void Place(Collectible collectible)
    {
        Instantiate(collectible, transform.position, Quaternion.identity);
        _itemsPlaced++;
    }
}
