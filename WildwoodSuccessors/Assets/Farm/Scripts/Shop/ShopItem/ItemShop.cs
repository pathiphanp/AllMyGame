using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item shop", menuName = "Item shop")]
public class ItemShop : ScriptableObject
{
    public new string name;
    public Sprite image;
    public string description;
    public int price;
}
