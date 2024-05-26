using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public PlayerFarming playerFarming;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] Canvas canvas;

    public string inventoryName;

    public Inventory inventory;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        //canvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Start()
    {
        //inventory = GameManagerPor.Instance.inventoryData.inventory.GetInventoryByName(inventoryName);

        if (this.gameObject.name == "Toolbar")
        {
            inventory = inventoryManager.toolbar;
        }
        else
        {
            inventory = inventoryManager.backpack;
        }
        
        SetupSlot();
        Refresh();
    }

    public void Update()
    {

    }

    public void Refresh()
    {
        if (this.gameObject.name == "Toolbar")
        {
            inventory = inventoryManager.toolbar;
        }
        else
        {
            inventory = inventoryManager.backpack;
        }
        if (slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Debug.Log(inventory.slots[i].itemName);
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                    Debug.Log("set" + i);
                }
                else
                {
                    slots[i].SetEmpty();
                    Debug.Log("set empty" + i);
                }
            }
            Debug.Log(slots.Count + ": " + this.gameObject.name + " == " + inventory.slots.Count + ": " + this.gameObject.name);
        }
    }

    public void Remove()
    {
        Item itemToDrop = GameManagerPor.Instance.itemManager.GetItemByName(inventory.slots[UIManager.draggedSlot.slotID].itemName);

        if (itemToDrop != null)
        {
            if (UIManager.draggedSlot.itemTpye == ItemType.Sell_Only)
            {
                if (playerFarming.dragSingle)
                {
                    inventory.Remove(UIManager.draggedSlot.slotID);
                    Refresh();
                    GameManagerPor.Instance.dataManager.money += UIManager.draggedSlot.price;
                    AudioManager.Instance.PlaySFX("BuyAndSell02");

                    Debug.Log("Remove item drag single" + itemToDrop);
                }
                else
                {
                    GameManagerPor.Instance.dataManager.money += UIManager.draggedSlot.price * inventory.slots[UIManager.draggedSlot.slotID].count;
                    AudioManager.Instance.PlaySFX("BuyAndSell02");
                    inventory.Remove(UIManager.draggedSlot.slotID, inventory.slots[UIManager.draggedSlot.slotID].count);
                    Refresh();
                    Debug.Log("Remove item drag more than one");
                }
            }
        }
        //Debug.Log("sell");
        UIManager.draggedSlot = null;

    }

    public void SellItem()
    {

    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        UIManager.draggedSlot = slot;
        UIManager.draggedIcon = Instantiate(UIManager.draggedSlot.itemIcon);
        UIManager.draggedIcon.transform.SetParent(canvas.transform);
        UIManager.draggedIcon.raycastTarget = false;
        UIManager.draggedIcon.rectTransform.sizeDelta = new Vector2(100, 100);

        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        //Debug.Log("Start Drag: " + UIManager.draggedSlot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        //Debug.Log("Drtagging: " + UIManager.draggedSlot.name);
    }

    public void SlotEndDrag()
    {
        Destroy(UIManager.draggedIcon.gameObject);
        UIManager.draggedIcon = null;
        //Debug.Log("Done Dragging: " + draggedSlot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        if (playerFarming.dragSingle)
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotID, slot.slotID, slot.inventory);

        }
        else
        {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotID, slot.slotID, slot.inventory, UIManager.draggedSlot.inventory.slots[UIManager.draggedSlot.slotID].count);
        }
        Refresh();
        GameManagerPor.Instance.uiManager.RefreshAll();
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    void SetupSlot()
    {
        int counter = 0;

        foreach (Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
