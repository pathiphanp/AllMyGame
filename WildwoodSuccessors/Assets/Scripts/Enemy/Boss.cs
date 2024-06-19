using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected SpawnEnemy spawnEnemy;
    [Header("SpawnEnemy")]
    [SerializeField] protected float durationSpawnMonster;
    [SerializeField] protected float delaySpawnMonster;
    [SerializeField] protected MonsterType monsterType;
    [Header("Shield")]
    [SerializeField] protected float durationRegenShield;
    [SerializeField] protected float durationStun;
    public virtual void Start()
    {
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }
    public virtual IEnumerator SpawnMonsterDuration(bool side)
    {
        SetMoveEnable(false);
        SetShield();
        StartCoroutine(RegenShield());
        yield return new WaitForSeconds(20);
        RandomPositionInStartPosition();
        sprite.flipX = side;
        SetMoveEnable(true);
        onAttack = false;
    }   
}
