using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    public List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    public Slot_UI selectedSlot;

    //Inventory
    public string inventoryName;
    [HideInInspector] public Inventory inventory;
    public PlayerFarming playerFarming;
    public InventoryManager inventoryManager;
    private void Start()
    {
        //inventory = GameManagerPor.Instance.inventoryData.inventory.GetInventoryByName(inventoryName);
        inventory = inventoryManager.toolbar;
        SelectSlot(0);
    }

    public void SelectSlot(int index)
    {
        inventory = inventoryManager.toolbar;
        if(toolbarSlots.Count == 9)
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighligth(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighligth(true);
        }
    }
}
