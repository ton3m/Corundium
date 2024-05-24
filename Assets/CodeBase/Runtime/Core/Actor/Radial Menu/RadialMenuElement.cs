using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuElement : MonoBehaviour
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
