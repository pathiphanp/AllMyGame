using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpearWind : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    bool getSetting;
    [Header("ObjectPool")]
    [SerializeField] SpearWindObjectPool spearWindPool;
    [Header("Spawn")]
    int countSpawn;
    [Header("SpawnSpear")]
    int spearAmount;
    [Header("SpearClampSpawn")]
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;
    bool canSpawn = true;
    [Header("PlayerCombat")]
    PlayerCombat playerCombat;
    [Header("Data Skill")]
    [HideInInspector] public DataSkill dataSkill;
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
    public void CastSkill(PlayerCombat _playerCombat)
    {
        if (!getSetting)
        {
            getSetting = true;
            //Get value from control
            GetSetting();
        }
        if (playerCombat == null)
        {
            playerCombat = _playerCombat;
        }
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnSkill());
        }
        if (countSpawn == spearAmount)
        {
            CancelSkill();
        }
    }
    void GetSetting()
    {
        spearAmount = dataSkill.amount / 3;
    }
    IEnumerator SpawnSkill()
    {
        AudioManager.Instance.PlaySFX("SpearStart");
        //Count spear spawn
        countSpawn++;
        //Spawn
        SpearWind spearWind = spearWindPool.Pool.Get();
        //Add control
        #region RndPosition Spear
        float rndX = Random.Range(min.x, max.x);
        float rndY = Random.Range(min.y, max.y);
        Vector3 rndTranfrom = new Vector3(rndX, rndY, 0);
        spearWind.transform.position = rndTranfrom;
        // spearWind.controlSpearWind = controlSpearWind;
        #endregion
        yield return new WaitForSeconds(dataSkill.delaySpawnSkill);
        canSpawn = true;
    }
    public void CancelSkill()
    {
        playerCombat.CancelCombo();
        countSpawn = 0;
        getSetting = false;
        canSpawn = true;
        StopAllCoroutines();
    }
}
