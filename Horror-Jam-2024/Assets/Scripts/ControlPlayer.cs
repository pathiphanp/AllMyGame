using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;


    [Header("Part")]
    [SerializeField] public PartData[] parts;
    [Header("Part Status")]
    bool canUseWeapon;
    bool canUseShield;
    int countLeg = 0;

    [Header("PartsRespawn")]
    List<RespawnParts> listRespawnParts = new List<RespawnParts>();

    [Header("Skill ")]
    [Header("Skill Attack")]
    [SerializeField] public Skill[] dargonSkills;
    [SerializeField] public Skill[] axeSkills;
    [SerializeField] public Skill[] swordSkills;
    [HideInInspector] public List<Skill> skillsCanUse;
    [Header("Skill Defend")]
    [SerializeField] public Skill skillDefend;
    PartData shieldPart;
    [HideInInspector] public bool useShiled;
    [Header("Skill Idel")]
    [SerializeField] public Skill skillIdel;


    [SerializeField] public GameObject positionAttack;
    [SerializeField] public Vector3 positionIdel;

    [Header("Debuff")]
    [HideInInspector] public bool isStun = false;

    bool onClickShowUI = false;
    Coroutine callShowStatusPlayer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        positionIdel = transform.position;
        SetUpPart();
    }
    //Check Part เช็คว่ามีชิ้นส่วนไหนบ้างทำแอ็คชั่นไหนได้บ้าง
    //Check arm leg weapon 
    void SetUpPart()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].hpPart = parts[i].part.hp;
        }
    }
    public void CheckPart()
    {
        StartCoroutine(StartCheckPart());
    }
    IEnumerator StartCheckPart()
    {
        countLeg = 0;
        canUseWeapon = false;
        canUseShield = false;
        skillsCanUse.Clear();
        //Regeneration Part
        RegenerationPart();
        #region Check Part can Use
        foreach (PartData pD in parts)
        {
            //Check can use Weapon
            if (pD.namePart == "ArmLeft")
            {
                //can use skill dargon bite and Weapon
                if (pD.canUsePart)
                {
                    canUseWeapon = true;
                    ControlGamePlay._instance.controlUI.attackBtn.gameObject.SetActive(true);
                    AddUseSkill(dargonSkills);
                }
            }
            //Check can use Weapon Axe
            if (pD.namePart == "Axe")
            {
                //can use Axe skill
                if (pD.canUsePart)
                {
                    AddUseSkill(axeSkills);
                }
            }
            //Check can use Weapon Sword
            if (pD.namePart == "Sword")
            {
                //can use Sword skill
                if (pD.canUsePart)
                {
                    AddUseSkill(swordSkills);
                }
            }
            //Check can use Shield
            if (pD.namePart == "Shield")
            {
                //can use Shield skill
                if (pD.canUsePart)
                {
                    canUseShield = true;
                    shieldPart = pD;
                    ControlGamePlay._instance.controlUI.defendBtn.gameObject.SetActive(true);
                }
            }
            foreach (PartData pD_P in parts)
            {
                foreach (TypePart tp_P in pD_P.part.typePart)
                {
                    if (tp_P == TypePart.Leg && pD_P.canUsePart)
                    {
                        countLeg++;
                    }
                }
            }
        }
        #endregion
        if (!canUseShield && !canUseWeapon)
        {
            Invoke("SkipTurn", 0.5f);
        }
        yield return true;
    }
    void SkipTurn()
    {
        ControlGamePlay._instance.PlayerEndTurn();
    }
    void AddUseSkill(Skill[] _Skill)
    {
        foreach (Skill sk in _Skill)
        {
            skillsCanUse.Add(sk);
        }
    }
    public void UseShield()
    {
        useShiled = true;
        foreach (PartData pD_P in parts)
        {
            pD_P.haveShield = true;
        }
    }
    public void TakeDamage(int _damage, EffectSkill _effectSkill, PartData _partTarget)
    {
        bool isWeakness = false;

        int chanceDodge = 10;
        if (useShiled && _effectSkill != EffectSkill.DodgeShields)
        {
            // Debug.Log("useShiled");
            useShiled = false;
            _partTarget = shieldPart;
            _partTarget.hpPart -= _damage;
            ControlGamePlay._instance.controlUI.ShowDamageUI(_damage, _partTarget.namePart);
        }
        else
        {
            foreach (TypePart tyP in _partTarget.part.typePart)
            {
                if (tyP == TypePart.Head)
                {
                    if (countLeg == 2)
                    {
                        chanceDodge += 80;
                    }
                    else if (countLeg == 1)
                    {
                        chanceDodge += 40;
                    }
                }
                if (tyP == TypePart.Leg)
                {
                    chanceDodge += 30;
                }
                if (tyP == TypePart.Weakness)
                {
                    isWeakness = true;
                }
            }
            //Chance To dodge
            int rndhitChance = Random.Range(0, 101);
            if (rndhitChance > chanceDodge)
            {
                _partTarget.hpPart -= _damage;
                ControlGamePlay._instance.controlUI.ShowDamageUI(_damage, _partTarget.namePart);
                if (_effectSkill == EffectSkill.Stun && !useShiled)
                {
                    isStun = true;
                }
            }
            else
            {
                ControlGamePlay._instance.controlUI.ShowDamageUI(0, "");
            }
        }
        if (_partTarget.hpPart <= 0)
        {
            _partTarget.hpPart = 0;
            if (!isWeakness)
            {
                _partTarget.canUsePart = false;
                AddPartsToRespawnStatus(_partTarget, 3);
            }
            else
            {
                ControlGamePlay._instance.PlayerDie();
            }
        }
    }

    public void AddPartsToRespawnStatus(PartData _partTarget, int _turnRespawn)
    {
        RespawnParts new_respawnParts = new RespawnParts();
        new_respawnParts.partData = _partTarget;
        new_respawnParts.turnRespawn = _turnRespawn;
        listRespawnParts.Add(new_respawnParts);
    }

    void RegenerationPart()
    {
        if (listRespawnParts.Count > 0)
        {
            foreach (RespawnParts spP in listRespawnParts)
            {
                spP.turnRespawn--;
                if (spP.turnRespawn == 0)
                {
                    spP.partData.hpPart = spP.partData.part.hp;
                    spP.partData.canUsePart = true;
                }
            }
        }
    }

    public PartData FindPartInPlayer(string _namePart)
    {
        foreach (PartData pD in parts)
        {
            if (pD.namePart == _namePart)
            {
                return pD;
            }
        }
        return null;
    }
    private void OnMouseEnter()
    {
        if (ControlGamePlay._instance.canShowPlayerStatus)
        {
            ControlGamePlay._instance.controlUI.ShowPlayerStatus();
            ControlGamePlay._instance.controlUI.SetShowPlayerStatus(true);
            if (callShowStatusPlayer != null)
            {
                StopCoroutine(callShowStatusPlayer);
            }
        }
    }
    private void OnMouseExit()
    {
        callShowStatusPlayer = StartCoroutine(DelayOffPlayerStatus());
    }
    IEnumerator DelayOffPlayerStatus()
    {
        yield return new WaitForSeconds(0.5f);
        ControlGamePlay._instance.controlUI.SetShowPlayerStatus(false);
        callShowStatusPlayer = null;
    }

    public void ReturnToIdel()
    {
        if (FindPartInPlayer("Shield").canUsePart)
        {
            spriteRenderer.sprite = skillIdel.spriteSkillHaveShield;
        }
        else
        {
            spriteRenderer.sprite = skillIdel.spriteSkillNotHaveShield;
        }
    }
}
