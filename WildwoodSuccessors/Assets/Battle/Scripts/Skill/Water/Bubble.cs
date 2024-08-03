using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator anim;
    [HideInInspector] public GameObject target;
    Takedamage tartgetT;
    [Header("Bubble")]
    public GameObject bubble;
    [HideInInspector] public BubbleSpawn bubbleSpawn;
    [Header("DataSkill")]
    DataSkill dataSkill;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        dataSkill = bubbleSpawn.dataSkill;
        StartCoroutine(Dodamage());
    }
    IEnumerator Dodamage()
    {
        if (target != null)
        {
            //ระหว่างอยู่กลางอากาศ จะโดนดาเมจเลื่อยๆ
            if (tartgetT == null)
            {
                tartgetT = target.GetComponent<Takedamage>();
            }
            tartgetT.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            if (target == null)
            {
                bubbleSpawn.RemoveBubbleFromList(this);
                Die();
                yield break;
            }
            yield return new WaitForSeconds(dataSkill.delayDamage);
            StartCoroutine(Dodamage());
        }
    }
    public void Die()
    {
        StopAllCoroutines();
        AudioManager.Instance.PlaySFX("Bubble ex");
        anim.Play("Die");
    }
    void FallDamage()
    {
        //จากนั้นเล่นAnimation ลอยและตกลงมาทำดาเมจปิดท้าย
        if (target != null)
        {
            target.transform.SetParent(null);
            AddBubble targetB = target.GetComponent<AddBubble>();
            targetB.CancelBubble();
            tartgetT.Takedamage(dataSkill.fallDamage, dataSkill.damageGuard, dataSkill.element,dataSkill);
        }
        Destroy(this.gameObject);
    }
}
