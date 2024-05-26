using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateInGame
{
    WorldArea,FarmingArea,FishingArea,ShopArea,CombatArea,HouseArea
}
public class GameManagerPor : Singleton<GameManagerPor>
{
    [Header("Inventory data")]
    //Inventory
    public InventoryData inventoryData;

    [Header("Manager")]
    public ItemManager itemManager;
    public UIManager uiManager;
    public DataManager dataManager;
    public TimeCycle timeCycle;
    public StateManager stateManager;

    [Header("Status")]
    public StateInGame stateCamera;

    [Header("Script")]
    public ShopManager shopManager;

    public override void Awake()
    {
        base.Awake();

        stateCamera = StateInGame.WorldArea;

        itemManager = GetComponent<ItemManager>();

        inventoryData = FindObjectOfType<InventoryData>();

        uiManager = GetComponent<UIManager>();
        dataManager = GetComponent<DataManager>();
        timeCycle = GetComponent<TimeCycle>();
        stateManager = GetComponent<StateManager>();
    }

}
