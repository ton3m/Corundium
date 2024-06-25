using System.Collections.Generic;

namespace CodeBase.ItemsSystem
{
    public class ItemsSorter
    {
        public List<ItemData> GetEmptySorted(List<ItemData> allItems, out EmptyItemData emptyItem)
        {
            List<ItemData> items = new();
            emptyItem = null;

            foreach (var item in allItems)
            {
                if (item is EmptyItemData empty)
                {
                    emptyItem ??= empty;
                    continue;
                }

                items.Add(item);
            }

            return items;
        }
    }
}