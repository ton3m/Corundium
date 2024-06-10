using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SetUpMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startHostButton; 
    [SerializeField] private Button _startClientButton; 
    [SerializeField] private TMP_InputField _inputAddressField; 
    private CustomNetworkManager _networkManager;

    [Inject]
    public void Construct(CustomNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    private void Start()
    {
        _startHostButton.onClick.AddListener(_networkManager.StartHost);
        _startClientButton.onClick.AddListener(_networkManager.StartClient);
        _inputAddressField.onEndEdit.AddListener((string newAddress) => _networkManager.networkAddress = newAddress);
    }

    private void OnDisable()
    {
        _startClientButton.onClick.RemoveAllListeners();
        _startHostButton.onClick.RemoveAllListeners();
        _inputAddressField.onEndEdit.RemoveAllListeners();
    }
}
