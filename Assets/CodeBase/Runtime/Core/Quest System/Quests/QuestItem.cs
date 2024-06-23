using System;
using CodeBase.Runtime.Core.Inventory;

namespace CodeBase.Runtime.Core.Quest_System.Quests
{
    [Serializable]
    public class QuestItem
    {
        public Item Item;
        public int Count;
    }
}