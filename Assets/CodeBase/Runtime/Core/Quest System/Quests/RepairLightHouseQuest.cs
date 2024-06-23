using CodeBase.Runtime.Core.Inventory;
using UnityEngine;
using Zenject;

namespace CodeBase.Runtime.Core.Quest_System.Quests
{
    public class RepairLightHouseQuest : Quest
    {
        [SerializeField] private QuestItemsProgressUIController _uiController;
        [SerializeField] private LightHouseView _lightHouseView;
        [SerializeField] private QuestItem[] _questItems; 
        private IInventory _inventory;

        [Inject]
        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        public override void SetUpUI()
        {
            _uiController.SetUp();
            _uiController.UpdateView(_questItems);
        }

        public override void CheckComplete()
        {
            if (CheckAllQuestItemsContained())
            {
                base.CheckComplete();
                _lightHouseView.Repair();
                return;
            }

            QuestItem questItem;
            InventorySlot inventoryItem;

            for (int i = 0; i < _questItems.Length; i++)
            {
                questItem = _questItems[i];
                inventoryItem = _inventory.GetItemSlot(questItem.Item);
            
                if (inventoryItem != null)
                {
                    questItem.Count -= inventoryItem.Quantity;
                    _inventory.TryRemove(inventoryItem.Item, inventoryItem.Quantity);
                }
            }
        
            if (CheckAllQuestItemsContained())
            {
                _lightHouseView.Repair();
                base.CheckComplete();
            }
            
            _uiController.UpdateView(_questItems);
        }

        private bool CheckAllQuestItemsContained()
        {
            for (int i = 0; i < _questItems.Length; i++)
            {
                if (_questItems[i].Count > 0)
                    return false;
            }

            return true;
        }
    }
}