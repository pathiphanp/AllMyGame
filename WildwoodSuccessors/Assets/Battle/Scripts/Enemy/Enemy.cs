using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using System;

public class Enemy : MonoBehaviour, Takedamage, AddSlow,
AddPull, AddKnockback, CancelPulled, AddBubble, AddStun
{
    public bool BossMode;
    [HideInInspector] public Collider2D coll;
    protected Animator anim;
    protected Rigidbody2D rb;
    public IObjectPool<Enemy> Pool { get; set; }
    [SerializeField] public SpriteRenderer sprite;
    [Header("Status")]
    [Header("Hp")]
    protected int _hp;
    int maxHp;
    [Header("Shield")]
    Element elementShield;
    bool rndElementShield = true;
    int maxShield;
    int _shield;
    [Header("Move")]
    [SerializeField] protected float move;
    float speedMove;
    bool canMove = true;
    [Header("Slow")]
    bool slowOn;
    [Header("Pull")]
    float pullSpeed;
    bool onPull;
    bool canAddPull = true;
    [HideInInspector] public GameObject pullTarget;
    Vector3 moveDiraction;
    [Header("Knockback")]
    bool canKnockback = true;
    Coroutine callKnockback;
    [Header("Stun")]
    bool canStun = true;
    Coroutine callStun;
    [Header("Bubble")]
    Bubble _bubbleS;
    [Header("Attack")]
    int damage;
    protected GameObject target;
    protected bool onAttack;
    [Header("CheckAttack")]
    [SerializeField] Transform checkAttack;
    [SerializeField] float distanceCheckAttack;
    [SerializeField] LayerMask attackTartget;
    [Header("UI")]
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected Slider shieldSlider;
    protected bool shieldRegenMode;
    protected bool haveSield = true;

    [Header("DataSkill")]
    public DataMonster dataMoster;
    Coroutine _UIUpdate;
    [SerializeField] bool _UiShowAways;
    [Header("TotalDamage")]
    [SerializeField] int totalDamage;
    Coroutine calltotaldamage;
    [Header("Boss")]
    protected bool outMap;
    protected bool onHpHalf;
    float halfHp;

    #region Dummy
    [SerializeField] bool isDummy;
    Coroutine resetDummy;
    Vector3 startPositionDummy;
    #endregion
    void Awake()
    {
        startPositionDummy = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        if (dataMoster != null)
        {
            SetUpMonster();
        }
        halfHp = _hp * 0.5f;
    }
    void Update()
    {
        OnPulled();
        RayCheckAttack();
    }
    void FixedUpdate()
    {
        MoveToTower();
    }

    #region Debuf
    #region Pull
    public void AddPull(GameObject target, float _pullspeed, float _durationPull,
    bool addKnockback, Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce)
    {
        if (canAddPull)
        {
            if (!BossMode)
            {
                AddPullSetting(target, _pullspeed);
                StartCoroutine(DurationPulled(_durationPull, addKnockback, knockbackDiraction, knockbackForce, durationknockbackForce));
            }

        }
    }
    public void AddPull(GameObject target, float _pullspeed, float _durationPull)
    {
        if (canAddPull)
        {
            AddPullSetting(target, _pullspeed);
            StartCoroutine(DurationPulled(_durationPull, false, Vector3.zero, 0f, 0));
        }
    }
    void AddPullSetting(GameObject target, float _pullspeed)
    {
        SetPullEnable(true);
        pullTarget = target;
        pullSpeed = _pullspeed;
        move = pullSpeed;
    }
    IEnumerator DurationPulled(float _durationPull, bool addKnockback, Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce)
    {
        yield return new WaitForSeconds(_durationPull);
        SetPullEnable(false);
        SetKnockEnable(false);
        if (addKnockback)
        {
            AddKnockback(knockbackDiraction, knockbackForce, durationknockbackForce);
        }
        if (coll.enabled == false)
        {
            coll.enabled = true;
        }
    }

    void OnPulled()
    {
        if (pullTarget != null)
        {
            Vector3 diraction = (transform.position - pullTarget.transform.position).normalized;
            moveDiraction = diraction;
        }
    }
    #endregion
    #region Knockback
    public void AddKnockback(Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce)
    {

        if (canKnockback)
        {
            if (BossMode && haveSield)
            {
                SetKnockEnable(true);
                callKnockback = StartCoroutine(Knockback(knockbackDiraction, knockbackForce, durationknockbackForce));
            }
            else
            {

            }
            if (!BossMode)
            {
                SetKnockEnable(true);
                callKnockback = StartCoroutine(Knockback(knockbackDiraction, knockbackForce, durationknockbackForce));
            }
        }
    }
    IEnumerator Knockback(Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce)
    {
        if (knockbackDiraction.x > 0)
        {
            knockbackDiraction.x = 1;
        }
        else
        {
            knockbackDiraction.x = -1;
        }
        if (BossMode)
        {
            knockbackForce *= 0.20f;
        }
        rb.AddForce(knockbackDiraction * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(durationknockbackForce);
        SetMoveEnable(true);
        SetKnockEnable(false);
        if (isDummy)
        {
            if (resetDummy != null)
            {
                StopCoroutine(resetDummy);
            }
            resetDummy = StartCoroutine(ResetPositionDummy());
        }
    }
    IEnumerator ResetPositionDummy()
    {
        yield return new WaitForSeconds(3);
        transform.position = startPositionDummy;
    }
    #endregion
    #region Slow
    public void AddSlow(float durationSlow, bool add, float percent)
    {
        if (add && !slowOn)
        {
            add = false;
            slowOn = true;
            float _speedSlow = move * percent;
            StartCoroutine(DelaySlow(durationSlow, _speedSlow));
        }
    }
    IEnumerator DelaySlow(float durationSlow, float _speedSlow)
    {
        move = _speedSlow;
        yield return new WaitForSeconds(durationSlow);
        slowOn = false;
        move = speedMove;
    }
    #endregion
    #region Stun
    public void AddStun(float duration, bool playKnockUp)
    {
        if (canStun)
        {
            canStun = false;
            if (callStun == null)
            {
                callStun = StartCoroutine(Stun(duration, playKnockUp));
            }
            else
            {
                StopCoroutine(callStun);
                callStun = StartCoroutine(Stun(duration, playKnockUp));
            }
        }
    }
    IEnumerator Stun(float duration, bool knockUp)
    {
        SetMoveEnable(false);
        if (BossMode)
        {
            if (!haveSield)
            {
                yield return new WaitForSeconds(duration);
                BossGoOutMap();
            }
            else if (haveSield)
            {
                duration *= 0.5f;
                yield return new WaitForSeconds(duration);
                SetMoveEnable(true);
                onAttack = false;
                canStun = true;
            }
        }
        else
        {
            if (knockUp)
            {
                StartCoroutine(KnockUp());

            }
            yield return new WaitForSeconds(duration);
            SetMoveEnable(true);
            onAttack = false;
            canStun = true;
        }
        callStun = null;
    }
    IEnumerator KnockUp()
    {
        Vector2 knockUp = new Vector2(0, 1);
        rb.AddForce(knockUp * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        rb.AddForce(knockUp * -2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
    }
    #endregion
    #region Bubble
    public void AddBubble(GameObject bubble, BubbleSpawn bubbleSpawn)
    {
        if (!BossMode)
        {
            SetBubbleEnable(true);
            //เส็กระเบิดที่Enemy จากนั้นให้Enemyเป็นชายของBubbleAnimation 
            GameObject _bubble = Instantiate(bubble, transform.position, transform.localRotation);
            _bubbleS = _bubble.GetComponent<Bubble>();
            _bubbleS.bubbleSpawn = bubbleSpawn;
            bubbleSpawn.bubbleList.Add(_bubbleS);
            transform.SetParent(_bubbleS.bubble.transform);
            transform.localPosition = Vector3.zero;
            _bubbleS.target = this.gameObject;
        }

    }
    public void CancelBubble()
    {
        SetBubbleEnable(false);
    }
    #endregion
    #endregion
    #region SetEnable
    void SetKnockEnable(bool enable)
    {
        if (enable)
        {
            canKnockback = false;
            SetMoveEnable(false);
            onAttack = true;
        }
        else
        {
            SetMoveEnable(true);
            onAttack = false;
            canKnockback = true;
        }
    }
    void SetPullEnable(bool enable)
    {
        if (enable)
        {
            onPull = true;
            canAddPull = false;
            SetMoveEnable(false);
            SetKnockEnable(false);
        }
        else
        {
            onPull = false;
            onAttack = false;
            pullTarget = null;
            canAddPull = true;
            SetMoveEnable(true);
            SetKnockEnable(true);
        }
    }
    void SetBubbleEnable(bool enable)
    {
        if (enable)
        {
            SetMoveEnable(false);
            SetKnockEnable(true);
            SetPullEnable(false);
            onAttack = true;
            canStun = false;
        }
        else
        {
            SetPullEnable(false);
            canKnockback = true;
            canStun = true;
            onAttack = false;
            SetMoveEnable(true);
        }
    }
    public void SetMoveEnable(bool enable)
    {
        if (enable)
        {
            rb.velocity = Vector2.zero;
            move = speedMove;
            canMove = true;
        }
        else
        {
            canMove = false;
            rb.velocity = Vector2.zero;
            canStun = true;
        }
    }
    #endregion
    void MoveToTower()
    {
        if (canMove)
        {
            anim.Play("EnemyIdel");
            rb.velocity = new Vector2(move, 0);
        }
        if (onPull)
        {
            rb.velocity = new Vector2(moveDiraction.x, moveDiraction.y) * move;
        }
    }
    #region Trigger
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (BossMode)
            {
                StartCoroutine(DelayBossBlackAttack());
            }
        }
        if (other.gameObject.tag == "DestroyArea")
        {
            if (BossMode)
            {
                BossSleep();
            }
            else
            {
                Die();
            }
        }
        if (other.gameObject.tag == "Tower")
        {
            if (BossMode)
            {
                StartCoroutine(DelayBossBlackAttack());
            }
        }
    }
    #endregion
    public void RandomPositionInStartPosition()
    {
        float rndY = UnityEngine.Random.Range(-3.4f, 3.5f);
        transform.position = new Vector2(-13, rndY);
    }
    public void Takedamage(int damage, int damageShield, Element attackelement, DataSkill dataSkill)
    {
        if (_UIUpdate != null)
        {
            StopCoroutine(_UIUpdate);
        }
        if (calltotaldamage != null)
        {
            StopCoroutine(calltotaldamage);
        }
        calltotaldamage = StartCoroutine(TotalDamage());
        float damageTake = damage;
        totalDamage += Mathf.RoundToInt(damageTake);
        if (_shield > 0)
        {
            //_shield get hit
            float reduceDamage = damageTake * 0.20f;
            damageTake -= reduceDamage;
            if (dataMoster.elementShield == attackelement)
            {
                _shield -= damageShield;
                if (_shield <= 0 && haveSield)
                {
                    //_shield break
                    BossArmorElementBreak();
                }
            }
        }
        //Get damage
        _hp -= Mathf.RoundToInt(damageTake);
        _UIUpdate = StartCoroutine(UpdateUI());
        if (BossMode)
        {
            //Boss Do Something
            BossGetHit();
            //BossMode if hp Half
            if (_hp <= halfHp && !onHpHalf)
            {
                if (!haveSield)
                {
                    HalfHp();
                }
            }
        }
        if (_hp <= 0)
        {
            if (!isDummy)
            {
                //Die
                _hp = 0;
                if (!BossMode)
                {
                    transform.position = new Vector2(50, 50);
                }
                Die();
            }
            else
            {
                _hp = dataMoster.maxHp;
                hpSlider.maxValue = maxHp;
                hpSlider.value = _hp;
            }
        }
    }
    public virtual void HalfHp() { }
    IEnumerator TotalDamage()
    {
        yield return new WaitForSeconds(2f);
        totalDamage = 0;
    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(checkAttack.transform.position, Vector3.right * distanceCheckAttack);
    }
    void RayCheckAttack()
    {
        RaycastHit2D rayAttack = Physics2D.Raycast(checkAttack.position, Vector2.right, distanceCheckAttack, attackTartget);
        if (rayAttack && !onAttack)
        {
            onAttack = true;
            if (BossMode)
            {
                BossAttack();
                target = rayAttack.collider.gameObject;
                target.GetComponent<TakedamageS>().Takedamage(dataMoster.damage, target);
            }
            else
            {
                SetMoveEnable(false);
                target = rayAttack.collider.gameObject;
                StartCoroutine(StartAtk());
            }
        }
    }
    IEnumerator StartAtk()
    {
        yield return new WaitForSeconds(0.2f);
        anim.Play("EnemyAttack");
    }
    void Attack()
    {
        if (target != null)
        {
            target = target.GetComponent<TakedamageS>().Takedamage(damage, target);
            anim.Play("Attack");
        }
        else
        {
            onAttack = false;
            SetMoveEnable(true);
        }
    }
    public void SetUpMonster()
    {
        sprite.sprite = dataMoster.spriteEnemy;
        coll.enabled = true;
        maxHp = dataMoster.maxHp;
        _hp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = _hp;
        if (shieldSlider != null)
        {
            maxShield = dataMoster.shield;
            _shield = maxShield;
            shieldSlider.maxValue = maxShield;
            shieldSlider.value = _shield;
        }
        speedMove = dataMoster.speedMove;
        move = speedMove;
        damage = dataMoster.damage;
        _UIUpdate = StartCoroutine(UpdateUI());
        onAttack = false;
        SetMoveEnable(true);
    }
    IEnumerator UpdateUI()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.value = _hp;
        if (shieldSlider != null && !shieldRegenMode)
        {
            shieldSlider.value = _shield;
        }
        if (!_UiShowAways)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            hpSlider.gameObject.SetActive(false);
        }

    }
    public void RetrunToPool()
    {
        StopAllCoroutines();
        Pool.Release(this);
    }
    void Die()
    {
        if (!BossMode)
        {
            StopAllCoroutines();
            SetPullEnable(false);
            SetKnockEnable(false);
            SetMoveEnable(false);
            rb.velocity = Vector2.zero;
            rb.Sleep();
            coll.enabled = false;
            target = null;
            if (_bubbleS != null)
            {
                _bubbleS.target = null;
            }
            //Spawn Effect Die
            StartCoroutine(DelayReturnToPool());
        }
        else
        {
            BossDie();
        }
    }
    IEnumerator DelayReturnToPool()
    {
        yield return new WaitForSeconds(2f);
        RetrunToPool();
    }
    public virtual void SetShield()
    {
        if (rndElementShield)
        {
            Element rndElement = (Element)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Element)).Length);
            dataMoster.elementShield = rndElement;
        }
        else
        {
            dataMoster.elementShield = elementShield;
        }
        //SetColor shield
        if (dataMoster.elementShield == Element.FIRE)
        {
            shieldSlider.fillRect.gameObject.GetComponent<Image>().color = Color.red;
        }
        if (dataMoster.elementShield == Element.WATER)
        {
            shieldSlider.fillRect.gameObject.GetComponent<Image>().color = Color.blue;
        }
        if (dataMoster.elementShield == Element.WIND)
        {
            shieldSlider.fillRect.gameObject.GetComponent<Image>().color = Color.green;
        }
        if (dataMoster.elementShield == Element.EARTH)
        {
            shieldSlider.fillRect.gameObject.GetComponent<Image>().color = new Color(0.6f, 0.3f, 0.1f);
        }
    }
    #region Boss
    public virtual void BossAttack() { }
    public virtual void BossGoOutMap() { }
    public virtual void BossBackAttack() { }
    public virtual void BossGetHit() { }
    public virtual void BossArmorElementBreak() { }
    public virtual void BossSleep() { }
    public virtual void BossDie()
    {
        StopAllCoroutines();
        SetMoveEnable(false);
        hpSlider.gameObject.SetActive(false);
        if (shieldSlider != null)
        {
            shieldSlider.gameObject.SetActive(false);
        }
        onAttack = true;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        coll.enabled = false;
        GameManagerPor.Instance.dataManager.money += 300;
        anim.Play("Die");
    }
    void DestroyWithDie()
    {
        Destroy(this.gameObject);
    }
    IEnumerator DelayBossBlackAttack()
    {
        yield return new WaitForSeconds(0.5f);
        BossBackAttack();
    }
    #endregion
    public IEnumerator RegenShield()
    {
        _shield = maxShield;
        shieldRegenMode = true;
        while (shieldSlider.value != _shield)
        {
            shieldSlider.value += Time.deltaTime * dataMoster.speedRegenShield;
            if (shieldSlider.value >= _shield)
            {
                shieldSlider.value = _shield;
            }
            yield return true;
        }
        shieldRegenMode = false;
        haveSield = true;
    }
}


