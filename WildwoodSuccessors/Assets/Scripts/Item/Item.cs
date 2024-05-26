using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    public void GetItem()
    {
        Item item = GetComponent<Item>();

        if (item != null)
        {
            Debug.Log(InventoryData.Instance);
            InventoryData.Instance.inventory.Add("Backpack", item);
            Destroy(gameObject);
        }
    }
}
