using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSlime : Boss, CallBlackBoss
{
    bool BossOn;
    [Header("Move")]
    [SerializeField] float movedistance;
    [SerializeField] float attackdistance;
    [SerializeField] float blackdistance;
    bool rndMove;
    [Header("Jump")]
    [SerializeField] float durationJumpUp;
    [SerializeField] float heightJump;
    [SerializeField] float durationJumpDown;
    [SerializeField] float downJump;
    [SerializeField] float holdAirTime;
    [SerializeField] int loopJump;
    [SerializeField] float delayNextJump;
    Vector2 lastfloor;
    void OnEnable()
    {
        BossOn = true;
        SetShield();
        // StartJump();
    }
    #region test
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         // shieldSlider.value = 0;
    //         // StartCoroutine(RegenShield());
    //         // shieldRegenMode = true;
    //         // StartJump();
    //         BossAttack();
    //     }
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         // JumpOutMap();
    //         spawnEnemy.StopSpawnMonster();
    //     }
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         // transform.position = new Vector3(-10, 0, 0);
    //         // StopAllCoroutines();
    //         // rb.Sleep();
    //         // rb.velocity = Vector2.zero;
    //     }
    // }
    #endregion
    public override void BossArmorElementBreak()
    {
        // AddStun(durationStun, false);
        SetMoveEnable(false);
        haveSield = false;
        onAttack = true;
        StartCoroutine(BossStun());
    }
    IEnumerator BossStun()
    {
        yield return new WaitForSeconds(10);
        BossGoOutMap();
    }
    void StartJump()
    {
        sprite.flipX = false;
        outMap = false;
        StartCoroutine(Jump(loopJump, movedistance, false, true));
    }

    IEnumerator Jump(int loopJump, float movedistance, bool canbackAtk, bool canGetfloor)
    {
        SetMoveEnable(false);
        onAttack = true;
        for (int i = 0; i < loopJump; i++)
        {
            //Move
            rb.velocity = new Vector2(movedistance, rb.velocity.y);
            if (canGetfloor)
            {
                lastfloor.y = transform.position.y;
            }
            // Up
            rb.AddForce(new Vector2(rb.velocity.x, heightJump));
            yield return new WaitForSeconds(durationJumpUp);
            //Hold Time in Air
            rb.Sleep();
            yield return new WaitForSeconds(holdAirTime);
            //Down
            rb.AddForce(new Vector2(rb.velocity.x, downJump));
            yield return new WaitForSeconds(durationJumpDown);
            //Stop
            rb.Sleep();
            transform.position = new Vector2(transform.position.x, lastfloor.y);
            rb.velocity = Vector2.zero;
            if (i < loopJump)
            {
                yield return new WaitForSeconds(delayNextJump);
            }
            else
            {
                yield return true;
            }
        }
        if (!outMap)
        {
            SetMoveEnable(true);
        }
        if (canbackAtk)
        {
            onAttack = false;
        }
    }
    public override void HalfHp()
    {
        // onHpHalf = true;
        // loopJump = 3;
        // // StartJump();
    }
    public override void BossAttack()
    {
        StopAllCoroutines();
        StartCoroutine(BossAttackSet());
        // StartCoroutine(Jump(1, attackdistance, false, true));
    }
    IEnumerator BossAttackSet()
    {
        SetMoveEnable(false);
        rb.velocity = new Vector2(5, rb.velocity.y);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        rb.Sleep();
        rb.velocity = new Vector2(-18 , rb.velocity.y);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;
        rb.Sleep();
        onAttack = false;
        SetMoveEnable(true);
    }
    public override void BossBackAttack()
    {
        StopAllCoroutines();
        // StartCoroutine(Jump(1, blackdistance, true, false));
    }
    public override void BossGoOutMap()
    {
        StopAllCoroutines();
        AudioManager.Instance.PlaySFX("Warp");
        anim.Play("Warp");
        transform.position = new Vector2(100, 100);
        StartCoroutine(SpawnMonsterDuration(false));
        // sprite.flipX = true;
        // StartCoroutine(Jump(50, -movedistance, false, true));
    }
    public override void BossOutMap()
    {
        transform.position = new Vector2(100, 100);
        outMap = true;
        StopAllCoroutines();
        rb.Sleep();
        rb.velocity = Vector2.zero;
        if (BossOn)
        {
            StartCoroutine(SpawnMonsterDuration(false));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void CallBackBoss()
    {
        BossOn = false;
        BossGoOutMap();
    }
    public override void BossSleep()
    {
        StopAllCoroutines();
        rb.Sleep();
        transform.position = new Vector2(100, 100);
        gameObject.SetActive(false);
    }
}
