using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PresetCamearaArea
{
    WorldArea, FarmingArea, FishingArea, ShopArea, HouseArea, CombatArea
}

[ExecuteInEditMode]
public class EditManager : MonoBehaviour
{
    [Header("Area to edit")]
    public PresetCamearaArea preset;

    [Header("Component world area")]
    public GameObject[] worldArea;

    [Header("Component framing area")]
    public GameObject[] farmingArea;

    [Header("Component fishing area")]
    public GameObject[] fishingArea;

    [Header("Component shop area")]
    public GameObject[] shopArea;

    [Header("Component house area")]
    public GameObject[] houseArea;

    [Header("Component combat area")]
    public GameObject[] combatArea;

    // Start is called before the first frame update
    void Start()
    {
        preset = PresetCamearaArea.WorldArea;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (preset == PresetCamearaArea.WorldArea)
        {
            AreaPresetOn(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (preset == PresetCamearaArea.FarmingArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOn(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (preset == PresetCamearaArea.FishingArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOn(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (preset == PresetCamearaArea.ShopArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOn(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (preset == PresetCamearaArea.HouseArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOn(houseArea);
            AreaPresetOff(combatArea);
        }
        else if(preset == PresetCamearaArea.CombatArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOn(combatArea);
        }

    }

    public void AreaPresetOn(GameObject[] objArea)
    {
        foreach( GameObject obj in objArea)
        {
            obj.SetActive(true);
        }
    }

    public void AreaPresetOff(GameObject[] objArea)
    {
        foreach( GameObject obj in objArea)
        {
            obj.SetActive(false);
        }
    }
}
