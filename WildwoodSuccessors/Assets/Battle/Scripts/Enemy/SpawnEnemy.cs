using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    SLIME = 0, BAT = 1, SNAKE = 2
}
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] EnemyObjectPool enemyPool;
    [Header("Clamp Position")]
    [SerializeField] float clampX;
    [SerializeField] float rndYMax;
    [SerializeField] float rndYMin;
    [SerializeField] float delaySpawn;
    [SerializeField] float scaleDelaySpawn;
    [Header("Monster")]
    [SerializeField] DataMonster[] dataEnemy;
    [SerializeField] GameObject[] allboss;
    [HideInInspector] GameObject boss;
    [Header("Warning")]
    [SerializeField] GameObject bossWarning;
    [Header("Boss")]
    Coroutine bossCallSpawn;
    bool bossMode;
    [Header("DestroyMonsterArea")]
    [SerializeField] GameObject destroyMonsterArea;
    [Header("TestSetting")]
    [SerializeField] bool spawnBoss;
    [SerializeField] bool canSpawn;
    void Start()
    {
        //GameManagerGameplay.Instance.spawnEnemy = this;
        if (canSpawn)
        {
            if (spawnBoss)
            {
                CallSpawnBoss();
            }
            else
            {
                StartCoroutine(CallSpawnMonster());
            }
        }

    }
    public void StartBattle()
    {
        StartCoroutine(CallSpawnMonster());
        CallSpawnBoss();
    }
    public void StopSpawnMonster()
    {
        StopAllCoroutines();
    }
    IEnumerator CallSpawnMonster()
    {
        Enemy enemy = enemyPool.Pool.Get();
        int rndDataEnemy = Random.Range(0, dataEnemy.Length);
        enemy.dataMoster = dataEnemy[rndDataEnemy];
        enemy.SetUpMonster();
        float rndY = Random.Range(rndYMin, rndYMax);
        Vector2 rndSpawnPosition = new Vector2(clampX, rndY);
        enemy.transform.position = rndSpawnPosition;
        yield return new WaitForSeconds(delaySpawn);
        StartCoroutine(CallSpawnMonster());
    }
    IEnumerator CallSpawnMoster(float delaySpawn, MonsterType enemyNormal)
    {
        Enemy enemy = enemyPool.Pool.Get();
        float rndY = Random.Range(rndYMin, rndYMax);
        enemy.dataMoster = dataEnemy[(int)enemyNormal];
        enemy.SetUpMonster();
        Vector2 rndSpawnPosition = new Vector2(clampX, rndY);
        enemy.transform.position = rndSpawnPosition;
        yield return new WaitForSeconds(delaySpawn);
        if (bossMode)
        {
            bossCallSpawn = StartCoroutine(CallSpawnMoster(delaySpawn, enemyNormal));
        }
        else
        {
            StartCoroutine(CallSpawnMoster(delaySpawn, enemyNormal));
        }
    }
    public void CallSpawnBoss()
    {
        StartCoroutine(StartCallSpawnBoss());
    }
    IEnumerator StartCallSpawnBoss()
    {
        bool offOn = true;
        for (int i = 0; i < 5; i++)
        {
            bossWarning.SetActive(offOn);
            offOn = !offOn;
            yield return new WaitForSeconds(0.5f);
        }
        bossWarning.SetActive(false);
        if (boss == null)
        {
            int rndBoss = Random.Range(0, allboss.Length);
            boss = Instantiate(allboss[rndBoss]);
            float rndY = Random.Range(rndYMin, rndYMax);
            Vector2 rndSpawnPosition = new Vector2(clampX, rndY);
            boss.transform.position = rndSpawnPosition;
        }
        else
        {
            boss.gameObject.SetActive(true);
            float rndY = Random.Range(rndYMin, rndYMax);
            Vector2 rndSpawnPosition = new Vector2(clampX, rndY);
            boss.transform.position = rndSpawnPosition;
        }
    }
    public void DestroyAllMoster()
    {
        Debug.Log("DestroMonster");
        destroyMonsterArea.SetActive(true);
        if (boss != null)
        {
            boss.GetComponent<Enemy>().BossSleep();
        }
        StartCoroutine(DelayDestroyMonsterArea());
    }
    IEnumerator DelayDestroyMonsterArea()
    {
        yield return new WaitForSeconds(3);
        destroyMonsterArea.SetActive(false);
    }
}
