using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Pool;

public class SpearWind : MonoBehaviour
{
    public IObjectPool<SpearWind> Pool { get; set; }
    Rigidbody2D rb;
    Animator anim;
    Collider2D coll;
    SpriteRenderer sprit;
    bool canMove;
    DataSkill dataSkill;
    bool canAtk = false;
    void OnEnable()
    {
        SetStart();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        sprit = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(dataSkill.moveSpeed, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<AddKnockback>() != null)
            {
                AddKnockback _enemyN = other.gameObject.GetComponent<AddKnockback>();
                Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
                knockbackDirection.y = 0;
                _enemyN.AddKnockback(knockbackDirection, dataSkill.knockbackForce,
                dataSkill.durationKnockbackForce);
            }
            Takedamage enemy = other.gameObject.GetComponent<Takedamage>();
            enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            Die();
        }
        if (other.gameObject.tag == "EndMap")
        {
            Die();
        }
    }
    void Die()
    {
        if (canAtk)
        {
            // AudioManager.Instance.PlaySFX("SpearAttack");
        }
        coll.enabled = false;
        anim.Play("Die");
    }
    public void RetrunToPool()
    {
        canAtk = true;
        sprit.enabled = false;
        Pool.Release(this);
    }
    void SetStart()
    {
        dataSkill = SkillManager.Instance.PullDataSkill(Skill.SPEARWIND);
        sprit.enabled = true;
        coll.enabled = true;
        anim.Play("Spawn");
        canMove = true;
    }
}

