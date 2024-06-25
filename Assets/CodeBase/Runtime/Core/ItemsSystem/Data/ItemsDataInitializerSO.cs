using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ItemsSystem
{
    [CreateAssetMenu(fileName = nameof(ItemsDataInitializerSO), menuName = "Items/Identifier")]
    public class ItemsDataInitializerSO : ScriptableObject, IButtonPressedHandler
    {
        [SerializeField] private string _path = "Items";
        [SerializeField] private ItemsDataInitializer _initializer;

       // private void Awake() => _initializer.Initialize(_path);
        
        public void OnButtonPressed() => _initializer.Initialize(_path);

        public List<ItemData> Data => _initializer.Items;
    }
}