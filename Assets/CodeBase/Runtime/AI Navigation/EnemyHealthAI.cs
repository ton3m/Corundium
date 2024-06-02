using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthAi : NetworkBehaviour, IDamageable
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] public int _maxHpEnemy = 100;
    [SyncVar] private float _hpEnemy = 100;
    public Type Type { get; }

    public int HP => (int)_hpEnemy;

    private void Start()
    {
        if (isServer)
            _hpEnemy = _maxHpEnemy;

        UpdateHpEnemySlider();
    }
    private void LateUpdate() => UpdateHpEnemySlider();

    public void ApplyDamage(float damage)
    {
        Debug.Log(damage);
        RpcTakeDamage(damage);
        if (_hpEnemy <= 0)
        {
            Extract();
        }
    }

    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        _hpEnemy -= damage;
        SliderUpdate();
    }

    public void Extract()
    {
        Destroy(gameObject);
    }
    private void SliderUpdate()
    {
        _healthSlider.value = _hpEnemy / _maxHpEnemy;
    }
    private void UpdateHpEnemySlider()
    {
        //надо переделать
        ActorMotor target = FindObjectOfType<ActorMotor>();
        _healthSlider.transform.LookAt(target.transform);
        
    }
}
