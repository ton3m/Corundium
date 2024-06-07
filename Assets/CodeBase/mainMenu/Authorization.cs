using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    private ConnectDb _db;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    private string _login;
    private string _password;

    private void Start()
    {
        _db = FindObjectOfType<ConnectDb>();
    }
    
    public void SignIn()
    {
        GettingLoginPassword();
        if (_db.SignIn(_login, _password))
        {
            ActivateMenu();
        }
        else
        {
            _textBox.text = "Что то пошло не так :/";
        }
        
    }
    public void SignUp()
    {
        GettingLoginPassword();
        if (_db.SignUp(_login, _password))
        {
            ActivateMenu();
        }
        else
        {
            _textBox.text = "Что то пошло не так :/";
        }
    }
    private void GettingLoginPassword()
    {
        _login = usernameInput.text;
        _password = passwordInput.text;
    }

    private void ActivateMenu()
    {
        gameObject.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
