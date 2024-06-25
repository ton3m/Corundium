using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ItemsSystem
{
    public class ItemsDataProvider 
    {
        public EmptyItemData EmptyItem { get; private set; }
        
        private readonly List<ItemData> _items;

        public ItemsDataProvider(List<ItemData> items, EmptyItemData emptyItem)
        {
            _items = items;
            EmptyItem = emptyItem;
        }

        public ItemData GetItemById(string id)
        {
            foreach (var item in _items)
                if (item.Id == id)
                    return item;

            throw GetException(id);
        }

        public bool IsValid(string id)
        {
            bool result = false;
            
            _items.ForEach(item =>
            {
                if (item.Id == id)
                    result = true;
            });

            return result;
        }

        public ItemData GetRandomItem()
        {
            int index = UnityEngine.Random.Range(0, _items.Count);

            return _items[index];
        }

        public bool IsEmptyItem(string id) => 
            id == EmptyItem.Id;

        private static Exception GetException(string id) =>
            new($"No itemData with id {id} found.");
    }
}