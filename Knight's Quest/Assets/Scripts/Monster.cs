using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private ControlSpawnMoster controlSpawnMoster;
    [SerializeField] Animator anim;

    [SerializeField] Slider hpSlider;

    [Header("DataMonster")]
    [SerializeField] GameObject positionSkillText;
    [SerializeField] float attackTime;
    [SerializeField] float dieTime;
    [Header("Status Monster")]
    string nameMonster;
    int hp;
    int damage;
    int boostDamage;
    int crystalDrop;
    int speedMove;
    bool canReduceDamage;
    int skill;
    string skillInfo;
    int skillPoint;
    GameObject skillShow;
    bool onDie = false;
    private void Start()
    {
        hpSlider.gameObject.SetActive(false);
    }
    public void SetMonster(string _nameMonster, int _hpMax, int _damage, int _crystal, ControlSpawnMoster _controlSpawnMoster)
    {
        nameMonster = _nameMonster;
        //get data
        hp = _hpMax;
        damage = _damage;
        crystalDrop = _crystal;
        controlSpawnMoster = _controlSpawnMoster;
        //set output
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        ControlGamePlay._instance.monster = this;
        int rndSkill = UnityEngine.Random.Range(1, 4);
        skill = rndSkill;
        SkillInfo(skill);
        skillShow = Instantiate(controlSpawnMoster.skillText[skill - 1], positionSkillText.transform);
        skillShow.SetActive(false);
    }
    void SkillInfo(int skill)
    {
        if (skill == 1)
        {
            //Heal 
            skillInfo = " : ฮิล (maxHp * 0.2)";
        }
        if (skill == 2)
        {
            //Boost Damage
            skillInfo = " : เพิ่มดาเมจในการโจมตีครั้งถัดไป (damage * 0.2)";
        }
        if (skill == 3)
        {
            //Reduce Daamge
            skillInfo = " : ลดดาเมจที่ได้รับ (damage * 0.2)";
        }
    }
    public void MoveToPositionTarget(GameObject _positiontarget, int _monsterSpeed)
    {
        speedMove = _monsterSpeed;
        StartCoroutine(StartMoveToPositionTarget(_positiontarget));
    }
    IEnumerator StartMoveToPositionTarget(GameObject _positiontarget)
    {
        anim.Play("Walk");
        while (transform.position.x != _positiontarget.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, _positiontarget.transform.position, speedMove * Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
        anim.Play("Idel");
        hpSlider.gameObject.SetActive(true);
        ControlGamePlay._instance.controlEnemyInfo.OnEnemyInfo(nameMonster, (hpSlider.value + " / " + hpSlider.maxValue).ToString()
        , damage.ToString(), crystalDrop.ToString(), skillInfo);
        ControlGamePlay._instance.StartGamePlay();
    }
    public void TakeDamage(int _damage)
    {
        if (canReduceDamage)
        {
            canReduceDamage = false;
            _damage -= (int)(_damage * 0.2f);
        }
        hpSlider.value -= _damage;
        if (hpSlider.value <= 0)
        {
            hpSlider.gameObject.SetActive(false);
            anim.Play("Die");
            onDie = true;
            ControlGamePlay._instance.controlPlayer.AddMyCrystal(crystalDrop);
            StartCoroutine(Die());
        }
        else
        {
            ControlGamePlay._instance.controlEnemyInfo.UpdateHpMonster(hpSlider.value.ToString(), hpSlider.maxValue.ToString());
            anim.Play("Hurt");
        }
    }
    public void CountSkillMonster()
    {
        skillPoint++;
        if (skillPoint == 3)
        {
            StartCoroutine(UseSkill());
        }
        ControlGamePlay._instance.controlEnemyInfo.skillPoint(skillPoint.ToString());
    }
    IEnumerator UseSkill()
    {
        skillShow.SetActive(true);
        if (skill == 1)
        {
            //Heal 
            hpSlider.value += (hpSlider.maxValue * 0.2f);
        }
        if (skill == 2)
        {
            //Boost Damage
            boostDamage = (int)(damage * 0.2f);
        }
        if (skill == 3)
        {
            //Reduce Daamge
            canReduceDamage = true;
        }
        skillPoint = 0;
        yield return new WaitForSeconds(1f);
        skillShow.SetActive(false);
    }
    public void MosterTurn()
    {
        if (!onDie)
        {
            StartCoroutine(StartMosterTurn());
        }
    }
    IEnumerator StartMosterTurn()
    {
        anim.Play("Attack");
        AudioManager._instance.PlaySFX("Hit");
        yield return new WaitForSeconds(attackTime);
        ControlGamePlay._instance.controlPlayer.TakeDamage(damage + boostDamage);
        boostDamage = 0;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(dieTime);
        controlSpawnMoster.MonsterDie();
        Destroy(this.gameObject);
    }
}
