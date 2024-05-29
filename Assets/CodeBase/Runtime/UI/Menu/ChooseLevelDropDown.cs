using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;



public class ChooseLevelDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _levelsDropdown;
    //[SerializeField] private Scene[] _scenes;
    private CustomNetworkManager _networkManager;

    [Inject]
    public void Construct(CustomNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    private void Start()
    {
        _networkManager.onlineScene = GameScenePaths.SampleSceneName;
        
        var dropDownItems = new List<TMP_Dropdown.OptionData>()
        {
            new TMP_Dropdown.OptionData() { text = "Sample Scene" },
            new TMP_Dropdown.OptionData() { text = "VB_Test Scene" },
            new TMP_Dropdown.OptionData() { text = "EnemyScene" }

        };
        //var t = SceneManager.sceneCount;

        // foreach (var scene in _scenes)
        // {
        //     if(_scenes.Length == 0)
        //         break;
        //     Debug.Log("New Scene: " + scene.name);
        //     dropDownItems.Add(new TMP_Dropdown.OptionData() { text = scene.name });
        // }

        // for (int i = SceneManager.sceneCountInBuildSettings-1; i > 0; i--)
        // {
        //     // if(i == 1)
        //     //     break;
        //     Debug.Log("New Scene: " + SceneManager.GetSceneAt(i).name);
        //     dropDownItems.Add(new TMP_Dropdown.OptionData() { text = SceneManager.GetSceneAt(i).name });
        // }

        _levelsDropdown.ClearOptions();
        _levelsDropdown.AddOptions(dropDownItems);

        _levelsDropdown.onValueChanged.AddListener(ChangeLoadGameplayLevel);
    }

    private void ChangeLoadGameplayLevel(int index)
    {
        // _networkManager.onlineScene = _scenes[index].name;
        if(index == 0)
            _networkManager.onlineScene = GameScenePaths.SampleSceneName;
        else if(index == 1)
            _networkManager.onlineScene = GameScenePaths.VB_TestSceneName;
        else if(index == 2)
            _networkManager.onlineScene = "EnemyScene";

    }
}
