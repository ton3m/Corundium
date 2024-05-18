public interface ILightHouseStateMachine
{
    LightHouseUpgradeLevelData GetCurrentLevelUpgradeData();
    void UpdateCurrentUpgradeLevelData(LightHouseUpgradeLevelData updatedData);
    void UpdateCurrentUpgradeLevelItem(LightHouseUpgradeLevelItem updatedItem);
}
