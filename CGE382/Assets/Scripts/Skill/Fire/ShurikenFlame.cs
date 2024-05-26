using System;
using System.Collections;
using UnityEngine;

public class ShurikenFlame : MonoBehaviour, CancelSkill, AddDataSkill
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    [Header("Move")]
    //Move
    [SerializeField] float moveSpeed;
    bool canMove;
    [SerializeField] float clampX;
    [Header("OnHold")]
    bool onHold;
    bool checkHold;
    [Header("DataSkill")]
    DataSkill dataSkill;
    [Header("PlayerCombat")]
    [HideInInspector] public PlayerCombat playerCombat;
    [Header("Audio")]
    [SerializeField] AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        coll.enabled = false;
        SetSound();
    }
    void SetSound()
    {
        AudioManager.Instance.PlaySFX("ShurikenFlame");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FollowMouseSpawn();
        Move();
    }
    void FollowMouseSpawn()
    {
        if (onHold)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(clampX, mousePos.y, 0);
        }
    }
    public void CastSpell()
    {
        onHold = true;
        if (onHold)
        {
            Vector3 targetScale = new Vector3(dataSkill.size, dataSkill.size, dataSkill.size);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.fixedDeltaTime * dataSkill.speedScaleSize);
        }
        if (!checkHold)
        {
            checkHold = true;
            StartCoroutine(CanHoldDuration());
        }
    }
    IEnumerator CanHoldDuration()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        playerCombat.CancelCombo();
        CancelSkill();
    }
    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Add knock
            AddKnockback _enemyK = other.gameObject.GetComponent<AddKnockback>();
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            knockbackDirection = new Vector2(knockbackDirection.x, 0);
            _enemyK.AddKnockback(knockbackDirection, dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
            //Add Damage
            Takedamage enemyT = other.gameObject.GetComponent<Takedamage>();
            enemyT.Takedamage(dataSkill.damage,dataSkill.damageGuard,dataSkill.element,dataSkill);
        }
        if (other.gameObject.tag == "EndMap")
        {
            Destroy(this.gameObject);
        }
    }
    public void CancelSkill()
    {
        onHold = false;
        canMove = true;
        checkHold = false;
        coll.enabled = true;
    }
    public void AddDataSkill(DataSkill _dataSkill)
    {
        dataSkill = _dataSkill;
    }
}
