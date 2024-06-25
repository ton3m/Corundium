using System;
using System.Collections.Generic;
using CodeBase.ItemsSystem;
using UnityEngine;

public class RadialMenuView : MonoBehaviour
{
    [SerializeField] private List<RadialMenuSectorView> _sectors;

    public int SectorsCount => _sectors.Count;
    
    public void SetSector(int index, ItemData item)
    {
        ValidateIndex(index);
        _sectors[index].Set(item.Icon);
    }

    public void ClearSector(int index)
    {
        ValidateIndex(index);
        _sectors[index].Clear();
    }

    public void SelectSector(int index)
    {
        ValidateIndex(index);
        _sectors[index].Select();
    }

    public void DeselectSector(int index)
    {
        ValidateIndex(index);
        _sectors[index].Select();
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= _sectors.Count)
            throw new IndexOutOfRangeException();
    }
}