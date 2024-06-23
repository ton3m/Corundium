using System;
using UnityEngine;

namespace CodeBase.Runtime.Core.Quest_System.Quests
{
    [Serializable]
    public class RewardItem 
    {
        public GameObject Prefab;
        public int Count;
    }
    public class NothingButRewardQuest : Quest
    {
        [SerializeField] private RewardItem[] _rewardItems;
        [SerializeField] private Transform _rewardItemSpawnPoint;
        
        public override void CheckComplete()
        {
            base.CheckComplete();
            
            foreach (var item in _rewardItems)
            {
                Instantiate(item.Prefab, _rewardItemSpawnPoint.position, Quaternion.identity);
                //поменять метод на созданный СЕРВИС который корректно спавнит вещи и пробрасывает их через мультиплеер
            }
        }
    }
}