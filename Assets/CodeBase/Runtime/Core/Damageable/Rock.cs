using System;
using Mirror;
using TMPro;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Rock : NetworkBehaviour, IDamageable
{
    [SerializeField] private float _maxHpRock = 100;
    [SyncVar] [SerializeField] private float _hpRock;
    //[SerializeField] private TMP_Text _hpRockText;

    private void Awake()
    {
        _hpRock = _maxHpRock;
        //UpdateHpRockText();
    }

    // private void Start()
    // {
    //     
    // }
    [ClientRpc]
    public void ApplyDamage(int damage)
    {
        _hpRock -= damage;
        Debug.Log("hp rock = "+_hpRock);
        if (_hpRock <= 0)
        {
            Destroy(gameObject);
        }
    }
    // private void UpdateHpRockText()
    // {
    //     _hpRockText.text = Convert.ToString(_hpRock);
    // }
}
