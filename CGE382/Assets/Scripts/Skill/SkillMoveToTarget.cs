using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMoveToTarget : MonoBehaviour
{
    Animator anim;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public SpawnSkill spawn;
    [Header("Damage")]
    bool checkAtk = true;
    [Header("Target")]
    [HideInInspector] public Transform target;
    [SerializeField] float stopRange;
    [Header("DataSkill")]
    [HideInInspector] public DataSkill dataSkill;
    
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    void Start()
    {
        SoundSpawnSkill();
    }
    public virtual void Update()
    {
        MoveToTarget();
    }
    void MoveToTarget()
    {
        if (target != null)
        {
            Vector2 diraction = target.position - transform.position;
            float distance = diraction.magnitude;
            if (distance > stopRange)
            {
                diraction.Normalize();
                transform.Translate(diraction * dataSkill.moveSpeed * Time.deltaTime);
            }
            else
            {
                if (checkAtk)
                {
                    checkAtk = false;
                    coll.enabled = true;
                    Die();
                }
            }
        }
    }
    void Destroythis()
    {
        Destroy(this.gameObject);
    }
    public virtual void Die()
    {
        SoundDestroySkill();
        anim.Play("Die");
        Destroy(target.gameObject);
    }
    public virtual void SoundSpawnSkill() { }
    public virtual void SoundDestroySkill() { }
}
