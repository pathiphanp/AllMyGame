using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemyZone : MonoBehaviour
{
    [SerializeField] DataSkill dataSkill;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Takedamage>().Takedamage(dataSkill.damage,dataSkill.damageGuard,dataSkill.element,dataSkill);
        }
    }
}
