using Mirror;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Rock : NetworkBehaviour, IDamageable
{
    [SerializeField] private GameObject _stone;
    [SerializeField] private TMP_Text _hpRockText;
    [SerializeField] public int _maxHpRock = 100;
    [SyncVar] private float _hpRock = 100;
    public Type Type { get; }
    
    public int HP => (int)_hpRock;

    private void Start()
    {
        if (isServer)
            _hpRock = _maxHpRock;

        UpdateHpRockText();
    }
    public void ApplyDamage(float damage)
    {
        RpcTakeDamage(damage);
        if (_hpRock <= 1)
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
        NetworkServer.Spawn(stone); 
    }

    private void UpdateHpRockText() 
    {
        _hpRockText.text = "hp = " + _hpRock;
    }
}
