using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaves : SkillWave
{
    public override void CastSkill()
    {
        AudioManager.Instance.PlaySFX("WaterWave");
        anim.Play("Spawn");
    }
    public override void SkillAbirity(GameObject enemy)
    {
        AddSlow _enemySlow = enemy.GetComponent<AddSlow>();
        _enemySlow.AddSlow(dataSkill.durationSlow, true, dataSkill.percentSlow);
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    void OffSkill()
    {
        anim.Play("Idel");
    }
}
