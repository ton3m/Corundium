using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.ItemsSystem
{
    [Serializable]
    public class ItemData
    {
        [field: SerializeField] //[field: ReadOnlyInspector]
        public string Id { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }

        public void SetId(string id) => Id = id;

        public ItemData(ItemData data)
        {
            Id = data.Id;
            Name = data.Name;
            Icon = data.Icon;
            Description = data.Description;
        }

        public ItemData(string id, string name, Sprite sprite, string description)
        {
            Id = id;
            Name = name;
            Icon = sprite;
            Description = description;
        }
    }

    [Serializable]
    public class EmptyItemData : ItemData
    {
        public EmptyItemData(ItemData data) : base(data)
        {
        }
    }

    [Serializable]
    public class StackableItemData : ItemData
    {
        [SerializeField] private int _stackAmount = 1;
        
        public StackableItemData(ItemData data, int stackAmount) : base(data)
        {
            _stackAmount = stackAmount;
        }
    }

    [Serializable]
    public class ToolItemData : StackableItemData
    {
        [SerializeField] private float _maxDurability = 100;

        public ToolItemData(ItemData data, float maxDurability, int stackAmount) : base(data, stackAmount)
        {
            _maxDurability = maxDurability;
        }
    }

    [Serializable]
    public class FoodItemData : StackableItemData
    {
        [SerializeField] private float _satiety = 10;

        public FoodItemData(ItemData data, float satiety, int stackAmount) : base(data, stackAmount)
        {
            _satiety = satiety;
        }
    }
}