using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public enum TierSkill
{
    Tier1, Tier2, Tire3
}
[CreateAssetMenu(fileName = "Skill - ", menuName = "DataSkill")]
public class DataSkill : ScriptableObject
{
    public TierSkill tier;
    [Header("Skill")]
    public Element element;
    public Skill skill;
    public string nameSkill;
    public VideoClip iconSkill;
    [TextArea(5, 10)] public string description;
    [Header("Status")]
    public int damage;
    public int damageGuard;
    public float delayDamage;
    public float delaySpawnSkill;
    public float durationSkill;
    public float knockbackForce;
    public float durationKnockbackForce;
    public float moveSpeed;
    public int amount;
    public float size;
    public float speedScaleSize;
    public float pullSpeed;
    public int maxHp;
    public float durationSlow;
    public float percentSlow;
    public float durationStun;
    public GameObject extraOject;
    public int fallDamage;
}
