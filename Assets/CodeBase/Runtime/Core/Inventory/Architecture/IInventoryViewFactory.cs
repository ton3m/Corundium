using CodeBase.Inventory.View;

namespace CodeBase.Inventory.Architecture
{
    public interface IInventoryViewFactory
    {
        IInventoryView CreateInventory(int capacity);
    }
}