using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ControlUiSkill : MonoBehaviour
{
    [Header("Skill")]
    [SerializeField] GameObject uiSkill;
    [SerializeField] GameObject centralObject;
    [SerializeField] GameObject[] skillObject;
    public float radius; // รัศมีระยะห่างจากจุดศูนย์กลาง
    int numberOfObjects; // จำนวนอ็อบเจ็กต์ที่ต้องการหมุนรอบ ๆ
    public int countUiSkilloff;
    [Header("UI Description")]
    [SerializeField] GameObject infoSkill;
    [SerializeField] TMP_Text nameSkillText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text effectText;
    [SerializeField] TMP_Text descriptionText;

    void Start()
    {
        centralObject.GetComponent<SkillObject>().controlUiSkill = this;
    }
    public void ShowSkill(Vector3 _target)
    {
        centralObject.transform.position = _target;
        foreach (GameObject sOg in skillObject)
        {
            sOg.transform.position = _target;
        }
        uiSkill.SetActive(true);
        OpenSkill();
    }
    public void OpenSkill()
    {
        numberOfObjects = ControlGamePlay._instance.controlPlayer.skillsCanUse.Count;
        ControlGamePlay._instance.canSelectPart = false;
        float angleStep = 360f / numberOfObjects;
        Vector3 positionStart = centralObject.transform.position;
        Vector3 targetMove;
        SkillObject _skillObj;
        for (int i = 0; i < numberOfObjects; i++)
        {
            centralObject.transform.position = positionStart + new Vector3(radius, 0, 0);
            centralObject.transform.RotateAround(positionStart, Vector3.forward, i * angleStep);
            targetMove = centralObject.transform.position;
            _skillObj = skillObject[i].GetComponent<SkillObject>();
            _skillObj.controlUiSkill = this;
            _skillObj.skill = ControlGamePlay._instance.controlPlayer.skillsCanUse[i];
            _skillObj.MoveToTarget(targetMove, false, true);
        }
        centralObject.transform.position = positionStart;
    }

    public void CancelSkill(bool _resetSelect)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            skillObject[i].GetComponent<SkillObject>().MoveToTarget(centralObject.transform.position, true, _resetSelect);
        }
    }

    public void CheckOffAllUiSkill(bool _resetSelect)
    {
        countUiSkilloff++;
        if (countUiSkilloff == numberOfObjects)
        {
            uiSkill.SetActive(false);
            if (ControlGamePlay._instance.partPlayerSelect != null)
            {
                ControlGamePlay._instance.partPlayerSelect.GetComponent<ControlPart>().StopBlinkEffect();
                if (_resetSelect)
                {
                    ControlGamePlay._instance.canSelectPart = true;
                }
            }
            countUiSkilloff = 0;
        }
    }

    public void ShowInfoSkill(string _nameSkill, string _damage, string _effect, string _cooldown, string _description)
    {
        nameSkillText.text = _nameSkill;
        damageText.text = _damage;
        effectText.text = _effect;
        descriptionText.text = _description;
        infoSkill.SetActive(true);
    }
    public void OffInfoSkill()
    {
        infoSkill.SetActive(false);
    }

}
