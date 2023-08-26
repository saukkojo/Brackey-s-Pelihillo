using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawnPoint : MonoBehaviour
{
    public int itemsPlaced => _itemsPlaced;
    private int _itemsPlaced = 0;
    private void Reset()
    {
        gameObject.name = this.GetType().Name;
    }

    public void Place(Collectible collectible)
    {
        if (collectible == null) return;
        var parent = transform.parent != null ? transform.parent : transform;
        Instantiate(collectible, transform.position, Quaternion.identity, parent);
        _itemsPlaced++;
    }
}
