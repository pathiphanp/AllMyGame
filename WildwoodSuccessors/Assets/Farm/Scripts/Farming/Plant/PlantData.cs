using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantData
{
    //Status
    public bool havePlant;
    public bool wantWater;
    public bool complete;
    public float timeDiasbleCount;

    //Age
    public float growthSpeed;
    public int token;
    public float seconds;
    public int mins;
    public bool isGrowth;

    //Test
    public PlantsType seedType;
    public PlayerFarming player;
    public NotificationFarming notification;

    //ItemData
    public ItemData itemData;

    public Sprite sprite;
}
