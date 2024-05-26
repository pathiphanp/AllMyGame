using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;


public class CardControl : MonoBehaviour
{
    DataUpgradeSkill dataUpgradeSkill;
    [Header("Button")]
    [SerializeField] Button btn;
    [Header("Card")]
    public TMP_Text nameSkill;
    public VideoPlayer iconSkill;
    public TMP_Text description;
    [Header("UpgradeSkill")]
    DataSkill upgradeSkill;
    [Header("Skill Manager")]
    SkillManager skillManager;
    void Start()
    {
        btn.onClick.AddListener(OnClickChooseCard);
        skillManager = SkillManager.Instance;
        dataUpgradeSkill = GameObject.FindObjectOfType<DataUpgradeSkill>();
        // Debug.Log(iconSkill.clip.name);
    }
    void OnClickChooseCard()
    {
        skillManager.UpgradeDataSkill(upgradeSkill);
        dataUpgradeSkill.RemoveFromListUpgrade(upgradeSkill);
    }
    public void AddCardUpgrade(DataSkill _upgradeSkill)
    {
        upgradeSkill = _upgradeSkill;
        nameSkill.text = _upgradeSkill.nameSkill;
        if (_upgradeSkill.iconSkill != null)
        {
            iconSkill.clip = _upgradeSkill.iconSkill;
        }
        description.text = _upgradeSkill.description;
    }
    public void ClearCard()
    {
        upgradeSkill = null;
        nameSkill.text = "";
        description.text = "";
    }
}
