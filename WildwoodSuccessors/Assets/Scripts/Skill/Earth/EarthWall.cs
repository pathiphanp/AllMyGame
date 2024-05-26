using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthWall : SkillWave, TakedamageS
{
    [SerializeField] List<Animator> animWall;
    [Header("Status")]
    [SerializeField] int _hpA;
    bool onDie = false;
    [SerializeField] Slider hpSlider;
    Coroutine showHp;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<Animator>() != null)
            {
                animWall.Add(transform.GetChild(i).gameObject.GetComponent<Animator>());
            }
        }
    }
    public override void CastSkill()
    {
        StartCoroutine(WallDuration());
        StartCoroutine(SpawnWallWave());
        coll.enabled = true;
        onDie = false;
        WallReset();
    }
    IEnumerator SpawnWallWave()
    {
        foreach (Animator w in animWall)
        {
            AudioManager.Instance.PlaySFX("Rock");
            w.Play("Spawn");
            yield return new WaitForSeconds(0.05f);
        }
        canDodamage = false;
    }
    IEnumerator WallDuration()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        StartCoroutine(CancelWall());
    }
    IEnumerator CancelWall()
    {
        onDie = true;
        coll.enabled = false;
        //Die
        foreach (Animator w in animWall)
        {
            w.Play("DieNotDestroy");
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        //Explode
        foreach (Animator w in animWall)
        {
            AudioManager.Instance.PlaySFX("Earth Explode");
            w.Play("ExplodeNotDestroy");
            yield return new WaitForSeconds(0.05f);
        }
        WallReset();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    public GameObject Takedamage(int damage, GameObject returnTarget)
    {
        if (onDie)
        {
            returnTarget = null;
        }
        else
        {
            _hpA -= damage;
            if (_hpA <= 0)
            {
                StartCoroutine(CancelWall());
                returnTarget = null;
            }
            else
            {
                if (showHp != null)
                {
                    StopCoroutine(showHp);
                    showHp = null;
                }
                showHp = StartCoroutine(ShowHpSlider());
            }
        }
        return returnTarget;
    }
    void WallReset()
    {
        hpSlider.maxValue = dataSkill.maxHp;
        _hpA = dataSkill.maxHp;
    }
    IEnumerator ShowHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.value = _hpA;
        yield return new WaitForSeconds(1);
        hpSlider.gameObject.SetActive(false);
    }
}
