using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ChooseLevelDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _levelsDropdown;
    private CustomNetworkManager _networkManager;
    private string path;
    private string sceneName;

    [Inject]
    public void Construct(CustomNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    private void Start()
    {
        var dropDownItems = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (i <= 1)
                continue;

            path = SceneUtility.GetScenePathByBuildIndex(i);
            sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

            dropDownItems.Add(new TMP_Dropdown.OptionData() { text = sceneName });
        }

        _levelsDropdown.ClearOptions();
        _levelsDropdown.AddOptions(dropDownItems);

        _levelsDropdown.onValueChanged.AddListener(ChangeLoadGameplayLevel);

        _networkManager.onlineScene = dropDownItems[0].text;
    }
 
    private void ChangeLoadGameplayLevel(int index)
    {
        index += 2;

        path = SceneUtility.GetScenePathByBuildIndex(index);
        sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

        _networkManager.onlineScene = sceneName;
    }
}
