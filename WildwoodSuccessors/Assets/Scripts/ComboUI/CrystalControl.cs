using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrystalControl : MonoBehaviour
{
    [Header("Crytal")]
    [SerializeField] Image[] crystalColor;
    [Header("RingCastSpell")]
    [SerializeField] public SpriteRenderer ringSprite;
    public void ChangeCrystalColor(Element element)
    {
        foreach (Image crystal in crystalColor)
        {
            Color _color = new Color(128, 71, 28);
            if (element == Element.FIRE)
            {
                _color = Color.red;
            }
            if (element == Element.WATER)
            {
                _color = Color.blue;
            }
            if (element == Element.WIND)
            {
                _color = Color.green;
            }
            if (element == Element.EARTH)
            {
                _color = new Color(0.7137255f, 0.2784314f, 0.1098039f);
            }
            crystal.color = _color;
            ringSprite.color = _color;
        }
    }
}
