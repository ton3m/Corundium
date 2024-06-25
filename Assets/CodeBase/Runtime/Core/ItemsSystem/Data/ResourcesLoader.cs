using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.ItemsSystem
{
    public class ResourcesLoader
    {
        public List<T> LoadAll<T>(string path) where T : Object
        {
            List<T> items = Resources.LoadAll<T>(path).ToList();

            Debug.Log($"{nameof(ResourcesLoader)}: Loaded {items.Count} object. {typeof(T)}");

            return items;
        }
    }
}