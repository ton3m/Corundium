using UnityEngine;

public class TestBootsTrap : MonoBehaviour
{
    [SerializeField] private InputHandler InputHandler;
    [SerializeField] private PauseMenu PauseMenu;

    private void Start()
    {
        PauseMenu.Init(InputHandler);
    }
}
