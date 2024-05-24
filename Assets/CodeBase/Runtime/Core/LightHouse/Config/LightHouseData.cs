using UnityEngine;

  
[CreateAssetMenu(fileName = "LightHouseData")]
public class LightHouseData : ScriptableObject
{
    [field: SerializeField] public LightHouseLevelData[] LevelsData { get; private set; }
}
