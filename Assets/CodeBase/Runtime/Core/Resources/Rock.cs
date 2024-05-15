using Mirror;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Rock : NetworkBehaviour, IDamageable
{
    [SerializeField] private GameObject _stone;
    [SerializeField] private TMP_Text _hpRockText;
    [SerializeField] private float _maxHpRock = 100;
    [SyncVar] private float _hpRock = 100f;

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
        CmdSpawnStone();
        Debug.Log("Drop stone");
        Destroy(gameObject);
    }

    private void CmdSpawnStone()
    {
        GameObject stone = Instantiate(_stone, gameObject.transform.position, quaternion.identity);
        NetworkServer.Spawn(stone); 
        Debug.Log("Spawn stone");
    }

    private void UpdateHpRockText() 
    {
        _hpRockText.text = "hp = " + _hpRock;
    }
}
