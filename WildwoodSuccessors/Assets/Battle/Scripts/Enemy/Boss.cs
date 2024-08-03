using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected SpawnEnemy spawnEnemy;
    [Header("SpawnEnemy")]
    [SerializeField] protected float durationSpawnMonster;
    [SerializeField] protected float delaySpawnMonster;
    [SerializeField] protected MonsterType monsterType;
    [Header("Shield")]
    [SerializeField] protected float durationRegenShield;
    [SerializeField] protected float durationStun;
    [Header("Attack")]
    [SerializeField] float movebackDuration;
    [SerializeField] float movebackForce;
    public virtual void Start()
    {
        SetShield();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }
    public virtual IEnumerator SpawnMonsterDuration(bool side)
    {
        SetMoveEnable(false);
        SetShield();
        StartCoroutine(RegenShield());
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(20);
        RandomPositionInStartPosition();
        sprite.flipX = side;
        SetMoveEnable(true);
        onAttack = false;
    }
    public override void BossArmorElementBreak()
    {
        onAttack = true;
        haveSield = false;
        StopAllCoroutines();
        anim.Play("Stun");
        SetMoveEnable(false);
        StartCoroutine(BossStun());
    }
    IEnumerator BossStun()
    {
        yield return new WaitForSeconds(10);
        BossGoOutMap();
    }
    public override void BossGoOutMap()
    {
        StopAllCoroutines();
        AudioManager.Instance.PlaySFX("Warp");
        anim.Play("Warp");
        transform.position = new Vector2(100, 100);
        StartCoroutine(SpawnMonsterDuration(true));
    }
     public override void BossAttack()
    {
        SetMoveEnable(false);
        anim.Play("Attack");
    }
    public override void BossBackAttack()
    {
        Vector2 knockbackDirection = new Vector2(1, 0);
        AddKnockback(knockbackDirection, movebackForce, movebackDuration);
        onAttack = false;
    }
    public override void BossSleep()
    {
        StopAllCoroutines();
        rb.Sleep();
        transform.position = new Vector2(100, 100);
        gameObject.SetActive(false);
    }
    public override void BossDie()
    {
        base.BossDie();
    }
}
