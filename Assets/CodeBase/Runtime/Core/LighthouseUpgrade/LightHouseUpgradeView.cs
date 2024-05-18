using UnityEngine;
using Mirror;
using Unity.Mathematics;
using UnityEditor;

public class LightHouseUpgradeView: NetworkBehaviour, IInteractable
{
        [SerializeField] private GameObject _uiLighthouseLevelUpgrade;
        
        public void Interact()
        {
                _uiLighthouseLevelUpgrade.SetActive(true);
        }
}
