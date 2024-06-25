using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.ItemsSystem
{
    [Serializable]
    public class ItemsDataInitializer
    {
        [field: SerializeField] 
        private List<ItemData> _items;

        private ResourcesLoader _loader;
        private Identifier _identifier;

        public ItemsDataInitializer()
        {
            _loader = new ResourcesLoader();
            _identifier = new Identifier();
        }

        public List<ItemData> Initialize(string path)
        {
            _items = _loader.LoadAll<BaseItemDataSO<ItemData>>(path)
                .Select(item => item.Data).ToList();

            IdentifyItems(_items);

            return _items;
        }

        public List<ItemData> Items => _items;

        private void IdentifyItems(List<ItemData> items)
        {
            List<string> names = items.Select(item => item.Name).ToList();
            List<string> ids = _identifier.GetIdsByNames(names, null);

            for (int i = 0; i < items.Count; i++)
                items[i].SetId(ids[i]);
        }
    }
}