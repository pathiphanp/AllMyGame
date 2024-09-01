using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlUIPlayer : MonoBehaviour
{
    [SerializeField] public TMP_Text maxHPText;
    [SerializeField] public TMP_Text hpText;
    [SerializeField] public TMP_Text damageText;
    [SerializeField] public TMP_Text chancePointLettersUpText;
    [SerializeField] TMP_Text[] healPotionText;
    [SerializeField] TMP_Text[] buffdamagePotionText;
    [SerializeField] TMP_Text crystelText;

    public void SetHealPotion(int _value)
    {
        foreach (TMP_Text ht in healPotionText)
        {
            ht.text = _value.ToString();
        }
    }
    public void SetBuffDamagePotion(int _value)
    {
        foreach (TMP_Text bdt in buffdamagePotionText)
        {
            bdt.text = _value.ToString();
        }
    }

    public void ResetPlayerCrystal()
    {
        crystelText.text = ControlGamePlay._instance.controlPlayer.myCrystal.ToString();
    }
}
