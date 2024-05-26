 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBat : Boss, CallBlackBoss
{
    [Header("Attack")]
    [SerializeField] float movebackDuration;
    [SerializeField] float movebackForce;
    [Header("OutOffMap")]
    [SerializeField] float durationWaitCastSkill;
    [SerializeField] float speedMoveoutOffMap;
    [Header("ShadowMode")]
    [HideInInspector] public BossBat baseBatSpawn;
    [SerializeField] GameObject prefabBatClone;
    [HideInInspector] public bool shadowMode;
    List<GameObject> batClone = new List<GameObject>();
    
    // Update is called once per frame
    public override void Start()
    {
        SetShield();
        base.Start();
    }
    public override void BossArmorElementBreak()
    {
        onAttack = true;
        haveSield = false;
        StopAllCoroutines();
        anim.Play("Stun");
        SetMoveEnable(false);
        StartCoroutine(BossStun());
        // AddStun(durationStun, false);
    }
    IEnumerator BossStun()
    {
        yield return new WaitForSeconds(10);
        BossGoOutMap();
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
    public override void BossGoOutMap()
    {
        StopAllCoroutines();
        AudioManager.Instance.PlaySFX("Warp");
        anim.Play("Warp");
        transform.position = new Vector2(100, 100);
        StartCoroutine(SpawnMonsterDuration(true));
        // StartCoroutine(GoOutMap());
    }
    public override void BossOutMap()
    {
        // StopAllCoroutines();
        // StartCoroutine(SpawnMonsterDuration(true));
        // onAttack = false;
        // outMap = false;
    }
    IEnumerator GoOutMap()
    {
        SetMoveEnable(false);
        onAttack = true;
        outMap = true;
        float castSkillTime = 0;
        anim.Play("Idel");
        yield return new WaitForSeconds(durationWaitCastSkill);
        anim.Play("CastSkill");
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip c in clips)
        {
            if (c.name == "CastSkill")
            {
                castSkillTime = c.length;
            }
        }
        yield return new WaitForSeconds(castSkillTime);
        anim.Play("Idel");
        sprite.flipX = false;
        SetMoveEnable(true);
        move = speedMoveoutOffMap;
    }
    public override void HalfHp()
    {
        // if (!shadowMode)
        // {
        //     onHpHalf = true;
        //     SkillSplitShadow();
        // }
    }
    void SkillSplitShadow()
    {
        hpSlider.gameObject.SetActive(false);
        shieldSlider.gameObject.SetActive(false);
        transform.position = new Vector2(100, 100);
        SetMoveEnable(false);
        //top 2.5 / mid 0 /bot -3
        float[] positionClone = { 2.5f, 0, -3 };
        SetMoveEnable(false);
        for (int i = 0; i < 3; i++)
        {
            Vector2 shadowPo = new Vector2(-10, positionClone[i]);
            GameObject _batshadow = Instantiate(prefabBatClone, shadowPo, Quaternion.Euler(0, 0, 0));
            BossBat _baseBat = _batshadow.GetComponent<BossBat>();
            _baseBat.shadowMode = true;
            _baseBat.baseBatSpawn = this;
            _batshadow.SetActive(false);
            batClone.Add(_batshadow);
        }
        foreach (GameObject bh in batClone)
        {
            bh.SetActive(true);
        }
    }
    public override void BossDie()
    {
        base.BossDie();
        if (shadowMode)
        {
            if (baseBatSpawn != null)
            {
                baseBatSpawn.batClone.Remove(this.gameObject);
                baseBatSpawn.CheckClone();
            }
        }
    }
    public void CheckClone()
    {
        if (batClone.Count == 0)
        {
            Takedamage(500, 0, Element.FIRE, null);
            StartCoroutine(RegenShield());
            RandomPositionInStartPosition();
            SetMoveEnable(true);
        }
    }
    public override void BossSleep()
    {
        Debug.Log("EndDay");
        StopAllCoroutines();
        rb.Sleep();
        transform.position = new Vector2(100, 100);
        gameObject.SetActive(false);
    }

    public void CallBackBoss()
    {

    }
}
