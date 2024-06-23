using UnityEngine;

namespace CodeBase.Runtime.Core.Inventory
{
    public class InventorySlot
    {
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = Mathf.Clamp(value, 0, 100);
        }

        public Item Item { get; }

        public InventorySlot(Item item)
        {
            Item = item;
            _quantity = 1;
        }
        
        public void IncreaseCount(int count)
        {
            if(count > 0)
                Quantity += count;
        }

        public void DecreaseCount(int count)
        {
            if(count > 0)
                Quantity -= count;
        }
    }
}