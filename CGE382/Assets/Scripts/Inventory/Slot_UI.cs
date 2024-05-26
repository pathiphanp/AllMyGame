using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Inventory;
using UnityEngine.InputSystem.EnhancedTouch;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class Slot_UI : MonoBehaviour
{
    public static event Action<Slot_UI> sentSlot;

    //Inheritent
    public Image itemIcon;
    public TextMeshProUGUI valueText;
    public string type;
    public ItemType itemTpye;
    //public SeedType seedType;
    public PlantsType plantsType;
    public int price;

    public int slotID;
    public Inventory inventory;

    [SerializeField] private GameObject highlight;

    [SerializeField]bool onClick = false;
    public static void StartSentSlot(Slot_UI slot_UI)
    {
        Slot_UI.sentSlot?.Invoke(slot_UI);
    }

    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            valueText.text = slot.count.ToString();
            //seedType = slot.seedType;
            type = slot.itemName;
            plantsType = slot.plantsType;
            itemTpye = slot.itemType;
            price = slot.price;
        }
        Debug.Log("Set Item");
    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        valueText.text = "";
        type = null;
        plantsType = null;
        itemTpye = ItemType.None;
    }

    public void SetHighligth(bool isOn)
    {
        highlight.SetActive(isOn);
    }

    //Test self
    public void checkSelf()
    {
        onClick = true;
        Slot_UI.StartSentSlot(this);
    }
}
