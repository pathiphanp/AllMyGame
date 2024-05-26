using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : Singleton<InventoryData>
{
    public InventoryManager inventory;

    public override void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = Input.mousePosition;
    }
}
