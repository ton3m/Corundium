using System;
using UnityEngine;


[Serializable]
public class LightHouseUpgradeLevelItem
{
    public long ItemID;
    public int CountToUpdate;
}


[Serializable]
public class LightHouseUpgradeLevelData
{
    public LightHouseUpgradeLevelItem[] Items;
}


[Serializable]
public class LightHouseLevelData
{
    public LightHouseUpgradeLevelData UpgradeData;
    public float ForRadiusDispersion;
    public Mesh LightHouseMesh; // will activate 
}


[CreateAssetMenu(fileName = "LightHouseData")]
public class LightHouseData : ScriptableObject
{
    [field: SerializeField] public LightHouseLevelData[] LevelsData { get; private set; }
}
