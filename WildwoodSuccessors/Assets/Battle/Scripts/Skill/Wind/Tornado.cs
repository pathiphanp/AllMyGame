using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour, CancelSkill
{
    Collider2D coll;
    Animator anim;
    [SerializeField] Skill skill;
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject tornadoAnimation;

    [Header("Setting Target Pull")]
    [SerializeField] Transform rotationCenter;
    [SerializeField] GameObject targetPull;
    [SerializeField] float rotationRadius = 2f;
    [SerializeField] float angularSpeed = 2f;
    float posX, posY, angle = 0f;
    [Header("List Enemy")]
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] float maxDurationPull;
    float _durationPull;
    float _AdddurationPull;
    [Header("DataSkill")]
    DataSkill dataSkill;
    PlayerCombat player;
    bool onSkill = false;
    [Header("Sound")]
    [SerializeField] AudioSource sfx;
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
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = tornadoAnimation.GetComponent<Animator>();
    }
    public void CastSkill(PlayerCombat _player)
    {
        if (!onSkill)
        {
            onSkill = true;
            coll.enabled = true;
            sfx.volume = AudioManager.Instance.sfxSource.volume;
            AudioManager.Instance.PlaySFXInObject(sfx, "Tornado");
            transform.position = spawnPosition.position;
            _durationPull = dataSkill.durationSkill;
            StartCoroutine(DurationLifeTornad());
            tornadoAnimation.SetActive(true);
            anim.Play("Spawn");
        }
        player = _player;
        TimePullUpdate();
        FollowMose();
        TargetMoveAround();
    }
    void Update()
    {
        TargetMoveAround();
    }
    void TargetMoveAround()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        targetPull.transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;
        if (angle >= 360f)
        {
            angle = 0;
        }
    }
    void TimePullUpdate()
    {
        _durationPull -= Time.deltaTime;
        if (_durationPull >= maxDurationPull)
        {
            _AdddurationPull = maxDurationPull;
        }
        else
        {
            _AdddurationPull = _durationPull;
        }
    }
    void FollowMose()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = Vector2.MoveTowards(transform.position, mousePos, dataSkill.moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject _targetPull = null;
            Enemy enemySp = other.GetComponent<Enemy>();
            enemySp.sprite.sortingLayerName = "OnTop";
            // Random Knockback Position
            Vector2 knockbackDirection = new Vector2(1f, 0.9f);
            AddPull _enemyP = other.gameObject.GetComponent<AddPull>();
            if (enemySp.BossMode)
            {
                _targetPull = this.gameObject;
            }
            else
            {
                _targetPull = targetPull;
            }
            _enemyP.AddPull(_targetPull, dataSkill.pullSpeed, _AdddurationPull, true
            , knockbackDirection, dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
            StartCoroutine(DodamageControl(other.gameObject));
        }
    }
    IEnumerator DurationLifeTornad()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        CancelSkill();
        yield return new WaitForSeconds(0.26f);
        tornadoAnimation.SetActive(false);
    }
    IEnumerator DodamageControl(GameObject enemy)
    {
        Coroutine atk = StartCoroutine(DoDamage(enemy));
        Enemy enemySp = enemy.GetComponent<Enemy>();
        enemySp.coll.enabled = false;
        yield return new WaitForSeconds(_AdddurationPull);
        enemySp.sprite.sortingLayerName = "Mid";
        enemySp.coll.enabled = true;
        enemySp.pullTarget = null;
        enemy.GetComponent<Takedamage>().Takedamage(dataSkill.fallDamage, dataSkill.damageGuard, dataSkill.element, dataSkill);
        StopCoroutine(atk);
    }
    IEnumerator DoDamage(GameObject enemy)
    {
        bool end = false;
        while (!end)
        {
            if (enemy.activeSelf)
            {
                enemy.GetComponent<Takedamage>().Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element, dataSkill);
            }
            else
            {
                end = true;
            }
            yield return new WaitForSeconds(dataSkill.delayDamage);
        }
        yield break;
    }
    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        StopAllCoroutines();
        AnimationClip[] clip = anim.runtimeAnimatorController.animationClips;
        StartCoroutine(OffTornado(clip[2].length));
        anim.Play("Die");
        coll.enabled = false;
        onSkill = false;
        player.CancelCombo();
    }
    IEnumerator OffTornado(float time)
    {
        yield return new WaitForSeconds(time);
        tornadoAnimation.SetActive(false);
    }
}
