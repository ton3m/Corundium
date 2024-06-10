using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugTestScript : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    

    public void Init()
    {
        Debug.Log("INit Debug script");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag object: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Handle by: " + eventData.button);
    }
}
