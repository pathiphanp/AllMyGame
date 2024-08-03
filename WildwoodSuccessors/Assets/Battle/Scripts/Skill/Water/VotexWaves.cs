using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class VotexWaves : MonoBehaviour, AddDataSkill
{
    Collider2D coll;
    Animator anim;
    //durationPull = 10
    //pullSpeed = -1
    //knockbackForce = -5
    float durationSkill;
    float speedOffSpawn = 10f;
    float targetScalePull;
    [Header("Enemy")]
    List<GameObject> enemyList;
    [Header("DataSkill")]
    DataSkill dataSkill;
    [Header("Audio")]
    [SerializeField] AudioSource sfx;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        enemyList = new List<GameObject>();
    }
    void Start()
    {
        durationSkill = dataSkill.durationSkill;
        StartCoroutine(DurationPull());
        StartCoroutine(DelayDamage());
    }
    void Update()
    {
        durationSkill -= Time.deltaTime;
    }
    IEnumerator Spawn()
    {
        bool scale = true;
        StartSound();
        while (scale)
        {
            targetScalePull = Mathf.Lerp(transform.localScale.x, dataSkill.size, Time.deltaTime * dataSkill.speedScaleSize);
            transform.localScale = new Vector3(targetScalePull, targetScalePull, targetScalePull);
            if (transform.localScale.x >= (dataSkill.size - 0.05))
            {
                scale = false;
            }
            yield return true;
        }
    }
    IEnumerator Die()
    {
        coll.enabled = false;
        bool scale = true;
        while (scale)
        {
            targetScalePull = Mathf.Lerp(transform.localScale.x, 0.00f, Time.deltaTime * speedOffSpawn);
            transform.localScale = new Vector3(targetScalePull, targetScalePull, targetScalePull);
            if (transform.localScale.x <= 0.1f)
            {
                scale = false;
            }
            yield return true;
        }
        Destroythis();
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            AddPull _enemyP = other.gameObject.GetComponent<AddPull>();
            _enemyP.AddPull(this.gameObject, dataSkill.pullSpeed, durationSkill, true,
            knockbackDirection, dataSkill.knockbackForce, dataSkill.durationKnockbackForce);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject remove = RemoveEnemyFromList.Instance.Remove(enemyList, other.gameObject);
            enemyList.Remove(remove);
        }
    }
    IEnumerator DelayDamage()
    {
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].GetComponent<Takedamage>() != null)
                {
                    enemyList[i].GetComponent<Takedamage>().Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element, dataSkill);
                }
            }
        }
        yield return new WaitForSeconds(dataSkill.delayDamage);
        StartCoroutine(DelayDamage());
    }
    IEnumerator DurationPull()
    {
        //Spawn Votex
        Coroutine _Spawn = StartCoroutine(Spawn());
        yield return new WaitForSeconds(dataSkill.durationSkill);
        //Stop Do Damage
        StopCoroutine(_Spawn);
        StartCoroutine(Die());
    }
    void Destroythis()
    {
        EndSound();
        Destroy(this.gameObject);
    }
    public void AddDataSkill(DataSkill _dataSkill)
    {
        dataSkill = _dataSkill;
    }
    public virtual void StartSound()
    {
        sfx.volume = AudioManager.Instance.sfxSource.volume;
        AudioManager.Instance.PlaySFXInObject(sfx, "VertexWave");
    }
    public virtual void EndSound()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
    }
}
