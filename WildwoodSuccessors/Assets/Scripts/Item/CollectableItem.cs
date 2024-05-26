using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Item))]
public class CollectableItem : MonoBehaviour
{
    public Sprite icon;

    public PlantsType plantType;

    //public InventoryData inventoryData;

    private void Start()
    {
        //inventoryData = FindObjectOfType<InventoryData>();
    }

    public void GetItem()
    {
        Item item = GetComponent<Item>();

        if(item != null)
        {
            //InventoryData.Instance.inventory.Add(item);
            Destroy(gameObject);
        }   
    }
}
