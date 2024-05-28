public interface ILightHouseState
{
    int ID { get; }
    void Enter();
    void Exit();
    void SetNextStateID(int nextStateID);
    LightHouseUpgradeLevelData GetLevelUpgradeData();
    void SetUpdatedData(LightHouseUpgradeLevelData updatedData);
    void SetUpdatedItem(LightHouseUpgradeLevelItem updatedItem);
}