using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Choose type
public enum ItemType
{
    None, Seed, Tools, Sell_Only
}

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data")]


public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public int price;

    //Seed type of plants
    public PlantsType plantType;

    public ItemType itemType;
}
