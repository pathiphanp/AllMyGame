using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    StateInGame state;

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
        //state = GameManagerPor.Instance.stateCamera;
        //state = StateInGame.WorldArea;
    }

    // Update is called once per frame
    void Update()
    {
        state = GameManagerPor.Instance.stateCamera;
    }

    public void SetState()
    {
        if (state == StateInGame.WorldArea)
        {
            AreaPresetOn(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (state == StateInGame.FarmingArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOn(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (state == StateInGame.FishingArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOn(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (state == StateInGame.ShopArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOn(shopArea);
            AreaPresetOff(houseArea);
            AreaPresetOff(combatArea);
        }
        else if (state == StateInGame.HouseArea)
        {
            AreaPresetOff(worldArea);
            AreaPresetOff(farmingArea);
            AreaPresetOff(fishingArea);
            AreaPresetOff(shopArea);
            AreaPresetOn(houseArea);
            AreaPresetOff(combatArea);
        }
        else if(state == StateInGame.CombatArea)
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
        foreach (GameObject obj in objArea)
        {
            obj.SetActive(true);
        }
    }

    public void AreaPresetOff(GameObject[] objArea)
    {
        foreach (GameObject obj in objArea)
        {
            obj.SetActive(false);
        }
    }
}
