using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private InputHandler _esc;
    private bool SetMenuActive = false;
    public void Init(InputHandler _esc)
    {
        _menu.SetActive(false);
        _esc.EscPerformed += BoolMenu;
    }
    void SetMenu()
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
    void BoolMenu()
    {
        SetMenuActive = !SetMenuActive;
        SetMenu();
    }
}
