using CodeBase.Runtime.Core.Inventory;
using CodeBase.Runtime.Core.Quest_System.Quests;
using UnityEngine;
using Zenject;


public class QuestItemsProgressUIController : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ItemViewSlot _itemViewSlotPrefab;
    private QuestItem[] _questItems;
    private IInventory _inventory;

    [Inject]
    public void Construct(IInventory inventory)
    {
        _inventory = inventory;
    }
    
    public void SetUp(QuestItem[] questItems)
    {
        _questItems = questItems;
        _parent.gameObject.SetActive(true);
        
        UpdateView();
        _inventory.InventoryChanged += UpdateView;
    }

    private void OnDestroy()
    {
        _inventory.InventoryChanged -= UpdateView;
    }
    
    private void UpdateView()
    {
        foreach (Transform child in _parent)
        {
            Destroy(child.gameObject);
        }

        ItemViewSlot itemViewSlot;
        
        foreach (var item in _questItems)
        {
            var inventorySlot = _inventory.GetItemSlot(item.Item);
            
            if (inventorySlot == null)
            {
                if (item.Count <= 0)
                    continue;
                
                itemViewSlot = Instantiate(_itemViewSlotPrefab, _parent);
                itemViewSlot.Icon.sprite = item.Item.Icon;
                itemViewSlot.TextCount.text = item.Count.ToString();
                continue;
            }
            
            if (item.Count - inventorySlot.Quantity <= 0)
                continue;

            itemViewSlot = Instantiate(_itemViewSlotPrefab, _parent);
            
            itemViewSlot.Icon.sprite = item.Item.Icon;
            itemViewSlot.TextCount.text = Mathf.Clamp(item.Count - inventorySlot.Quantity, 0, 10).ToString();
        }
        
        if(_parent.childCount == 0)
            _parent.gameObject.SetActive(false);
    }
}
