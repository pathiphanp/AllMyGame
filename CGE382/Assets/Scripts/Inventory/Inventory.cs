using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string itemName;
        //public SeedType seedType;
        public int count;
        public int maxAllowed;
        public int price;
        public PlantsType plantsType;

        public Sprite icon;
        public ItemType itemType;

        public Slot()
        {   
            itemName = "";
            //seedType = SeedType.None;
            count = 0;
            maxAllowed = 99;
        }

        public bool IsEmpty
        {
            get
            {
                if(itemName == "" && count == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool CanAddItem(string slot)
        {
            if(count < maxAllowed)
            {
                return true;
            }

            return false;
        }

        //OG Add item from item drop
        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            //this.seedType = item.seedType;
            this.icon = item.data.icon;
            this.itemType = item.data.itemType;
            this.plantsType = item.data.plantType;
            this.price = item.data.price;
            count++;
        }

        //Adjust Add item from plants
        public void AddItem(ItemData item, int numToAdd)
        {
            this.itemName = item.itemName;
            this.icon = item.icon;
            this.itemType = item.itemType;
            this.plantsType = item.plantType;
            this.price = item.price;
            count += numToAdd;
        }

        public void BuyItemAdd(ItemData item, int numToAdd)
        {

        }

        //
        public void AddItem(string itemName, Sprite icon, int maxAllowed, PlantsType plantsType, ItemType itemType,int price)
        {
            this.itemName = itemName;
            //this.seedType = seed;
            this.icon = icon;
            this.plantsType = plantsType;
            this.itemType = itemType;
            count++;
            this.maxAllowed = maxAllowed;
            this.price = price;
        }

        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;

                if(count == 0)
                {
                    icon = null;
                    itemName = "";
                    //seedType = SeedType.None;
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    //OG Add item from item drop
    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName)
            {
                slot.AddItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    //Adjust Add Item from plants
    public void Add(ItemData item, int numToAdd)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.itemName)
            {
                slot.AddItem(item, numToAdd);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(item, numToAdd);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for(int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];


        if(toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for(int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed, fromSlot.plantsType, fromSlot.itemType, fromSlot.price);
                fromSlot.RemoveItem();
            }
        }
    }
}
