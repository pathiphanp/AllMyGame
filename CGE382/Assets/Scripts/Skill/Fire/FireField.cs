using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : MonoBehaviour
{
    Collider2D coll;
    List<Takedamage> enemyList = new List<Takedamage>();
    public DataSkill dataSkill;
    void Start()
    {
        coll = GetComponent<Collider2D>();
        StartCoroutine(Dodaamge());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Add(other.GetComponent<Takedamage>());
            other.gameObject.GetComponent<Takedamage>().Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Remove(other.GetComponent<Takedamage>());
        }
    }
    // IEnumerator Dodaamge()
    // {
    //     if (enemyList.Count > 0)
    //     {
    //         for (int i = 0; i < enemyList.Count; i++)
    //         {
    //             enemyList[0].Takedamage(dataSkill.damage,dataSkill.damageGuard,dataSkill.element);
    //         }
    //     }
    //     yield return new WaitForSeconds(dataSkill.delayDamage);
    //     StartCoroutine(Dodaamge());
    // }
    IEnumerator Dodaamge()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(dataSkill.delayDamage);
        coll.enabled = true;
        yield return new WaitForSeconds(0.001f);
        StartCoroutine(Dodaamge());
    }
}
