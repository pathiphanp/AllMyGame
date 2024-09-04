using UnityEngine;
using TMPro;

public class ControlEnemyInfo : MonoBehaviour
{
    [SerializeField] GameObject enemyInfo;
    [SerializeField] TMP_Text nameMonsterValue;
    [SerializeField] TMP_Text hpMonsterVale;
    [SerializeField] TMP_Text damageMonsterVale;
    [SerializeField] TMP_Text crystalValue;
    [SerializeField] TMP_Text skillMonsterInfo;
    [SerializeField] TMP_Text skillMonsterPoint;

    private void Start()
    {
        ControlGamePlay._instance.controlEnemyInfo = this;
    }
    public void OnEnemyInfo(string _nameMonsterValue, string _hpMonsterValue, string _damageMonsterVale, string _crystalValue, string _skillMonster)
    {
        nameMonsterValue.text = _nameMonsterValue;
        hpMonsterVale.text = _hpMonsterValue;
        damageMonsterVale.text = _damageMonsterVale;
        crystalValue.text = _crystalValue;
        skillMonsterInfo.text = _skillMonster;
        skillMonsterPoint.text = 0 + " / 3";
    }
    public void UpdateHpMonster(string _NowHp, string _MaxHp)
    {
        hpMonsterVale.text = _NowHp + " / " + _MaxHp;
    }
    public void skillPoint(string _skillpoint)
    {
        skillMonsterPoint.text = _skillpoint + " / 3";
    }

}
