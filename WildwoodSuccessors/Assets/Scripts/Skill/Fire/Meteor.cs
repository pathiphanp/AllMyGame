using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : SkillMoveToTarget
{
    public override void SoundDestroySkill()
    {
        AudioManager.Instance.PlaySFX("Explosion sfx");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Takedamage enemy = other.GetComponent<Takedamage>();
            enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            AddStun enemyS = other.GetComponent<AddStun>();
            enemyS.AddStun(dataSkill.durationStun, false);
            coll.enabled = false;
        }
    }
}
