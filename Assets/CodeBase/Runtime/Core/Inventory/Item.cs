using UnityEngine;

namespace CodeBase.Runtime.Core.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item")]
    public class Item : ScriptableObject
    {
        [field: SerializeField] public long ID { get; private set; }
        [field: SerializeField] public GameObject GamePrefab { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}