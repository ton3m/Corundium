using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NoToDo Quest")]
public class NoToDoQuest : Quest
{
    public override void CheckComplete()
    {
        foreach (var item in RewardItems)
        {
            GameObject.Instantiate(item.Prefab, RewardItemSpawnPoint.position, Quaternion.identity);
            //поменять метод на созданный СЕРВИС который корректно спавнит вещи и пробрасывает их через мультиплеер
        }

        base.CheckComplete();
    }
}

