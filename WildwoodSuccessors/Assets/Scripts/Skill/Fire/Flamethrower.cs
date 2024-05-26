using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    Collider2D coll;
    Animator anim;
    [Header("Animationt")]
    [SerializeField] float delaySpawnFire;
    List<GameObject> fireAnimation = new List<GameObject>();
    int indexSpawnfire;
    [Header("Status")]
    bool checkLimit;
    [Header("EnemyList")]
    List<GameObject> enemyList = new List<GameObject>();
    [Header("Player Control")]
    PlayerCombat playerCombat;
    [Header("Data Skill")]
    DataSkill dataSkill;
    [Header("ClampY")]
    [SerializeField] float maxY;
    [SerializeField] float minY;
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
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            fireAnimation.Add(transform.GetChild(i).gameObject);
        }
    }
    IEnumerator SpawnFire(bool spawnFire)
    {
        if (spawnFire)
        {
            anim.Play("Fire");
            for (int i = 0; i < fireAnimation.Count; i++)
            {
                fireAnimation[i].SetActive(spawnFire);
                indexSpawnfire = i;
                yield return new WaitForSeconds(delaySpawnFire);
            }
        }
        else
        {
            anim.Play("None");
            for (int i = indexSpawnfire; i >= 0; i--)
            {
                fireAnimation[i].SetActive(spawnFire);
                yield return new WaitForSeconds(delaySpawnFire);
            }
            coll.enabled = false;
        }
    }
    public void CastSkill(PlayerCombat _playerCombat)
    {
        if (!checkLimit)
        {
            checkLimit = true;
            coll.enabled = true;
            playerCombat = _playerCombat;
            StartCoroutine(DelayDamage());
            StartCoroutine(LimitTimeFlamethorwer());
            StartCoroutine(SpawnFire(true));
            AudioManager.Instance.PlaySFXInObject(sfx, "Flamethrower");
        }
        else
        {
            FollowMose();
        }
    }
    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        StopAllCoroutines();
        StartCoroutine(SpawnFire(false));
        playerCombat.CancelCombo();
        checkLimit = false;
    }
    void FollowMose()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // ตั้งค่า z เป็น 0 เพื่อให้เป็นตำแหน่งในโลก 2D
                         // หามุมหมุนที่ต้องการในแกน Z
                         //float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
                         // ทำให้ GameObject มองที่ตำแหน่งของเมาส์เฉพาะแกน Z
                         //transform.up = Quaternion.Euler(0, 0, angle) * Vector3.right;
        mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, mousePos.y, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject remove = RemoveEnemyFromList.Instance.Remove(enemyList, other.gameObject);
        enemyList.Remove(remove);
    }
    IEnumerator DelayDamage()
    {
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                Takedamage enemyT = enemyList[i].GetComponent<Takedamage>();
                enemyT.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
                if (dataSkill.tier == TierSkill.Tire3)
                {
                    AddSlow enemyS = enemyList[i].GetComponent<AddSlow>();
                    enemyS.AddSlow(dataSkill.delayDamage, true, dataSkill.percentSlow);
                }
            }
        }
        yield return new WaitForSeconds(dataSkill.delayDamage);
        StartCoroutine(DelayDamage());
    }
    IEnumerator LimitTimeFlamethorwer()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        CancelSkill();
    }
}
