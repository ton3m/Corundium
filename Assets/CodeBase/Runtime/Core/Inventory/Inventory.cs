using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Runtime.Core.Inventory
{
    public class Inventory : IInventory
    {
        public event Action InventoryChanged;
        public List<InventorySlot> InventorySlots => _slots;
        
        private readonly List<InventorySlot> _slots;

        public Inventory()
        {
            _slots = new();
        }
        
        public bool TryAdd(Item itemtoAdd, int count = 1)
        {
            try
            {
                foreach (InventorySlot slot in _slots)
                {
                    if (itemtoAdd.ID == slot.Item.ID)
                    {
                        slot.IncreaseCount(count);
                        Debug.Log("Item added, ID:  " + slot.Item.ID + " quantity: "+ slot.Quantity);
                        return true;
                    }
                }
            
                _slots.Add(new InventorySlot(itemtoAdd));
                Debug.Log("New Item added ");
                return true;
            }
            finally
            {
                InventoryChanged?.Invoke();
            }
        }

        public bool TryRemove(Item itemToRemove, int count = 1)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (itemToRemove.ID == slot.Item.ID)
                {
                    if (count > slot.Quantity)
                        return false;
                    
                    slot.DecreaseCount(count);
                    Debug.Log("Item removed, ID:  " + slot.Item.ID + " current quantity: "+ slot.Quantity);

                    if (slot.Quantity == 0)
                        _slots.Remove(slot);
                    
                    InventoryChanged?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public InventorySlot GetItemSlot(Item itemToFind)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (itemToFind.ID == slot.Item.ID)
                    return slot;
            }

            return null;
        }

        public void Dispose()
        {
            _slots.Clear();
        }
    }
}