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

        UpdateHpEnemyImage();
    }
    private void LateUpdate() => UpdateHpEnemyImage();

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
    private void UpdateHpEnemyImage()
    {
        ActorMotor target = FindObjectOfType<ActorMotor>();
        _healthSlider.transform.LookAt(target.transform);
        // if (target != null)
        // {
        //     Transform targetTransform = target.transform;
        //
        //     Vector3 directionToTarget = targetTransform.position - _healthImage.transform.position;
        //     directionToTarget.y = 0;
        //
        //     if (directionToTarget != Vector3.zero)
        //     {
        //         _healthImage.transform.rotation = Quaternion.LookRotation(directionToTarget);
        //
        //         _healthImage.transform.Rotate(0, 180, 0);
        //     }
        // }
        // else
        // {
        //     Debug.LogWarning("Target object not found!");
        // }
    }
}
