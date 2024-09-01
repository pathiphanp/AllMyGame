using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TypeUpgradeSkill
{
    MAXHP, DAMAGE, CHANCEPOINTLETTERSUP
}
public class ControlUpgrade : MonoBehaviour
{
    [Header("Upgrade MaxHP")]
    [SerializeField] Button btnUpgradeMaxHp;
    [SerializeField] TMP_Text levelMaxHPText;
    [SerializeField] TMP_Text costMaxHPText;
    int levelMaxHP = 1;
    [Header("Upgrade Damage")]
    [SerializeField] Button btnUpgradeDamage;
    [SerializeField] TMP_Text levelDamageText;
    [SerializeField] TMP_Text costDamageText;
    int levelDamage = 1;
    [Header("Upgrade ChancePointLettersUp")]
    [SerializeField] Button btnUpgradeChancePointLettersUp;
    [SerializeField] TMP_Text levelChancePointLettersUText;
    [SerializeField] TMP_Text costChancePointLettersHPText;
    int levelChancePointLettersUp = 1;
    [Header("Buy MaxHP")]
    [SerializeField] Button btnBuyHealPotion;
    [SerializeField] Button btnBuybuffDamgePotion;

    private void Start()
    {
        ControlGamePlay._instance.controlUpgrade = this;
        btnUpgradeMaxHp.onClick.AddListener(OnClickUpGradeMaxHP);
        btnUpgradeDamage.onClick.AddListener(OnClickUpGradeDamage);
        btnUpgradeChancePointLettersUp.onClick.AddListener(OnClickUpGradeChancePointLettersUp);
        btnBuyHealPotion.onClick.AddListener(OnClickBuyHealPotion);
        btnBuybuffDamgePotion.onClick.AddListener(OnClickBuybuffDamgePotion);
    }

    private void OnClickBuybuffDamgePotion()
    {
        if (ControlGamePlay._instance.controlPlayer.myCrystal >= 800)
        {
            ControlGamePlay._instance.controlPlayer.myCrystal -= 800;
            ControlGamePlay._instance.controlPlayer.AddBuffDamagePotion();
            ControlGamePlay._instance.controlPlayer.RestMyCrystal();
        }
    }

    private void OnClickBuyHealPotion()
    {
        if (ControlGamePlay._instance.controlPlayer.myCrystal >= 500)
        {
            ControlGamePlay._instance.controlPlayer.myCrystal -= 500;
            ControlGamePlay._instance.controlPlayer.AddHealPotion();
            ControlGamePlay._instance.controlPlayer.RestMyCrystal();
        }
    }

    private void OnClickUpGradeChancePointLettersUp()
    {
        UpgradeSkill(ref levelChancePointLettersUp, levelChancePointLettersUText, TypeUpgradeSkill.CHANCEPOINTLETTERSUP, costChancePointLettersHPText);
    }
    private void OnClickUpGradeDamage()
    {
        UpgradeSkill(ref levelDamage, levelDamageText, TypeUpgradeSkill.DAMAGE, costDamageText);
    }
    private void OnClickUpGradeMaxHP()
    {
        UpgradeSkill(ref levelMaxHP, levelMaxHPText, TypeUpgradeSkill.MAXHP, costMaxHPText);
    }

    void UpgradeSkill(ref int _LevelSkil, TMP_Text _textLevel, TypeUpgradeSkill _typeUpgrade, TMP_Text _costSkillText)
    {
        if (ControlGamePlay._instance.controlPlayer.myCrystal >= CostUpGradeSkill(_LevelSkil))
        {
            ControlGamePlay._instance.controlPlayer.myCrystal -= CostUpGradeSkill(_LevelSkil, _costSkillText);
            ControlGamePlay._instance.controlPlayer.RestMyCrystal();
            ControlGamePlay._instance.controlPlayer.UpgradeSkill(_typeUpgrade);
            _LevelSkil++;
            _textLevel.text = _LevelSkil.ToString();
            AudioManager._instance.PlaySFX("Upgrade");
        }
    }
    int CostUpGradeSkill(int _LevelSkil)
    {
        int _costUpgradeSkill = _LevelSkil;
        //cost upgrade value = Lv skill * 50
        _costUpgradeSkill *= 50;
        return _costUpgradeSkill;
    }
    int CostUpGradeSkill(int _LevelSkil, TMP_Text _costSkillText)
    {
        int _costUpgradeSkill = _LevelSkil;
        //cost upgrade value = Lv skill * 50
        _costUpgradeSkill *= 50;
        int nextcostUpgradSkill = _LevelSkil;
        nextcostUpgradSkill++;
        nextcostUpgradSkill *= 50;
        _costSkillText.text = nextcostUpgradSkill.ToString();
        return _costUpgradeSkill;
    }

    public void ResetGamePlay()
    {
        levelMaxHPText.text = 1.ToString();
        costMaxHPText.text = 50.ToString();
        levelMaxHP = 1;
        levelDamageText.text = 1.ToString();
        costDamageText.text = 50.ToString();
        levelDamage = 1;
        levelChancePointLettersUText.text = 1.ToString();
        costChancePointLettersHPText.text = 50.ToString();
        levelChancePointLettersUp = 1;
    }
}
