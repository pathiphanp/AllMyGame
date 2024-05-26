using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFire : SkillMoveToTarget
{
    public override void SoundDestroySkill()
    {
        AudioManager.Instance.PlaySFX("Explosion sfx");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Add Damege
            Takedamage _enemy = other.GetComponent<Takedamage>();
            _enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
        }
    }
}
