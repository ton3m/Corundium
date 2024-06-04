using Mirror;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Tree : NetworkBehaviour, IDamageable
{
    [SerializeField] private GameObject _wood;
    [SerializeField] private TMP_Text _hpTreeText;
    [SerializeField] public int _maxHpTree = 100;
    [SyncVar] private float _hpTree = 100;
    public Type Type { get; }
    
    public int HP => (int)_hpTree;

    private void Start()
    {
        if (isServer)
            _hpTree = _maxHpTree;

        UpdateHpRockText();
    }

    private void Update()
    {
        if (_hpTree <= 0)
        {
            Extract();
        }
    }

    private void LateUpdate() => UpdateHpRockText();
    
    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
        UpdateHpRockText();
    }

    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        _hpTree -= damage;
    }

    public void Extract()
    {   
        Destroy(gameObject);
        CmdSpawnStone();
        Debug.Log("Drop stone");
    }

    private void CmdSpawnStone()
    {
        GameObject stone = Instantiate(_wood, gameObject.transform.position, quaternion.identity);
        NetworkServer.Spawn(stone); 
    }

    private void UpdateHpRockText()
    {
        _hpTreeText.text = "hp = " + _hpTree;

        ActorMotor target = GameObject.FindObjectOfType<ActorMotor>();
        if (target != null)
        {
            Transform targetTransform = target.transform;

            Vector3 directionToTarget = targetTransform.position - _hpTreeText.transform.position;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                _hpTreeText.transform.rotation = Quaternion.LookRotation(directionToTarget);

                _hpTreeText.transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            Debug.LogWarning("Target object not found!");
        }
    }
}
