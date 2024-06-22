using System;
using System.Collections.Generic;

namespace CodeBase.Runtime.Core.Inventory
{
    public interface IInventory : IDisposable
    {
        event Action InventoryChanged;
        List<InventorySlot> InventorySlots { get; }
        bool TryAdd(Item itemtoAdd, int count = 1);
        bool TryRemove(Item itemToRemove, int count = 1);
        InventorySlot GetItemSlot(Item itemToFind);
    }
}