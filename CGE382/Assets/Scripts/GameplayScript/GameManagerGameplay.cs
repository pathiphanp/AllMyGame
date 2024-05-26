using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManagerGameplay : MonoBehaviour
{
    //Input system
    public PlayerCombat inputInCombat;
    public PlayerFarming inputInFarming;

    //Spawn enemy
    public SpawnEnemy spawnEnemy;

    //Upgrade
    public DataUpgradeSkill upgradeSkill;
    public Element element;

    public static event Action disableMonster;
    private void Start()
    {
        #region Use in put. Check by time cycle
        if (GameManagerPor.Instance.timeCycle.hours >= 20 || GameManagerPor.Instance.timeCycle.hours < 6)
        {
            inputInCombat.enabled = true;
            inputInFarming.enabled = false;
        }

        if (GameManagerPor.Instance.timeCycle.hours >= 6)
        {
            inputInCombat.enabled = false;
            inputInFarming.enabled = true;
        }
        #endregion
    }

    private void Update()
    {
        if (spawnEnemy != null)
        {
            TimeManagementBetweenTwoProject();
        }
    }

    public void GotoNight()
    {
        inputInFarming.enabled = false;
        inputInCombat.enabled = true;
    }

    public void GotoDay()
    {
        inputInCombat.SetHpTower();
        spawnEnemy.DestroyAllMoster();
        inputInFarming.enabled = true;
        inputInCombat.enabled = false;
    }

    public void SpawnEnemy()
    {
        StartCoroutine(DelaySpawnEnemy());
    }

    public void TimeManagementBetweenTwoProject()
    {
        #region Spawn enemy. Check by time cycle
        if (GameManagerPor.Instance.timeCycle.hours == 5)
        {
            spawnEnemy.StopSpawnMonster();
        }
        #endregion
    }

    IEnumerator DelaySpawnEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        spawnEnemy.StartBattle();
    }

    public void UpgradeSkillPopup()
    {
        element = (Element)UnityEngine.Random.Range(0, 3);
        upgradeSkill.CreateUpgradeSkill(element);
    }

    public static void StartDisableMonster()
    {
        disableMonster?.Invoke();
    }
    void OnDestroy()
    {
        Debug.Log("Destroy");
    }
}
