
public class LightHousePresenter
{
    private readonly ILightHouseStateMachine _model;

    public LightHousePresenter(ILightHouseStateMachine stateMachine)
    {
        _model = stateMachine;
    }

    public LightHouseUpgradeLevelData GetCurrentLevelUpgradeData()
    {
        return _model.GetCurrentLevelUpgradeData();
    }

    public void UpdateCurrentUpgradeLevelData(LightHouseUpgradeLevelData updatedLevelData)
    {
        _model.UpdateCurrentUpgradeLevelData(updatedLevelData);
    }
}
