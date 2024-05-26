using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    Collider2D coll;
    Animator anim;
    [Header("Damage")]
    bool canDodamage = true;
    bool onRain;
    [Header("Player Combat")]
    PlayerCombat playerCombat;
    [Header("DataSkill")]
    DataSkill dataSkill;
    [Header("Audio")]
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
    void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    public void CastSkill(PlayerCombat _playerCombat)
    {
        if (!onRain)
        {
            AudioManager.Instance.PlaySFXInObject(sfx, "Rain");
            onRain = true;
            anim.Play("RainOn");
            playerCombat = _playerCombat;
            StartCoroutine(RainDuration());
            coll.enabled = true;
        }
    }
    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        StopAllCoroutines();
        playerCombat.CancelCombo();
        canDodamage = true;
        anim.Play("RainOff");
        onRain = false;
        coll.enabled = false;
    }
    IEnumerator RainDuration()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        CancelSkill();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Add Damage
            Takedamage _enemy = other.GetComponent<Takedamage>();
            //Add Slow
            AddSlow _enemyS = other.GetComponent<AddSlow>();
            _enemy.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            _enemyS.AddSlow(dataSkill.durationSlow, true, dataSkill.percentSlow);
            coll.enabled = false;
            if (canDodamage)
            {
                canDodamage = false;
                StartCoroutine(DelayDamage());
            }
        }
    }
    IEnumerator DelayDamage()
    {
        yield return new WaitForSeconds(dataSkill.delayDamage);
        coll.enabled = true;
        canDodamage = true;
    }

}
