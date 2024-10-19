using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EffectSkill
{
    None, ArmorPiercing, ShieldBrake, BreakTheShield,DodgeShields,Legsweep,Stun,BreakWeapon,ChangeDodgeShields,WeaponWeaknes
}
[CreateAssetMenu(fileName = "Skill - ", menuName = "CreateSkill")]
public class Skill : ScriptableObject
{
    public Sprite iconSkill;
    public int damage;
    public EffectSkill effectSkill;
    public int accuract;
    public Sprite spriteSkillHaveShield;
    public Sprite spriteSkillNotHaveShield;
    [TextArea(1, 5)]
    public string description;
}
