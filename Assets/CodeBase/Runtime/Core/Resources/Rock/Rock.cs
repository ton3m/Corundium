using Mirror;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Rock : NetworkBehaviour, IDamageable
{
    private ConnectDb _db;
    
    [SerializeField] private GameObject _stone;
    [SerializeField] private TMP_Text _hpRockText;
    [SerializeField] public float _maxHpRock;
    [SyncVar] private float _hpRock;
    public Type Type { get; }
    
    public int HP => (int)_hpRock;

    private void Start()
    {
        _db = FindObjectOfType<ConnectDb>();
        _maxHpRock = _db.GetHPRes("Rock");
        
        if (isServer)
            _hpRock = _maxHpRock;

        UpdateHpRockText();
    }
    private void LateUpdate() => UpdateHpRockText();
    
    private void Update()
    {
        if (_hpRock <= 0)
        {
            Extract();
        }
    }
    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
        if (_hpRock <= 0)
        {
            Extract();
        }
    }

    [ClientRpc]
    private void RpcTakeDamage(float damage)
    {
        _hpRock -= damage;
        UpdateHpRockText();
    }

    public void Extract()
    {   
        Destroy(gameObject);
        CmdSpawnStone();
        Debug.Log("Drop stone");
    }

    private void CmdSpawnStone()
    {
        GameObject stone = Instantiate(_stone, gameObject.transform.position, quaternion.identity);
        //NetworkServer.Spawn(stone); 
    }

    private void UpdateHpRockText()
    {
        _hpRockText.text = "hp = " + _hpRock;

        ActorMotor target = GameObject.FindObjectOfType<ActorMotor>();
        if (target != null)
        {
            Transform targetTransform = target.transform;

            Vector3 directionToTarget = targetTransform.position - _hpRockText.transform.position;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                _hpRockText.transform.rotation = Quaternion.LookRotation(directionToTarget);

                _hpRockText.transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            Debug.LogWarning("Target object not found!");
        }
    }
}
