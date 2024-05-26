using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : SkillMoveToTarget
{
    public override void SoundDestroySkill()
    {
        AudioManager.Instance.PlaySFX("WaterBall");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Add Damege
            Takedamage _enemy = other.GetComponent<Takedamage>();
            _enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            if (dataSkill.tier > TierSkill.Tier1)
            {
                AddSlow _enemyS = other.GetComponent<AddSlow>();
                _enemyS.AddSlow(dataSkill.durationSlow, true, dataSkill.percentSlow);
            }
        }
    }
}
