using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    [Header("UI Components")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _stopGameplayButton;
    private IInputHandler _inputHandler;
    private ISaveLoadManager _saveLoadManager;
    private CustomNetworkManager _networkManager;
    private SceneStateMachine _sceneStateMachine;
    private bool _isMenuActive;

    [Inject]
    public void Construct(IInputHandler inputHandler, ISaveLoadManager saveLoadManager, 
        CustomNetworkManager networkManager, SceneStateMachine stateMachine)
    {
        _inputHandler = inputHandler;
        _saveLoadManager = saveLoadManager;
        _networkManager = networkManager;
        _sceneStateMachine = stateMachine;
    }

    private void Start()
    {
        _inputHandler.EscPerformed += ResetMenuBool;

        //_saveButton.onClick.AddListener(_saveLoadManager.SaveGame);
        _loadButton.onClick.AddListener(_saveLoadManager.LoadData);
        _stopGameplayButton.onClick.AddListener(_sceneStateMachine.EnterIn<ExitGameplayState>);

        _isMenuActive = false;
        UpdateMenuVisible();
    }

    private void OnDestroy()
    {
        _inputHandler.EscPerformed -= ResetMenuBool;

        _saveButton.onClick.RemoveAllListeners();
        _loadButton.onClick.RemoveAllListeners();
        _stopGameplayButton.onClick.RemoveAllListeners();
    }

    private void UpdateMenuVisible()
    {
        _menu.SetActive(_isMenuActive);

        if(_isMenuActive)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = _isMenuActive;
    }

    private void ResetMenuBool()
    {
        _isMenuActive = !_isMenuActive;
        UpdateMenuVisible();
    }
}
