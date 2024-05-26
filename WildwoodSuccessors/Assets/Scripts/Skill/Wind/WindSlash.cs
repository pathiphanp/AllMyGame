using System;
using UnityEngine;

public class WindSlash : MonoBehaviour, AddDataSkill
{
    Animator anim;
    Collider2D coll;
    [Header("Damage")]
    [Header("DataSkill")]
    DataSkill dataSkill;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        AudioManager.Instance.PlaySFX("WindSlash");
        anim.Play("Spawn");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector2 knockbackDirection = new Vector2(1, 0);
            AddKnockback _enemy = other.gameObject.GetComponent<AddKnockback>();
            _enemy.AddKnockback(knockbackDirection, dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
            other.gameObject.GetComponent<Takedamage>().Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            coll.enabled = false;
        }
    }

    void Desitroythis()
    {
        Destroy(this.gameObject);
    }

    public void AddDataSkill(DataSkill _dataSkill)
    {
        dataSkill = _dataSkill;
    }
}
