using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustWind : SkillWave
{
    public override void CastSkill()
    {
        AudioManager.Instance.PlaySFX("HeavyWind");
        anim.Play("Spawn");
    }
    void OffSkill()
    {
        anim.Play("Idel");
    }
}
