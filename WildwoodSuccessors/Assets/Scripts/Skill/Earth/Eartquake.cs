using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eartquake : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    Collider2D coll;
    [Header("Camera Chake")]
    [SerializeField] CinemachineShake cinemachineShake;
    [SerializeField] float shakePower;
    bool onChake = true;
    [Header("Enemy")]
    [SerializeField] List<GameObject> enemyList;
    [Header("Player")]
    PlayerCombat playerCombat;
    DataSkill dataSkill;
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
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    IEnumerator LimitHoldSkill()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        playerCombat.CancelCombo();
        CancelSkill();
    }
    public void CastSkill(PlayerCombat _playerCombat)
    {
        if (onChake)
        {
            onChake = false;
            AudioManager.Instance.PlaySFX("StartEarthquake");
            AudioManager.Instance.PlaySFXInObject(sfx, "LoopEarthquake");
            playerCombat = _playerCombat;
            //PlaySoundEffect
            cinemachineShake.StartShakeCamera(shakePower);
            StartCoroutine(LimitHoldSkill());
            coll.enabled = true;
            StartCoroutine(DelayDamage());
        }
    }

    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        AudioManager.Instance.PlaySFX("EndEarthquake");
        StopAllCoroutines();
        cinemachineShake.StopShakeCamera();
        enemyList.Clear();
        coll.enabled = false;
        onChake = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }
    IEnumerator DelayDamage()
    {
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                AddStun enemyS = enemyList[i].gameObject.GetComponent<AddStun>();
                enemyS.AddStun(dataSkill.durationStun, false);
                Takedamage enemyT = enemyList[i].gameObject.GetComponent<Takedamage>();
                enemyT.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
                if (enemyList[i].activeSelf == false)
                {
                    enemyList.Remove(enemyList[i]);
                }
            }
        }
        yield return new WaitForSeconds(dataSkill.delayDamage);
        StartCoroutine(DelayDamage());
    }
}
