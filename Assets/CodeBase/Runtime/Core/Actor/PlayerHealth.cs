using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHelths : NetworkBehaviour, IDamageable
{
    private ConnectDb _db;
    
    [SerializeField] public float _maxHpPlayer = 100;
    [SyncVar] private float _hpPlayer = 100;
    [SerializeField] private Slider _healthSlider;
    public Type Type { get; }
    
    private void Start()
    {
        _db = FindObjectOfType<ConnectDb>();
        _maxHpPlayer = _db.GetHPPlayer();
        
        if (isServer)
            _hpPlayer = _maxHpPlayer;
        
    }
    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
        if (_hpPlayer <= 0)
        {
            Extract();
        }
    }

    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        _hpPlayer -= damage;
        SliderUpdate();
    }

    private void SliderUpdate()
    {
        _healthSlider.value = _hpPlayer / _maxHpPlayer;
    }

    public void Extract()
    {   
        Debug.Log("YOU DIED");
    }
}
