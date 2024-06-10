using UnityEngine;

public class RadialMenuElements : MonoBehaviour
{
    public GameObject SelectObject;
    void Start()
    {
        SelectObject.SetActive(false);
    }
    public void Selected()
    {
        SelectObject.SetActive(true);
    }
    public void DeSeleted()
    {
        SelectObject.SetActive(false);
    }
}
