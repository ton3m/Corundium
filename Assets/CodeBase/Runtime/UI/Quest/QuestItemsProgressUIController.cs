using System.Collections;
using System.Collections.Generic;
using CodeBase.Runtime.Core.Inventory;
using CodeBase.Runtime.Core.Quest_System.Quests;
using UnityEngine;

public class QuestItemsProgressUIController : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ItemViewSlot _itemViewSlotPrefab;
    public void SetUp()
    {
        _parent.gameObject.SetActive(true);
    }
    
    public void UpdateView(QuestItem[] questItems)
    {
        foreach (Transform child in _parent)
        {
            Destroy(child.gameObject);
        }
            
        foreach (var item in questItems)
        {
            if (item.Count <= 0)
                continue;
            
            var itemViewSlot = Instantiate(_itemViewSlotPrefab, _parent);
            itemViewSlot.Icon.sprite = item.Item.Icon;
            itemViewSlot.TextCount.text = item.Count.ToString();
        }
        
        if(_parent.childCount == 0)
            _parent.gameObject.SetActive(false);
    }
}
