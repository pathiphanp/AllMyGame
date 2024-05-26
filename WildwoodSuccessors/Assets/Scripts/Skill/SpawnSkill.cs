using System.Collections;
using UnityEngine;


public class SpawnSkill : MonoBehaviour, CancelSkill
{
    [Header("ModeSkill")]
    [SerializeField] bool onHold;
    [Header("HoldSkill")]
    [SerializeField] bool notLimittime;
    [Header("Next Spawn Skill Rnd")]
    [SerializeField] bool next;
    [Header("Spawn Skill At Mouse Position")]
    [SerializeField] bool atMouse;
    [Header("Clamp Position")]
    [SerializeField] Vector3 ClampPositionMax;
    [SerializeField] Vector3 ClampPositionMin;
    Vector3 rndPosition;
    Vector3 mousePos;
    [Header("Skill")]
    [SerializeField] GameObject objectSkill;
    [SerializeField] GameObject targetSkill;
    bool castSkill = true;
    bool canSpawn = true;
    [Header("GameSpawn")]
    [HideInInspector] public GameObject instan;
    [Header("PlayerCombat")]
    [HideInInspector] public PlayerCombat playerCombat;
    [Header("DataSkill")]
    [SerializeField] Skill skill;
    DataSkill dataSkill;
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
    public void CastSkill(Vector3 _mousePos)
    {
        if (onHold)
        {
            if (!onHold && castSkill)
            {
                onHold = true;
                if (!notLimittime)
                {
                    StartCoroutine(CountDurationHoldSkill());
                }
            }
            if (onHold && canSpawn)
            {
                canSpawn = false;
                StartCoroutine(DelaySpawnHoldSkill());
            }
        }
        else
        {
            mousePos = _mousePos;
            StartCoroutine(DelaySpawn());
        }
    }
    public void CancelSkill()
    {
        playerCombat.CancelCombo();
    }
    IEnumerator CountDurationHoldSkill()
    {
        //Duration hold Skill
        yield return new WaitForSeconds(dataSkill.durationSkill);
        CancelSkill();
    }
    void Spawn()
    {
        #region Random Position
        //Random Position
        if (mousePos != Vector3.zero)
        {
            rndPosition = mousePos;
            if (next)
            {
                mousePos = Vector3.zero;
            }
        }
        else
        {
            float x = UnityEngine.Random.Range(ClampPositionMin.x, ClampPositionMax.x);
            float y = UnityEngine.Random.Range(ClampPositionMin.y, ClampPositionMax.y);
            rndPosition = new Vector3(x, y, 0);
        }
        #endregion
        if (targetSkill != null)//Spawn Skill MoveToTarget
        {
            GameObject target = Instantiate(targetSkill, rndPosition, targetSkill.transform.localRotation);
            SkillMoveToTarget _skillMove = Instantiate(objectSkill, transform.localPosition,
            objectSkill.transform.localRotation).GetComponent<SkillMoveToTarget>();
            _skillMove.spawn = this;
            _skillMove.target = target.transform;
            _skillMove.dataSkill = dataSkill;
        }
        else if (targetSkill == null)//Spawn Other Object
        {

            GameObject _instan = Instantiate(objectSkill, rndPosition, objectSkill.transform.rotation);
            instan = _instan;
            if (instan.GetComponent<AddDataSkill>() != null)
            {
                AddDataSkill _dataSkill = instan.GetComponent<AddDataSkill>();
                _dataSkill.AddDataSkill(dataSkill);
            }
            if (atMouse)
            {
                instan.transform.position = rndPosition;
            }
        }
    }
    IEnumerator DelaySpawnHoldSkill()
    {
        Spawn();
        yield return new WaitForSeconds(dataSkill.delaySpawnSkill);
        canSpawn = true;
    }
    IEnumerator DelaySpawn()
    {
        //SpawnSkill amount
        for (int i = 0; i < dataSkill.amount; i++)
        {
            Spawn();
            yield return new WaitForSeconds(dataSkill.delaySpawnSkill);
        }
    }


}
