using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : SkillMoveToTarget
{
    [Header("Wall")]
    [SerializeField] GameObject wall;
    [Header("Sound")]
    [SerializeField] protected AudioSource sfx;
    public override void SoundSpawnSkill()
    {
        sfx.volume = AudioManager.Instance.sfxSource.volume;
        AudioManager.Instance.PlaySFXInObject(sfx, "Mountain");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Takedamage enemy = other.gameObject.GetComponent<Takedamage>();
            enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element, dataSkill);
            coll.enabled = false;
        }
    }

    public override void Die()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        AudioManager.Instance.PlaySFX("Earth Explode");
        GameObject _wall = Instantiate(dataSkill.extraOject);
        _wall.transform.position = transform.position;
        _wall.GetComponent<Wall>().dataSkill = dataSkill;
        base.Die();
    }
}
