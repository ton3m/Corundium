using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightHouseView : MonoBehaviour, IInteractable
{
    private LightHousePresenter _presenter;

    public void Init(LightHousePresenter presenter)
    {
        _presenter = presenter;
    }

    public void Interact()
    {
        // as we don't have an inventory, we will use that!
        _presenter.UpdateCurrentUpgradeLevelData(_presenter.GetCurrentLevelUpgradeData()); 
    }

    public void UpdateCurrentLevelData()
    {
        // changes from UI
    }

    private void UpdateUIPanel()
    {
        // reaction on model changes
    }
}
