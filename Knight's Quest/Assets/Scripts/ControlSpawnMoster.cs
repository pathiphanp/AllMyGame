using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpawnMoster : MonoBehaviour
{
    [SerializeField] GameObject[] MonsterPrefab;
    [SerializeField] GameObject spawnPosition;
    [SerializeField] GameObject monsterTargetMove;
    [SerializeField] int monsterSpeed;

    [Header("SetUpMonster")]
    [SerializeField] int hpMonster;
    [SerializeField] int damageMonster;
    [SerializeField] int crystalMonster;
    [SerializeField] public GameObject[] skillText;
    int countMosterDie;
    void Start()
    {
        ControlGamePlay._instance.controlSpawnMoster = this;
    }

    public void SpawMonster()
    {
        GameObject _monster;
        int rndMonster = UnityEngine.Random.Range(0, MonsterPrefab.Length - 1);
        _monster = Instantiate(MonsterPrefab[rndMonster], spawnPosition.transform.position
        , MonsterPrefab[rndMonster].transform.rotation);
        _monster.GetComponent<Monster>().MoveToPositionTarget(monsterTargetMove, monsterSpeed);
        _monster.GetComponent<Monster>().SetMonster(SetUpName(rndMonster), hpMonster, damageMonster, crystalMonster, this);
    }
    string SetUpName(int _enemyIndex)
    {
        string _nameMonster = "";
        if (_enemyIndex == 0)
        {
            _nameMonster = "ORC'HAMMER";
        }
        if (_enemyIndex == 1)
        {
            _nameMonster = "ORC'AXE";
        }
        if (_enemyIndex == 2)
        {
            _nameMonster = "ORC'SWORD";
        }
        return _nameMonster;
    }


    public void MonsterDie()
    {
        #region  ScaleMonster
        countMosterDie++;
        if (countMosterDie == 5)
        {
            countMosterDie = 0;
            hpMonster += 50;
            damageMonster += 5;
            crystalMonster += 100;
        }
        #endregion
        ControlGamePlay._instance.BreakTurn();
    }

    public void ResetGamePlay()
    {
        countMosterDie = 0;
        hpMonster = 50;
        damageMonster = 5;
        crystalMonster = 100;
    }
}
