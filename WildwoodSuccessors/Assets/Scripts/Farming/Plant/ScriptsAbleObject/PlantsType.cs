using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Plants Type", menuName = "Plants Type")]
public class PlantsType : ScriptableObject
{
    public new string name;

    public float ageWatering;
    public float maxAge;

    public Sprite stage01;
    public Sprite stage02;
    public Sprite stage03;

    public Sprite HarvestIcon;

    //Hold item type
    public ItemData itemData;

    //public GameObject item;
}
