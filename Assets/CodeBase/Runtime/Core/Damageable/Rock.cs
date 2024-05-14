using System;
using Mirror;
using TMPro;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Rock : NetworkBehaviour, IDamageable
{
    [SerializeField] private float _maxHpRock = 100;
    [SyncVar] [SerializeField] private float _hpRock = 100f;
    [SerializeField] private TMP_Text _hpRockText;
    
    private void Start()
    {
        if (isServer)
            _hpRock = _maxHpRock;
        UpdateHpRockText();
    }
    
    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
        if (_hpRock <= 0)
        {
            DestroyTheRock();
        }
    }
    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        //Debug.Log("hp rock = "+_hpRock);
        _hpRock -= damage;
        UpdateHpRockText();
    }

    private void DestroyTheRock()
    {
        Destroy(gameObject);
    } 
    private void UpdateHpRockText() 
    {
        _hpRockText.text = ("hp = " + _hpRock);
        Debug.Log("hp rock = " + _hpRock);
    }
}
