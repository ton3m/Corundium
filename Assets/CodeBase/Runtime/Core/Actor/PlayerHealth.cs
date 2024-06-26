using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class PlayerHelths : NetworkBehaviour, IDamageable
{
    private ConnectDb _db;
    
    [SerializeField] public float _maxHpPlayer = 100;
    [SyncVar] public float _hpPlayer = 100;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private ActorMotor _actorMotor;
    [SerializeField] private GameObject _deathScreen;
    public Type Type { get; }
    
    private IInputHandler _inputHandler;
    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    private void Start()
    {
        _db = FindObjectOfType<ConnectDb>();
        _maxHpPlayer = _db.GetHPPlayer();
        
        if (isServer)
            _hpPlayer = _maxHpPlayer;
    }

    private void Update()
    {
        SliderUpdate();
        if (_hpPlayer <= 0)
        {
            Died();
        }
    }

    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
    }

    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        _hpPlayer -= damage;
    }

    private void SliderUpdate()
    {
        _healthSlider.value = _hpPlayer / _maxHpPlayer;
    }

    private void Died()
    {   
        _deathScreen.SetActive(true);
        _actorMotor.enabled = false;
    }
    
}
