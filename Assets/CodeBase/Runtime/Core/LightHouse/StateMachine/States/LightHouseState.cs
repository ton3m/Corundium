using UnityEngine;

public class LightHouseState : ILightHouseState
{
    private readonly LightHouseStateMachine _stateMachine;
    private readonly LightHouseLevelData _levelData;
    private int _nextStateID;

    public int ID { get; }

    public LightHouseState(LightHouseStateMachine stateMachine, LightHouseLevelData levelData, int stateID)
    {
        _stateMachine = stateMachine ?? throw new System.ArgumentNullException(nameof(stateMachine));
        _levelData = levelData ?? throw new System.ArgumentNullException(nameof(levelData));
        ID = stateID;
    }

    public void Enter()
    {
        // ping events about enter new level and fog will receive and disperce
        // set new model of LightHouse
        // etc
        _stateMachine.Filter.mesh = _levelData.LightHouseMesh;
        Debug.Log("Enter New state, ID:  " + ID);
    }

    public void Exit()
    {
        // not yet
    }

    public void SetUpdatedData(LightHouseUpgradeLevelData updatedData)
    {
        if(updatedData == null)
            return;
            
        _levelData.UpgradeData = updatedData;

        if(IsCurrentUpgradeConditionsComplete())
        {
            _stateMachine.SetNewStateByID(_nextStateID);
        } 
    }

    public void SetUpdatedItem(LightHouseUpgradeLevelItem updatedItem)
    {
        // not yet
    }
    public LightHouseUpgradeLevelData GetLevelUpgradeData()
    {
        return _levelData.UpgradeData;
    }

    public void SetNextStateID(int nextStateID)
    {
        _nextStateID = nextStateID;
    }

    private bool IsCurrentUpgradeConditionsComplete()
    {
        if(_levelData.UpgradeData.Items == null)
            return true;

        foreach (var item in _levelData.UpgradeData.Items)
        {
            if(item.CountToUpdate > 0)
                return false;
        }

        return true;
    }
}
