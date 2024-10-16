using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [Header("Part")]
    [SerializeField] PartData[] parts;

    [Header("Skill")]
    [SerializeField] public Skill[] skills;
    void Start()
    {
        SetUpPart();
        CheckPart();
    }
    //Check Part เช็คว่ามีชิ้นส่วนไหนบ้างทำแอ็คชั่นไหนได้บ้าง
    //Check arm leg weapon 
    void SetUpPart()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            // parts[i].hpPart = parts[i].part.hp;
        }
    }
    void CheckPart()
    {
        foreach (PartData pD in parts)
        {
            if (pD.hpPart > 0)
            {
                pD.canUsePart = true;
                // Debug.Log(pD.namePart + " can use");
            }
            else
            {
                // Debug.Log(pD.namePart + " can't use");
            }
        }
    }
    //--------------------Action-------------------------//
    //Attack
    public void PlayerAttack(Skill skill)
    {
        ControlGamePlay._instance.controlEnemy.TakeDamage(skill.damage,skill.effectSkill,ControlGamePlay._instance.partPlayerSelect);
    }
    
}
