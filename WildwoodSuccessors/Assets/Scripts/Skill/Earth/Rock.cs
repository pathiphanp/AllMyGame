using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : SkillMoveToTarget
{
    public override void SoundSpawnSkill()
    {
        AudioManager.Instance.PlaySFX("Rock");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Takedamage enemy = other.gameObject.GetComponent<Takedamage>();
            enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            Vector2 knockbackDiraction = (transform.position - other.gameObject.transform.position).normalized;
            knockbackDiraction.y = 0;
            other.gameObject.GetComponent<AddKnockback>().AddKnockback(knockbackDiraction,
            dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
        }
    }

    public override void Die()
    {
        AudioManager.Instance.PlaySFX("Earth Explode");
        base.Die();
        if (dataSkill.extraOject != null)
        {
            GameObject _wall = Instantiate(dataSkill.extraOject);
            _wall.transform.position = transform.position;
            _wall.GetComponent<Wall>().dataSkill = dataSkill;
        }
    }
}
