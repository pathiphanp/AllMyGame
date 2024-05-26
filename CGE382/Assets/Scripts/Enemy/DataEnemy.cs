using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy - ", menuName = "DataEnemy")]
public class DataMonster : ScriptableObject
{
    public Sprite spriteEnemy;
    public int maxHp;
    public int damage;
    public Element elementShield;
    public int shield;
    public float speedMove;
    public float speedRegenShield;
}
