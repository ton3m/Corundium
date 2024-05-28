using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ChooseLevelDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _levelsDropdown;
    private CustomNetworkManager _networkManager;

    [Inject]
    public void Construct(CustomNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    private void Start()
    {
        var dropDownItems = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData() { text = "Sample Scene" },
            new TMP_Dropdown.OptionData() { text = "VB_Test Scene" }
        };

        _levelsDropdown.ClearOptions();
        _levelsDropdown.AddOptions(dropDownItems);

        _levelsDropdown.onValueChanged.AddListener(ChangeLoadGameplayLevel);
    }

    private void ChangeLoadGameplayLevel(int index)
    {
        if(index == 0)
            _networkManager.onlineScene = GameScenePaths.SampleSceneName;
        else if(index == 1)
            _networkManager.onlineScene = GameScenePaths.VB_TestSceneName;
    }
}
