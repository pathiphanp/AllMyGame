using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour, TakedamageS
{
    Collider2D coll;
    Animator anim;
    public DataSkill dataSkill;
    [SerializeField] int hp;
    [SerializeField] bool immortal;
    [SerializeField] Slider hpSlider;
    Coroutine showHp;
    void Start()
    {
        AudioManager.Instance.PlaySFX("Rock");
        if (this.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
            anim.Play("Spawn");
        }
        coll = GetComponent<Collider2D>();
        hp = dataSkill.maxHp;
        hpSlider.maxValue = dataSkill.maxHp;

    }
    public GameObject Takedamage(int damage, GameObject returnTarget)
    {
        if (immortal)
        {

        }
        else
        {
            hp -= damage;
            if (hp <= 0)
            {
                coll.enabled = false;
                returnTarget = null;
                AudioManager.Instance.PlaySFX("Earth Explode");
                anim.Play("Die");
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
    void Destroythis()
    {
        Destroy(this.gameObject);
    }

    IEnumerator ShowHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.value = hp;
        yield return new WaitForSeconds(1);
        hpSlider.gameObject.SetActive(false);
    }
}
