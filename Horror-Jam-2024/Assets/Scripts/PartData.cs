using UnityEngine;

[System.Serializable]
public class PartData
{
    public string namePart;
    [Header("Part")]
    public GameObject partObject;
    public Part part;
    public int hpPart;
    [Header("Armor")]
    public GameObject armorObject;
    public Part armor;
    public int hpArmor;
    [Header("Status")]
    public bool canUsePart = true;
    [HideInInspector] public bool haveShield;
}
