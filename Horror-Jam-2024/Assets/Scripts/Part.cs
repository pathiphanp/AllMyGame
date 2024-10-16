using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePart
{
    Head,ArmLeft,ArmRight,LegLeft,LegRight,Body,Weapon,Armor,Weakness,Shield
}
[CreateAssetMenu(fileName = "Part - ", menuName = "CreatePart")]
public class Part : ScriptableObject
{
    public TypePart[] typePart;
    public int hp;

}
