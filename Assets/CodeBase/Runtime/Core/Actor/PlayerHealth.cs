using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHelths : NetworkBehaviour, IDamageable
{
    [SerializeField] public int _maxHpPlayer = 100;
    [SyncVar] private float _hpPlayer = 100;
    [SerializeField] private Slider _hpSlider;
    public Type Type { get; }
    
    private void Start()
    {
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
        _hpSlider.value = _hpPlayer / _maxHpPlayer;
    }

    public void Extract()
    {   
        Debug.Log("YOU DIED");
    }
    
}
