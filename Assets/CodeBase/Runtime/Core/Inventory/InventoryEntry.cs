using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryEntry : MonoBehaviour
    {
        [SerializeField] private ItemsIdentifierSO _itemsDataController;

        private void Start()
        {
            List<ItemData> itemData = _itemsDataController.GetItemsData();

            ItemsSorter sorter = new();
            List<ItemData> items = sorter.GetEmptySorted(itemData, out var emptyItem);

            ItemsDataProvider provider = new(items, emptyItem);
                
            Debug.Log(provider.GetItemById("emerald").MaxStackAmount);
            
            //            ItemsDataKeeper keeper = new(itemData);
//            List<ItemData> items = keeper.GetItemsData(out var emptyItem);

            //  ItemsDataProvider itemsDataProvider = new(items, emptyItem);

            // Debug.Log(itemsDataProvider.EmptyItem.Id);
            // Debug.Log(itemsDataProvider.GetItemBy("emerald"));

            // List<ItemData> itemsData = _itemsDataSoLoader.GetItemsData();
            // ItemData emptyItemData = _itemsDataSoLoader.GetEmptyItemData();
            //
            // ItemsDataProvider provider = new(itemsData, emptyItemData);
            // Identifier identifier = new Identifier();
            //
            // _itemsDataSoLoader.Construct(identifier);
            //
            // Debug.Log(provider.GetItemBy("Stones").Id);
        }
    }
}