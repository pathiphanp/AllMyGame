using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    [SerializeField] public GameObject positionAttack;
    [HideInInspector] public Vector2 positionIdel;

    [Header("Part")]
    [SerializeField] PartData[] parts;


    [Header("Sprite Skill")]
    [SerializeField] Skill smallSwordSkill;
    [SerializeField] Skill bigSwordSkill;
    [SerializeField] Skill biteSkill;
    [SerializeField] Skill headButSkill;
    [SerializeField] Skill roarSkill;
    [SerializeField] Skill guardSkill;
    [SerializeField] Skill headIdelSkill;
    [SerializeField] Skill bodyIdelSkill;
    [SerializeField] Skill armTopIdelSkill;
    [SerializeField] Skill armUnderIdelSkill;

    [Header("Count Turn")]
    int countTurn;
    int countDog;

    private void Start()
    {
        positionIdel = transform.position;
        SetUpPart();
    }
    void SetUpPart()
    {
        foreach (PartData pD in parts)
        {
            if (pD.part != null)
            {
                pD.hpPart = pD.part.hp;

            }
            pD.haveShield = true;
        }
    }
    public void StartEnemyTurn()
    {
        PartData _armLeftPlayer = ControlGamePlay._instance.controlPlayer.FindPartInPlayer("ArmLeft");
        PartData _armRightPlayer = ControlGamePlay._instance.controlPlayer.FindPartInPlayer("Shield");
        PartData _body = ControlGamePlay._instance.controlPlayer.FindPartInPlayer("Body");
        string[] findPartPlayer = { "ArmLeft", "Shield", "LegLeft", "LegRight" };
        List<PartData> playerPartTarget = new List<PartData>();
        playerPartTarget.Clear();
        for (int i = 0; i < findPartPlayer.Length; i++)
        {
            if (ControlGamePlay._instance.controlPlayer.FindPartInPlayer(findPartPlayer[i]).canUsePart)
            {
                playerPartTarget.Add(ControlGamePlay._instance.controlPlayer.FindPartInPlayer(findPartPlayer[i]));
            }
        }
        PartData head = FindEnemyPart("Head");
        PartData body = FindEnemyPart("Body");
        PartData armLeftTop = FindEnemyPart("ArmLeftTop");
        PartData armRightTop = FindEnemyPart("ArmRightTop");
        PartData armRightUnder = FindEnemyPart("ArmRightUnder");
        PartData shield = FindEnemyPart("Shield");
        //Count turn
        countTurn++;
        if (countDog == 1)
        {
            countDog = 0;
        }
        //Check Extar Action
        if (countDog == 2)
        {
            //Use Skill Rip Arm *Desrtoy 2 arm
            if (_armRightPlayer.canUsePart)
            {
                ControlGamePlay._instance.EnemyAttack(9999, EffectSkill.None, _armRightPlayer);
            }
            if (_armRightPlayer.canUsePart)
            {
                ControlGamePlay._instance.EnemyAttack(9999, EffectSkill.None, _armLeftPlayer);
            }
            return;
        }
        else if (countTurn == 5)
        {
            bool canUseSkill = false;
            // Debug.Log("Use BigSword");
            if (armLeftTop.canUsePart)
            {
                armLeftTop.partObject.GetComponent<SpriteRenderer>().sprite = bigSwordSkill.spriteSkillHaveShield;
                canUseSkill = true;
            }
            if (armRightTop.canUsePart)
            {
                armLeftTop.partObject.GetComponent<SpriteRenderer>().sprite = bigSwordSkill.spriteSkillNotHaveShield;
                canUseSkill = true;
            }
            //Use Skill CutPart
            if (canUseSkill)
            {
                if (playerPartTarget.Count > 0)
                {
                    int rndPart = Random.Range(0, playerPartTarget.Count);
                    ControlGamePlay._instance.EnemyAttack(9999, EffectSkill.None, playerPartTarget[rndPart]);
                }
            }
            return;
        }
        else if (countTurn == 8)
        {
            // Debug.Log("Use Headbutt");
            head.partObject.SetActive(false);
            body.partObject.GetComponent<SpriteRenderer>().sprite = headButSkill.spriteSkillHaveShield;
            //Use Skill Headbutt
            ControlGamePlay._instance.EnemyAttack(10, EffectSkill.Stun, _body);
            countTurn = 0;
            return;
        }
        else
        {
            bool canUseSmallSword = false;
            if (armRightUnder.canUsePart)
            {
                canUseSmallSword = true;
            }
            //Choose Part in Player
            int countLegPlayer = 0;
            bool armLeft = false;
            List<PartData> _partTargetArmAndLeg = new List<PartData>();
            List<PartData> _partTargetHeadAndBody = new List<PartData>();
            PartData _partTargetHead = null;
            PartData _partTargetSelect = null;
            foreach (PartData pD_P in ControlGamePlay._instance.controlPlayer.parts)
            {
                //Check Leg Player and Count Leg Player
                foreach (TypePart tp_P in pD_P.part.typePart)
                {
                    if (tp_P == TypePart.Leg && pD_P.canUsePart)
                    {
                        _partTargetArmAndLeg.Add(pD_P);
                        countLegPlayer++;
                    }
                    if (tp_P == TypePart.Head && pD_P.canUsePart)
                    {
                        _partTargetHeadAndBody.Add(pD_P);
                    }
                    if (tp_P == TypePart.Body && pD_P.canUsePart)
                    {
                        _partTargetHead = pD_P;
                    }
                }
                //Check ArmLeft Player Can Use
                if (pD_P.namePart == "ArmLeft" && pD_P.canUsePart)
                {
                    _partTargetArmAndLeg.Add(pD_P);
                    armLeft = true;
                }
            }
            float[] weightsArmAndLeg = { 0.7f, 0.15f, 0.15f };
            float[] weightsLeg = { 0.5f, 0.5f };
            float[] weightsHeadAndBody = { 0.5f, 0.5f };
            if (countLegPlayer == 2)
            {
                if (armLeft)
                {
                    _partTargetSelect = RandomParts(_partTargetArmAndLeg, weightsArmAndLeg);
                }
                else
                {
                    _partTargetSelect = RandomParts(_partTargetArmAndLeg, weightsLeg);
                }
            }
            else if (countLegPlayer == 1)
            {
                _partTargetSelect = RandomParts(_partTargetHeadAndBody, weightsHeadAndBody);
            }
            else if (countLegPlayer == 0)
            {
                _partTargetSelect = _partTargetHead;
            }
            //Use SmallSword
            if (canUseSmallSword)
            {
                armRightUnder.partObject.GetComponent<SpriteRenderer>().sprite = smallSwordSkill.spriteSkillHaveShield;
                int rndCanUseSkill = Random.Range(0, 101);
                EffectSkill _effectSkill = EffectSkill.None;
                if (rndCanUseSkill > 90)
                {
                    _effectSkill = EffectSkill.DodgeShields;
                }
                ControlGamePlay._instance.EnemyAttack(9, _effectSkill, _partTargetSelect);
            }
            else //Use Head bite
            {
                head.partObject.SetActive(false);
                body.partObject.GetComponent<SpriteRenderer>().sprite = biteSkill.spriteSkillHaveShield;
                int rndCanUseSkill = Random.Range(0, 101);
                int _damage = 9;
                if (rndCanUseSkill > 95)
                {
                    _damage *= 2;
                }
                ControlGamePlay._instance.EnemyAttack(_damage, EffectSkill.None, _partTargetSelect);
            }
        }
        if (shield.canUsePart)
        {
            foreach (PartData pD in parts)
            {
                pD.haveShield = true;
            }
        }
    }

    PartData RandomParts(List<PartData> _part, float[] weights)
    {

        float randomValue = Random.Range(0, 1f);
        for (int i = 0; i < _part.Count; i++)
        {
            if (randomValue <= weights[i])
            {
                return _part[i];
            }
            randomValue -= weights[i];
        }
        return _part[0];

    }
    public void TakeDamage(int _damage, EffectSkill _effectSkill, GameObject _part)
    {
        PartData partTarget = null;
        PartData partShield = null;
        int chanceDodge = 5;
        int countLeg = 0;
        if (_effectSkill == EffectSkill.BreakWeapon)
        {
            ControlPlayer cP = ControlGamePlay._instance.controlPlayer;
            cP.FindPartInPlayer("Sword").canUsePart = false;
            cP.AddPartsToRespawnStatus(cP.FindPartInPlayer("Sword"), 3);
        }
        if (_effectSkill == EffectSkill.WeaponWeaknes)
        {
            chanceDodge += 10;
        }
        //find part
        foreach (PartData pD in parts)
        {
            if (pD.namePart == _part.name)
            {
                partTarget = pD;
                if (partTarget.namePart == "Heart")//if part heart check armMid
                {
                    if (pD.namePart == "ArmLeftMid" && pD.canUsePart)
                    {
                        partTarget = pD;
                    }
                    else if (pD.namePart == "ArmRightMid" && pD.canUsePart)
                    {
                        partTarget = pD;
                    }
                }
            }
            if (pD.part.typePart[0] == TypePart.Shield)
            {
                partShield = pD;
            }
            foreach (TypePart tyP in pD.part.typePart)
            {
                if (tyP == TypePart.Leg && pD.canUsePart)
                {
                    countLeg++;
                }
            }
        }
        foreach (TypePart tyP in partTarget.part.typePart)
        {
            if (tyP == TypePart.Head)//player attack head
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
            if (tyP == TypePart.Leg)//player attack Leg
            {
                chanceDodge += 30;
            }
        }
        int rndhitChance = Random.Range(0, 101);
        if (_effectSkill == EffectSkill.ChangeDodgeShields)
        {
            int rndChange = Random.Range(0, 101);
            if (rndChange > 60)
            {
                _effectSkill = EffectSkill.DodgeShields;
            }
        }
        Debug.Log(_effectSkill);
        //if use skill shield
        if (partTarget.haveShield && _effectSkill != EffectSkill.DodgeShields)
        {
            foreach (PartData pD in parts)
            {
                pD.haveShield = false;
            }
            ChangeSpritePart(FindEnemyPart("Shield"), guardSkill.spriteSkillHaveShield);
            partTarget = partShield;
        }
        Debug.Log(partTarget.namePart);
        if (rndhitChance > chanceDodge)
        {
            PartTakeDamage(partTarget, _effectSkill, _damage);
        }
        else
        {
            ControlGamePlay._instance.controlUI.ShowDamageUI(0, "");
        }
    }
    void PartTakeDamage(PartData _partTarget, EffectSkill _effectSkill, int _damage)
    {
        bool isWeakness = false;
        for (int i = 0; i < _partTarget.part.typePart.Length; i++)
        {
            if (_partTarget.part.typePart[i] == TypePart.Weakness)
            {
                isWeakness = true;
            }
            if (_partTarget.part.typePart[i] == TypePart.Leg && _effectSkill == EffectSkill.Legsweep)
            {
                _damage *= 2;
            }
            if (_effectSkill == EffectSkill.BreakTheShield && _partTarget.part.typePart[i] == TypePart.Shield)
            {
                _damage *= 2;
            }
        }
        _partTarget.hpPart -= _damage;
        ControlGamePlay._instance.controlUI.ShowDamageUI(_damage, _partTarget.namePart);//ShowDamage
        if (_partTarget.hpPart <= 0)
        {
            if (isWeakness)
            {
                ControlGamePlay._instance.BoosDie();
            }
            else
            {
                //Off Part
                _partTarget.partObject.SetActive(false);
                //Break Part
                _partTarget.canUsePart = false;
                /*
                **play action spawn enemy**
                */
            }
        }
    }
    PartData FindEnemyPart(string _namePart)
    {
        foreach (PartData pD_E in parts)
        {
            if (pD_E.namePart == _namePart)
            {
                return pD_E;
            }
        }
        return null;
    }
    void ChangeSpritePart(PartData _partData, Sprite _sprite)
    {
        _partData.partObject.GetComponent<SpriteRenderer>().sprite = _sprite;
    }
    public void ReturnToIdel()
    {
        //head //body //armTop //armUnder
        FindEnemyPart("Head").partObject.SetActive(true);
        ChangeSpritePart(FindEnemyPart("Head"), headIdelSkill.spriteSkillHaveShield);
        ChangeSpritePart(FindEnemyPart("Body"), bodyIdelSkill.spriteSkillHaveShield);
        ChangeSpritePart(FindEnemyPart("ArmLeftTop"), armTopIdelSkill.spriteSkillHaveShield);
        ChangeSpritePart(FindEnemyPart("ArmRightTop"), armTopIdelSkill.spriteSkillNotHaveShield);
        ChangeSpritePart(FindEnemyPart("Shield"), armUnderIdelSkill.spriteSkillHaveShield);
        ChangeSpritePart(FindEnemyPart("ArmRightUnder"), armUnderIdelSkill.spriteSkillNotHaveShield);
    }
}
