using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _damageButton;
    private IInputHandler _inputHandler;
    private ISaveLoadManager _saveLoadManager;
    private bool SetMenuActive = false;

    [Inject]
    public void Construct(IInputHandler inputHandler, ISaveLoadManager saveLoadManager)
    {
        _inputHandler = inputHandler ?? throw new System.ArgumentNullException(nameof(inputHandler));
        _saveLoadManager = saveLoadManager ?? throw new System.ArgumentNullException(nameof(saveLoadManager));
    }

    private void Start()
    {
        _inputHandler.EscPerformed += BoolMenu;
        _menu.SetActive(false);
    
        //_saveButton.onClick.AddListener(_saveLoadManager.SaveGame);
        _loadButton.onClick.AddListener(_saveLoadManager.LoadGame);
        _damageButton.onClick.AddListener(_saveLoadManager.Damage);
    }

    private void OnDestroy()
    {
        _inputHandler.EscPerformed -= BoolMenu;

        _saveButton.onClick.RemoveAllListeners();
        _loadButton.onClick.RemoveAllListeners();
        _damageButton.onClick.RemoveAllListeners();
    }

    private void SetMenu()
    {
        if (SetMenuActive)
        {
            _menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!SetMenuActive)
        {
            _menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    private void BoolMenu()
    {
        SetMenuActive = !SetMenuActive;
        SetMenu();
    }
}
