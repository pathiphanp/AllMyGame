using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    [Header("Part")]
    [SerializeField] PartData[] parts;

    private void Start()
    {
        foreach (PartData pD in parts)
        {
            pD.hpPart = pD.part.hp;
            if (pD.armor != null)
            {
                pD.hpArmor = pD.armor.hp;
            }
            // pD.haveShield = true;
        }
    }
    void SetUpPart()
    {

    }
    void DebuffStun()
    {

    }
    void DebuffCandefend()
    {

    }
    void DebuffBurn()
    {

    }


    public void TakeDamage(int _damage, EffectSkill _effectSkill, GameObject _part)
    {
        PartData partTarget = null;
        PartData partShield = null;
        //find part
        foreach (PartData pD in parts)
        {
            if (pD.namePart == _part.name)
            {
                partTarget = pD;
            }
            if (pD.part.typePart[0] == TypePart.Shield)
            {
                partShield = pD;
            }
        }
        if (partTarget.haveShield)
        {
            partTarget = partShield;
        }
        //have armor
        if (partTarget.hpArmor > 0)
        {
            if (_effectSkill == EffectSkill.BreakTheShield)
            {
                _damage *= 2;

            }
            partTarget.hpArmor -= _damage;// 3 - 5 = -2 
            if (partTarget.hpArmor < 0)
            {
                _damage = Mathf.Abs(partTarget.hpArmor);
                partTarget.hpPart -= _damage;
                partTarget.armor = null;
                Destroy(partTarget.armorObject);
            }


        }
        else//not have armor
        {
            partTarget.hpPart -= _damage;
        }
    }

}
