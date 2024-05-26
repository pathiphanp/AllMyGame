using System.Collections;
using UnityEngine;

public class SkillWave : MonoBehaviour, AddDataSkill
{
    [SerializeField] Skill skill;
    [HideInInspector] public Animator anim;
    Rigidbody2D rb;
    [HideInInspector] public Collider2D coll;
    //Time Move
    [Header("Set")]
    [SerializeField] protected bool onDestroy;
    [HideInInspector] public bool canDodamage = true;
    [Header("DataSkill")]
    [HideInInspector] public DataSkill dataSkill;
    void OnEnable()
    {
        SkillManager.UpdateDataSKill += UpdateDataSkill;
    }
    void OnDisable()
    {
        SkillManager.UpdateDataSKill -= UpdateDataSkill;
    }
    void UpdateDataSkill()
    {
        dataSkill = SkillManager.Instance.PullDataSkill(skill);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        if (this.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }
    public IEnumerator Move()
    {
        rb.velocity = new Vector2(dataSkill.moveSpeed, rb.velocity.y);
        yield return new WaitForSeconds(dataSkill.durationSkill);
        rb.velocity = Vector2.zero;
        if (onDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {

        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log(other.gameObject.name);
            //Add knockback
            if (canDodamage)
            {
                AddKnockback _enemyK = other.gameObject.GetComponent<AddKnockback>();
                Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
                knockbackDirection = new Vector2(1, 0);
                _enemyK.AddKnockback(knockbackDirection, dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
                //Add damage
                Takedamage enemyT = other.gameObject.GetComponent<Takedamage>();
                enemyT.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
                SkillAbirity(other.gameObject);
            }

        }
    }
    public virtual void SkillAbirity(GameObject enemy) { }
    public void AddDataSkill(DataSkill _dataSkill)
    {
        dataSkill = _dataSkill;
    }
    public virtual void CastSkill() { }
}
