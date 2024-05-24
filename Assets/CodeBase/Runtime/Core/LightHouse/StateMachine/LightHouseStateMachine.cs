using UnityEngine;

public class LightHouseStateMachine : ILightHouseStateMachine
{
    private ILightHouseState[] _lightHouseStates;
    private ILightHouseState _currentState;

    public MeshFilter Filter { get; }

    public LightHouseStateMachine(LightHouseData lightHouseData, MeshFilter filter)
    {
        _lightHouseStates = new ILightHouseState[lightHouseData.LevelsData.Length];
        Filter = filter;

        for (int i = 0; i < _lightHouseStates.Length - 1; i++)
        {
            _lightHouseStates[i] = new LightHouseState(this, lightHouseData.LevelsData[i], i);
            //Debug.Log("Created new state: " + _lightHouseStates[i].ID);

            if(i == 0)
            {
                _currentState = _lightHouseStates[i];
                _currentState.Enter();
            }

        }

        for (int i = 0; i < _lightHouseStates.Length - 2; i++)
        {
            _lightHouseStates[i].SetNextStateID(_lightHouseStates[i+1].ID);
        }
    }

    public void SetNewStateByID(int newStateID)
    {
        foreach (var state in _lightHouseStates)
        {
            if(state.ID == newStateID)
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
                break;
            }
        }
    }

    public LightHouseUpgradeLevelData GetCurrentLevelUpgradeData()
    {
        return _currentState.GetLevelUpgradeData();
    }

    public void UpdateCurrentUpgradeLevelData(LightHouseUpgradeLevelData updatedData)
    {
        _currentState.SetUpdatedData(updatedData);
    }

    public void UpdateCurrentUpgradeLevelItem(LightHouseUpgradeLevelItem updatedItem)
    {
        _currentState.SetUpdatedItem(updatedItem);
    }
}
