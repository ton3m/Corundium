using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.ItemsSystem
{
    public class BaseItemDataSO<T> : ScriptableObject where T : ItemData
    {
        [SerializeField] private T _data;

        public T Data => _data;
    }

    [CreateAssetMenu(fileName = "EmptyItem", menuName = Constants.ItemsDataSOPath + "Empty")]
    public class EmptyItemDataSO : BaseItemDataSO<EmptyItemData>
    {
    }

    [CreateAssetMenu(fileName = "Item", menuName = Constants.ItemsDataSOPath + "Default")]
    public class StackableItemDataSO : BaseItemDataSO<StackableItemData>
    {
    }

    [CreateAssetMenu(fileName = "Tool", menuName = Constants.ItemsDataSOPath + "Tool")]
    public class ToolItemDataSO : BaseItemDataSO<ToolItemData>
    {
    }

    [CreateAssetMenu(fileName = "Food", menuName = Constants.ItemsDataSOPath + "Food")]
    public class FoodItemDataSO : BaseItemDataSO<FoodItemData>
    {
    }
}