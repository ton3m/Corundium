using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LightHouseUpgradeLevelItem
{
    public long ID;
    public int Count;
}


[Serializable]
public class LightHouseUpgradeLevelData
{
    public LightHouseUpgradeLevelItem[] Items;
    
}

[CreateAssetMenu(fileName = "LightHouse Levels Upgrade Data")]
public class LightHouseUpgradeData : ScriptableObject
{
    [field: SerializeField] public LightHouseUpgradeLevelData[] LevelLightHouse { get; private set; }
}
